-- ============================================================
-- SmartCRUD Management System - Complete Database Script
-- SQL Server 2019/2022 Compatible
-- Run this entire script in SSMS before starting the app
-- ============================================================

USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'SmartCRUDDB')
BEGIN
    ALTER DATABASE SmartCRUDDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE SmartCRUDDB;
END
GO

CREATE DATABASE SmartCRUDDB
    COLLATE SQL_Latin1_General_CP1_CI_AS;
GO

USE SmartCRUDDB;
GO

-- ============================================================
-- TABLE: Users
-- ============================================================
CREATE TABLE Users
(
    UserID    INT           IDENTITY(1,1) PRIMARY KEY,
    Username  NVARCHAR(100) NOT NULL,
    Password  NVARCHAR(255) NOT NULL,
    Role      NVARCHAR(50)  NOT NULL DEFAULT 'User',
    IsActive  BIT           NOT NULL DEFAULT 1,
    CreatedAt DATETIME2     NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT UQ_Users_Username UNIQUE (Username)
);
GO

-- ============================================================
-- TABLE: Students
-- ============================================================
CREATE TABLE Students
(
    StudentID  INT           IDENTITY(1,1) PRIMARY KEY,
    FullName   NVARCHAR(200) NOT NULL,
    Age        INT           NOT NULL,
    Gender     NVARCHAR(20)  NOT NULL,
    Phone      NVARCHAR(20)  NOT NULL,
    Email      NVARCHAR(150) NOT NULL DEFAULT '',
    Address    NVARCHAR(500) NOT NULL,
    IsActive   BIT           NOT NULL DEFAULT 1,
    CreatedAt  DATETIME2     NOT NULL DEFAULT SYSDATETIME(),
    UpdatedAt  DATETIME2     NOT NULL DEFAULT SYSDATETIME()
);
GO

-- ============================================================
-- INDEXES
-- ============================================================
CREATE NONCLUSTERED INDEX IX_Students_FullName ON Students (FullName);
CREATE NONCLUSTERED INDEX IX_Students_IsActive ON Students (IsActive);
GO

-- ============================================================
-- SEED DATA: Users
-- ============================================================
INSERT INTO Users (Username, Password, Role) VALUES
('admin',   'Admin@123',   'Admin'),
('manager', 'Manager@123', 'Manager'),
('user',    'User@123',    'User');
GO

-- ============================================================
-- SEED DATA: Students
-- ============================================================
INSERT INTO Students (FullName, Age, Gender, Phone, Email, Address) VALUES
('Alice Johnson',   20, 'Female', '0771234567', 'alice@email.com',   '12 Main Street, Colombo 01'),
('Bob Smith',       22, 'Male',   '0779876543', 'bob@email.com',     '45 Lake Road, Kandy'),
('Carol Williams',  21, 'Female', '0771112233', 'carol@email.com',   '78 Hill Avenue, Galle'),
('David Brown',     23, 'Male',   '0774445566', 'david@email.com',   '90 Ocean Drive, Negombo'),
('Eva Martinez',    20, 'Female', '0777778899', 'eva@email.com',     '34 Park Lane, Matara'),
('Frank Wilson',    24, 'Male',   '0770001122', 'frank@email.com',   '56 River View, Colombo 03'),
('Grace Lee',       19, 'Female', '0773334455', 'grace@email.com',   '23 Garden Path, Kurunegala'),
('Henry Taylor',    25, 'Male',   '0776667788', 'henry@email.com',   '67 Temple Road, Anuradhapura');
GO

-- ============================================================
-- STORED PROCEDURES
-- ============================================================
CREATE PROCEDURE sp_SearchStudents
    @Keyword NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT StudentID, FullName, Age, Gender, Phone, Email, Address, CreatedAt
    FROM   Students
    WHERE  IsActive = 1
      AND (FullName   LIKE '%' + @Keyword + '%'
        OR Phone      LIKE '%' + @Keyword + '%'
        OR Email      LIKE '%' + @Keyword + '%'
        OR CAST(StudentID AS NVARCHAR) LIKE '%' + @Keyword + '%')
    ORDER BY FullName;
END
GO

-- ============================================================
-- VIEWS
-- ============================================================
CREATE VIEW vw_ActiveStudents AS
    SELECT StudentID, FullName, Age, Gender, Phone, Email, Address, CreatedAt
    FROM   Students
    WHERE  IsActive = 1;
GO

PRINT 'SmartCRUDDB created successfully.';
PRINT 'Default logins: admin/Admin@123  |  manager/Manager@123  |  user/User@123';
GO
