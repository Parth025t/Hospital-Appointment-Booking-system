# CarePoint Hospital Appointment System

A fresh ASP.NET Core MVC (.NET 8) college project using SQLite, **not MySQL**.

## Features
- Dashboard with doctor, patient and appointment counts
- Doctor management: add, edit, delete
- Patient management: add, edit, delete
- Appointment booking, editing, status updates, deletion
- Doctor double-booking check for exactly matching appointment start times
- Automatically creates a new `hospital.db` database and seeds 3 sample doctors

## Run in VS Code (Windows)
1. Install the **.NET 8 SDK** and VS Code.
2. Extract the ZIP and open the folder containing `HospitalAppointmentSystem.csproj`.
3. Open Terminal > New Terminal and run:
   ```
   dotnet restore
   dotnet run --urls "http://localhost:5100"
   ```
4. Open http://localhost:5100 in your browser.
5. Add a patient, then book an appointment.

No XAMPP, MySQL, or manual SQL import is needed.

## Notes
- This is a **new project**, not an upgrade of your old MySQL project. Existing MySQL records are not migrated.
- Database is created via `EnsureCreated()` on first run. Do not mix it with EF migrations; if you change models later, use a migration-based approach or back up/delete the SQLite file for a fresh start.
- For educational use only. No authentication, role-based access, or production medical-data protections.
- This project was generated but could not be compiled in this environment because the .NET SDK is unavailable.
