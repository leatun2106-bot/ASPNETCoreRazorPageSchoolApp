# School Management - ASP.NET Core Razor Pages

## Scope
This project covers only the School Management Razor Pages exercise. It uses EF Core with SQL Server and connects to the existing `SchoolDB` database. CRUD pages are included for `Students`, `Courses`, `Instructors`, `Enrollments`, and `Departments`.

## Before running
1. Open `SchoolManagement.csproj` in Visual Studio 2022 with the .NET 8 SDK installed.
2. Confirm the actual SQL Server instance name on your computer.
3. Open `appsettings.json` and edit the `SchoolDB` connection string if needed. Current example:
   `Server=(local)\\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true`
4. Confirm that the existing `SchoolDB` has the five tables and columns described in the lab PDF.
5. Build the project and press F5.

## Important database note
`Database/SchoolDB_Setup_Optional.sql` is provided only as a reference/setup script if the database is missing or needs to be initialized. Since your database already exists, do not run this script blindly; first verify its schema and back up important data.

## Pages
- `/Students/Index`
- `/Courses/Index`
- `/Instructors/Index`
- `/Enrollments/Index`
- `/Departments/Index`

Each section has Index, Create, Edit, Details, and Delete pages.

## If the connection fails
- In SQL Server Object Explorer, check the instance name.
- Try `(localdb)\\MSSQLLocalDB` if that is the instance where SchoolDB exists.
- Ensure Windows Authentication is enabled and SQL Server is running.
- Make sure the database table/column names match the model classes.

## GitHub
Create a repository named `ASPNETCoreRazorPageSchoolApp`, then from the project folder run:

```bash
git init
git add .
git commit -m "Complete School Management Razor Pages lab"
git branch -M main
git remote add origin https://github.com/YOUR-USERNAME/ASPNETCoreRazorPageSchoolApp.git
git push -u origin main
```

Replace `YOUR-USERNAME` with your actual GitHub username. Add the real repository URL to the report after pushing.
