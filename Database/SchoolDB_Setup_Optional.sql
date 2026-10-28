/*
  School Management - verify that this script matches the existing SchoolDB schema
  before running. It creates the database/tables only when they do not already exist.
  If SchoolDB already exists with these five tables, do not run the CREATE statements;
  simply use the connection string in appsettings.json.
*/
IF DB_ID(N'SchoolDB') IS NULL
BEGIN
    CREATE DATABASE SchoolDB;
END
GO
USE SchoolDB;
GO

IF OBJECT_ID(N'dbo.Departments', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Departments (
        DepartmentID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Name NVARCHAR(50) NOT NULL,
        Budget DECIMAL(18,2) NOT NULL,
        StartDate DATE NOT NULL
    );
END
GO

IF OBJECT_ID(N'dbo.Instructors', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Instructors (
        InstructorID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        FirstName NVARCHAR(50) NOT NULL,
        LastName NVARCHAR(50) NOT NULL,
        HireDate DATE NOT NULL
    );
END
GO

IF OBJECT_ID(N'dbo.Courses', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Courses (
        CourseID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Title NVARCHAR(100) NOT NULL,
        Credits INT NOT NULL,
        DepartmentID INT NOT NULL,
        CONSTRAINT FK_Courses_Departments FOREIGN KEY (DepartmentID)
            REFERENCES dbo.Departments(DepartmentID)
    );
END
GO

IF OBJECT_ID(N'dbo.Students', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Students (
        StudentID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        FirstName NVARCHAR(50) NOT NULL,
        LastName NVARCHAR(50) NOT NULL,
        EnrollmentDate DATE NOT NULL
    );
END
GO

IF OBJECT_ID(N'dbo.Enrollments', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Enrollments (
        EnrollmentID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        CourseID INT NOT NULL,
        StudentID INT NOT NULL,
        Grade DECIMAL(3,2) NULL,
        CONSTRAINT FK_Enrollments_Courses FOREIGN KEY (CourseID)
            REFERENCES dbo.Courses(CourseID),
        CONSTRAINT FK_Enrollments_Students FOREIGN KEY (StudentID)
            REFERENCES dbo.Students(StudentID)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Departments)
    INSERT INTO dbo.Departments(Name, Budget, StartDate) VALUES (N'Computer Science', 120000.00, '2023-01-01');

IF NOT EXISTS (SELECT 1 FROM dbo.Instructors)
    INSERT INTO dbo.Instructors(FirstName, LastName, HireDate) VALUES (N'John', N'Doe', '2020-08-15');

IF NOT EXISTS (SELECT 1 FROM dbo.Courses)
BEGIN
    DECLARE @DepartmentID INT = (SELECT TOP 1 DepartmentID FROM dbo.Departments ORDER BY DepartmentID);
    INSERT INTO dbo.Courses(Title, Credits, DepartmentID) VALUES (N'Introduction to Programming', 3, @DepartmentID);
END

IF NOT EXISTS (SELECT 1 FROM dbo.Students)
    INSERT INTO dbo.Students(FirstName, LastName, EnrollmentDate) VALUES (N'Jane', N'Smith', '2023-09-01');

IF NOT EXISTS (SELECT 1 FROM dbo.Enrollments)
BEGIN
    DECLARE @CourseID INT = (SELECT TOP 1 CourseID FROM dbo.Courses ORDER BY CourseID);
    DECLARE @StudentID INT = (SELECT TOP 1 StudentID FROM dbo.Students ORDER BY StudentID);
    INSERT INTO dbo.Enrollments(CourseID, StudentID, Grade) VALUES (@CourseID, @StudentID, 3.50);
END
GO
