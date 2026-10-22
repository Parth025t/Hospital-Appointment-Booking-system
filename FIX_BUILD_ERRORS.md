# CS1597 build-error correction

Corrected six invalid C# auto-properties in Models/Admission.cs, Models/Invoice.cs, Models/Medicine.cs, and Models/Room.cs. C# auto-properties use `{ get; set; }` with no semicolon after `}`.

## Run in Windows PowerShell
1. Extract this ZIP to a **new empty folder**, not on top of the previous project.
2. Start **MySQL** in XAMPP Control Panel.
3. Open PowerShell in the folder containing `HospitalAppointmentSystem.csproj`.
4. Run:

   dotnet clean
   dotnet restore
   dotnet run

## Database
Your MySQL server must be running and match `appsettings.json` (port and password). The app attempts to create the database and missing tables.

## Test scope
The six reported syntax errors have been corrected and checked. A full `dotnet build` and live XAMPP test could not be performed in this packaging environment (no .NET SDK/MySQL server).
