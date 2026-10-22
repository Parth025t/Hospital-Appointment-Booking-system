using HospitalAppointmentSystem.Data;
using HospitalAppointmentSystem.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => { options.LoginPath = "/Account/Login"; options.AccessDeniedPath = "/Account/Login"; options.Cookie.HttpOnly = true; options.Cookie.SameSite = SameSiteMode.Lax; options.ExpireTimeSpan = TimeSpan.FromHours(8); });
builder.Services.AddAuthorization();
var connection = builder.Configuration.GetConnectionString("HospitalConnection") ?? throw new InvalidOperationException("HospitalConnection is missing");
builder.Services.AddDbContext<HospitalContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 30))));
var app = builder.Build();
try {
    var cs = new MySqlConnectionStringBuilder(connection);
    var database = cs.Database;
    if (string.IsNullOrWhiteSpace(database) || !System.Text.RegularExpressions.Regex.IsMatch(database, "^[a-zA-Z0-9_]+$")) throw new InvalidOperationException("Invalid database name");
    cs.Database = "";
    await using (var server = new MySqlConnection(cs.ConnectionString)) { await server.OpenAsync(); await using var cmd = server.CreateCommand(); cmd.CommandText = $"CREATE DATABASE IF NOT EXISTS `{database}` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci"; await cmd.ExecuteNonQueryAsync(); }
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<HospitalContext>();
    await db.Database.EnsureCreatedAsync();
    await db.Database.ExecuteSqlRawAsync("CREATE TABLE IF NOT EXISTS `Departments` (`Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY, `Name` varchar(500) NULL, `Floor` varchar(500) NULL, `Description` varchar(500) NULL) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4");
    await db.Database.ExecuteSqlRawAsync("CREATE TABLE IF NOT EXISTS `Rooms` (`Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY, `RoomNumber` varchar(500) NULL, `Ward` varchar(500) NULL, `Type` varchar(500) NULL, `DailyRate` decimal(18,2) NULL, `IsOccupied` tinyint(1) NULL) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4");
    await db.Database.ExecuteSqlRawAsync("CREATE TABLE IF NOT EXISTS `Staff` (`Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY, `FullName` varchar(500) NULL, `JobTitle` varchar(500) NULL, `Phone` varchar(500) NULL, `Shift` varchar(500) NULL) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4");
    await db.Database.ExecuteSqlRawAsync("CREATE TABLE IF NOT EXISTS `Medicines` (`Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY, `Name` varchar(500) NULL, `Category` varchar(500) NULL, `Stock` int NULL, `UnitPrice` decimal(18,2) NULL, `ExpiryDate` datetime(6) NULL) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4");
    await db.Database.ExecuteSqlRawAsync("CREATE TABLE IF NOT EXISTS `Invoices` (`Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY, `PatientName` varchar(500) NULL, `Description` varchar(500) NULL, `Amount` decimal(18,2) NULL, `PaymentStatus` varchar(500) NULL, `IssuedAt` datetime(6) NULL) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4");
    await db.Database.ExecuteSqlRawAsync("CREATE TABLE IF NOT EXISTS `Prescriptions` (`Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY, `PatientName` varchar(500) NULL, `DoctorName` varchar(500) NULL, `Medication` varchar(500) NULL, `Dosage` varchar(500) NULL, `Instructions` varchar(500) NULL, `PrescribedAt` datetime(6) NULL) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4");
    await db.Database.ExecuteSqlRawAsync("CREATE TABLE IF NOT EXISTS `LabTests` (`Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY, `PatientName` varchar(500) NULL, `TestName` varchar(500) NULL, `Status` varchar(500) NULL, `ResultSummary` varchar(500) NULL, `OrderedAt` datetime(6) NULL) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4");
    await db.Database.ExecuteSqlRawAsync("CREATE TABLE IF NOT EXISTS `Admissions` (`Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY, `PatientName` varchar(500) NULL, `RoomNumber` varchar(500) NULL, `Reason` varchar(500) NULL, `AdmittedAt` datetime(6) NULL, `DischargedAt` datetime(6) NULL) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4");
    if (!await db.Doctors.AnyAsync()) { db.Doctors.AddRange(new Doctor { Name = "Dr. Anjali Shah", Specialization = "General Medicine", Phone = "9876543210" }, new Doctor { Name = "Dr. Raj Mehta", Specialization = "Cardiology", Phone = "9876543211" }, new Doctor { Name = "Dr. Priya Patel", Specialization = "Dermatology", Phone = "9876543212" }); await db.SaveChangesAsync(); }
} catch (Exception ex) { app.Logger.LogCritical(ex, "MySQL setup failed. Start XAMPP MySQL and check the connection string in appsettings.json."); throw; }
if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/Home/Error"); app.UseHsts(); }
app.UseHttpsRedirection(); app.UseStaticFiles(); app.UseRouting(); app.UseAuthentication(); app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"); app.Run();
