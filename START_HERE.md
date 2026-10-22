# CarePoint Hospital Enterprise Edition

Expanded ASP.NET Core MVC (.NET 8) project with XAMPP MySQL.

## Modules
Login/registration, doctors, patients, appointments, departments, rooms, staff, medicines, invoices, prescriptions, laboratory tests, admissions, and reports with appointment CSV export.

## Run
1. Extract ZIP to a NEW EMPTY folder (do not nest or merge with the old project).
2. Start XAMPP MySQL. Verify connection in appsettings.json (default port 3306, root, blank password).
3. Open a terminal in the folder containing HospitalAppointmentSystem.csproj.
4. Run `dotnet restore` then `dotnet run`.
5. Register a user and log in.

Database is created automatically when the MySQL account has CREATE permission. Additional module tables are created automatically on existing installations. Existing tables are retained.

## Notes
This is an academic demo, not production-ready medical software. The new modules use simple text-based patient and doctor fields; they are not linked by foreign keys. Do not store real patient information in this demo. Back up your database before upgrades.

Build could not be executed in this environment because .NET SDK is unavailable.
