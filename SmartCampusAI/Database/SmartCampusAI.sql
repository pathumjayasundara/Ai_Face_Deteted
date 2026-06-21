-- ============================================================
-- SmartCampusAI - Full Database Script
-- SQL Server 2019 / SQL Server 2022 Compatible
-- ============================================================

USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'SmartCampusAI')
BEGIN
    ALTER DATABASE SmartCampusAI SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE SmartCampusAI;
END
GO

CREATE DATABASE SmartCampusAI;
GO

USE SmartCampusAI;
GO

-- ============================================================
-- TABLE: Users
-- ============================================================
CREATE TABLE Users (
    UserID      INT IDENTITY(1,1) PRIMARY KEY,
    Username    NVARCHAR(100) NOT NULL UNIQUE,
    Password    NVARCHAR(255) NOT NULL,
    Role        NVARCHAR(50)  NOT NULL DEFAULT 'User',
    CreatedAt   DATETIME      NOT NULL DEFAULT GETDATE()
);
GO

-- ============================================================
-- TABLE: Students
-- ============================================================
CREATE TABLE Students (
    StudentID   INT IDENTITY(1,1) PRIMARY KEY,
    FullName    NVARCHAR(200) NOT NULL,
    Age         INT           NOT NULL,
    Gender      NVARCHAR(20)  NOT NULL,
    Phone       NVARCHAR(20)  NOT NULL,
    Address     NVARCHAR(500) NOT NULL,
    CreatedAt   DATETIME      NOT NULL DEFAULT GETDATE()
);
GO

-- ============================================================
-- TABLE: FaceData
-- ============================================================
CREATE TABLE FaceData (
    FaceID          INT IDENTITY(1,1) PRIMARY KEY,
    StudentID       INT NOT NULL,
    FaceEncoding    NVARCHAR(MAX) NOT NULL,       -- JSON array of float values
    SampleImagePath NVARCHAR(500) NOT NULL,
    CapturedAt      DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_FaceData_Students FOREIGN KEY (StudentID)
        REFERENCES Students(StudentID) ON DELETE CASCADE
);
GO

-- ============================================================
-- TABLE: Attendance
-- ============================================================
CREATE TABLE Attendance (
    AttendanceID    INT IDENTITY(1,1) PRIMARY KEY,
    StudentID       INT           NOT NULL,
    AttendanceDate  DATE          NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    AttendanceTime  TIME          NOT NULL DEFAULT CAST(GETDATE() AS TIME),
    Status          NVARCHAR(20)  NOT NULL DEFAULT 'Present',
    MarkedBy        NVARCHAR(50)  NOT NULL DEFAULT 'FaceRecognition',
    ConfidenceScore FLOAT         NULL,
    CONSTRAINT FK_Attendance_Students FOREIGN KEY (StudentID)
        REFERENCES Students(StudentID) ON DELETE CASCADE,
    CONSTRAINT UQ_Attendance_StudentDate UNIQUE (StudentID, AttendanceDate)
);
GO

-- ============================================================
-- INDEXES
-- ============================================================
CREATE INDEX IX_FaceData_StudentID    ON FaceData(StudentID);
CREATE INDEX IX_Attendance_StudentID  ON Attendance(StudentID);
CREATE INDEX IX_Attendance_Date       ON Attendance(AttendanceDate);
GO

-- ============================================================
-- SEED DATA: Users
-- ============================================================
INSERT INTO Users (Username, Password, Role) VALUES
('admin',   'Admin@123',  'Admin'),
('teacher', 'Teacher@123','User'),
('staff',   'Staff@123',  'User');
GO

-- ============================================================
-- SEED DATA: Students
-- ============================================================
INSERT INTO Students (FullName, Age, Gender, Phone, Address) VALUES
('Alice Johnson',   20, 'Female', '0771234567', '12 Main Street, Colombo 01'),
('Bob Smith',       22, 'Male',   '0779876543', '45 Lake Road, Kandy'),
('Carol Williams',  21, 'Female', '0771112233', '78 Hill Avenue, Galle'),
('David Brown',     23, 'Male',   '0774445566', '90 Ocean Drive, Negombo'),
('Eva Martinez',    20, 'Female', '0777778899', '34 Park Lane, Matara');
GO

-- ============================================================
-- STORED PROCEDURE: GetAttendanceSummary
-- ============================================================
CREATE PROCEDURE sp_GetAttendanceSummary
    @StartDate DATE,
    @EndDate   DATE
AS
BEGIN
    SELECT
        s.StudentID,
        s.FullName,
        COUNT(a.AttendanceID)                        AS TotalPresent,
        CAST(COUNT(a.AttendanceID) AS FLOAT) /
            NULLIF(DATEDIFF(DAY, @StartDate, @EndDate) + 1, 0) * 100 AS AttendancePercent
    FROM Students s
    LEFT JOIN Attendance a ON s.StudentID = a.StudentID
        AND a.AttendanceDate BETWEEN @StartDate AND @EndDate
        AND a.Status = 'Present'
    GROUP BY s.StudentID, s.FullName
    ORDER BY s.FullName;
END
GO

-- ============================================================
-- VIEW: TodayAttendance
-- ============================================================
CREATE VIEW vw_TodayAttendance AS
    SELECT
        a.AttendanceID,
        s.StudentID,
        s.FullName,
        a.AttendanceDate,
        a.AttendanceTime,
        a.Status,
        a.ConfidenceScore,
        a.MarkedBy
    FROM Attendance a
    INNER JOIN Students s ON a.StudentID = s.StudentID
    WHERE a.AttendanceDate = CAST(GETDATE() AS DATE);
GO

PRINT 'SmartCampusAI database created successfully.';
GO
