# Hospital Appointment System — XAMPP MySQL Edition

## Requirements
- Windows + .NET 8 SDK / Visual Studio 2022
- XAMPP MySQL running

## Setup
1. Start MySQL in XAMPP Control Panel (Apache optional; only needed for phpMyAdmin).
2. In `appsettings.json`, set `HospitalConnection` with the correct MySQL port (usually 3306, sometimes 3307), username and password.
3. Run `dotnet restore` then `dotnet run` from the project directory.
4. The app attempts to create `hospital_appointment_db` and its tables automatically. MySQL user must have CREATE DATABASE permission. Alternatively import `database_setup.sql` through phpMyAdmin.
5. Open the displayed localhost URL, select Register, create your account, and sign in.

## Database tables
`Users` (accounts with ASP.NET password hashes), `Doctors`, `Patients`, `Appointments`. All CRUD records persist in MySQL.

## Notes
- Existing `hospital.db` SQLite file is not used or migrated; this starts with a fresh MySQL database.
- This is a college/demo application, not a production medical system. Add roles, audit logs, backups, stronger account protections, and privacy safeguards before using real patient information.
- If you get 'Unknown database', confirm the MySQL user can create databases or run the SQL script.
- If 'Connection refused', start MySQL and check the port. If 'Access denied', fix credentials.
