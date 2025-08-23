CREATE DATABASE Diplomna21180105
COLLATE Cyrillic_General_CI_AI;
GO

USE Diplomna21180105;
GO

CREATE SCHEMA [21180105];
GO


CREATE TABLE [21180105].[Users]
(
    UserID INT IDENTITY PRIMARY KEY,
    Email NVARCHAR(255) COLLATE Cyrillic_General_CI_AI NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) COLLATE Cyrillic_General_CI_AI NOT NULL,
    Role NVARCHAR(50) COLLATE Cyrillic_General_CI_AI NOT NULL,
    CreatedAt_21180105 DATETIME NOT NULL DEFAULT SYSDATETIME()
);
GO


CREATE TABLE [21180105].[Hotels]
(
    HotelID INT IDENTITY PRIMARY KEY,
    OwnerID INT NOT NULL,
    Name NVARCHAR(100) COLLATE Cyrillic_General_CI_AI NOT NULL,
    Location NVARCHAR(200) COLLATE Cyrillic_General_CI_AI,
    CreatedAt_21180105 DATETIME NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT FK_Hotels_Users FOREIGN KEY (OwnerID) REFERENCES [21180105].[Users](UserID)
);
GO


CREATE TABLE [21180105].[Rooms]
(
    RoomID INT IDENTITY PRIMARY KEY,
    HotelID INT NOT NULL,
    RoomNumber NVARCHAR(10) COLLATE Cyrillic_General_CI_AI NOT NULL,
    Capacity INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    RoomType NVARCHAR(50) COLLATE Cyrillic_General_CI_AI NOT NULL,
    IsAvailable BIT NOT NULL DEFAULT 1,
    CreatedAt_21180105 DATETIME NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT FK_Rooms_Hotels FOREIGN KEY (HotelID) REFERENCES [21180105].[Hotels](HotelID)
);
GO


CREATE TABLE [21180105].[Reservations]
(
    ReservationID INT IDENTITY PRIMARY KEY,
    UserID INT NOT NULL,
    RoomID INT NOT NULL,
    CheckInDate DATE NOT NULL,
    CheckOutDate DATE NOT NULL,
    CreatedAt_21180105 DATETIME NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT FK_Reservations_Users FOREIGN KEY (UserID) REFERENCES [21180105].[Users](UserID),
    CONSTRAINT FK_Reservations_Rooms FOREIGN KEY (RoomID) REFERENCES [21180105].[Rooms](RoomID)
);
GO

CREATE TABLE [21180105].[HotelImages]
(
    HotelImageID INT IDENTITY PRIMARY KEY,
    HotelID INT NOT NULL,
    ImageUrl NVARCHAR(500) COLLATE Cyrillic_General_CI_AI NOT NULL,
    CreatedAt_21180105 DATETIME DEFAULT SYSDATETIME(),
    CONSTRAINT FK_HotelImages_Hotels FOREIGN KEY (HotelID) REFERENCES [21180105].[Hotels](HotelID)
);
GO

CREATE TABLE [21180105].[RoomImages]
(
    RoomImageID INT IDENTITY PRIMARY KEY,
    RoomID INT NOT NULL,
    ImageUrl NVARCHAR(500) COLLATE Cyrillic_General_CI_AI NOT NULL,
    CreatedAt_21180105 DATETIME DEFAULT SYSDATETIME(),
    CONSTRAINT FK_RoomImages_Rooms FOREIGN KEY (RoomID) REFERENCES [21180105].[Rooms](RoomID)
);
GO



CREATE TABLE [21180105].[log_21180105]
(
    LogID INT IDENTITY PRIMARY KEY,
    TableName NVARCHAR(100) COLLATE Cyrillic_General_CI_AI,
    OperationType NVARCHAR(10) COLLATE Cyrillic_General_CI_AI,
    OperationDateTime DATETIME DEFAULT GETDATE(),
    UserID INT NULL,
    FOREIGN KEY (UserID) REFERENCES [21180105].[Users](UserID)
);
GO


CREATE OR ALTER TRIGGER trg_Users_Log
ON [21180105].[Users]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    UPDATE [21180105].[Users]
    SET CreatedAt_21180105 = SYSDATETIME()
    WHERE UserID IN (SELECT UserID
    FROM inserted);

    INSERT INTO [21180105].[log_21180105]
        (TableName, OperationType, OperationDateTime, UserID)
    SELECT
        'Users',
        CASE WHEN EXISTS (SELECT *
        FROM deleted) THEN 'UPDATE' ELSE 'INSERT' END,
        SYSDATETIME(),
        COALESCE(CAST(SESSION_CONTEXT(N'CurrentUserID') AS INT), 1)
    FROM inserted;

    INSERT INTO [21180105].[log_21180105]
        (TableName, OperationType, OperationDateTime, UserID)
    SELECT
        'Users',
        'DELETE',
        SYSDATETIME(),
        COALESCE(CAST(SESSION_CONTEXT(N'CurrentUserID') AS INT), 1)
    FROM deleted;
END;
GO


CREATE OR ALTER TRIGGER trg_Hotels_Log
ON [21180105].[Hotels]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    UPDATE [21180105].[Hotels]
    SET CreatedAt_21180105 = SYSDATETIME()
    WHERE HotelID IN (SELECT HotelID
    FROM inserted);

    INSERT INTO [21180105].[log_21180105]
        (TableName, OperationType, OperationDateTime, UserID)
    SELECT
        'Hotels',
        CASE WHEN EXISTS (SELECT *
        FROM deleted) THEN 'UPDATE' ELSE 'INSERT' END,
        SYSDATETIME(),
        COALESCE(CAST(SESSION_CONTEXT(N'CurrentUserID') AS INT), 1)
    FROM inserted;

    INSERT INTO [21180105].[log_21180105]
        (TableName, OperationType, OperationDateTime, UserID)
    SELECT
        'Hotels',
        'DELETE',
        SYSDATETIME(),
        COALESCE(CAST(SESSION_CONTEXT(N'CurrentUserID') AS INT), 1)
    FROM deleted;
END;
GO

CREATE OR ALTER TRIGGER trg_Rooms_Log
ON [21180105].[Rooms]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    UPDATE [21180105].[Rooms]
    SET CreatedAt_21180105 = SYSDATETIME()
    WHERE RoomID IN (SELECT RoomID
    FROM inserted);

    INSERT INTO [21180105].[log_21180105]
        (TableName, OperationType, OperationDateTime, UserID)
    SELECT
        'Rooms',
        CASE WHEN EXISTS (SELECT *
        FROM deleted) THEN 'UPDATE' ELSE 'INSERT' END,
        SYSDATETIME(),
        COALESCE(CAST(SESSION_CONTEXT(N'CurrentUserID') AS INT), 1)
    FROM inserted;

    INSERT INTO [21180105].[log_21180105]
        (TableName, OperationType, OperationDateTime, UserID)
    SELECT
        'Rooms',
        'DELETE',
        SYSDATETIME(),
        COALESCE(CAST(SESSION_CONTEXT(N'CurrentUserID') AS INT), 1)
    FROM deleted;
END;
GO

CREATE OR ALTER TRIGGER trg_Rooms_Log
ON [21180105].[Rooms]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    UPDATE [21180105].[Rooms]
    SET CreatedAt_21180105 = SYSDATETIME()
    WHERE RoomID IN (SELECT RoomID
    FROM inserted);

    INSERT INTO [21180105].[log_21180105]
        (TableName, OperationType, OperationDateTime, UserID)
    SELECT
        'Rooms',
        CASE WHEN EXISTS (SELECT *
        FROM deleted) THEN 'UPDATE' ELSE 'INSERT' END,
        SYSDATETIME(),
        COALESCE(CAST(SESSION_CONTEXT(N'CurrentUserID') AS INT), 1)
    FROM inserted;

    INSERT INTO [21180105].[log_21180105]
        (TableName, OperationType, OperationDateTime, UserID)
    SELECT
        'Rooms',
        'DELETE',
        SYSDATETIME(),
        COALESCE(CAST(SESSION_CONTEXT(N'CurrentUserID') AS INT), 1)
    FROM deleted;
END;
GO


CREATE OR ALTER TRIGGER trg_Reservations_Log
ON [21180105].[Reservations]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    UPDATE [21180105].[Reservations]
    SET CreatedAt_21180105 = SYSDATETIME()
    WHERE ReservationID IN (SELECT ReservationID
    FROM inserted);

    INSERT INTO [21180105].[log_21180105]
        (TableName, OperationType, OperationDateTime, UserID)
    SELECT
        'Reservations',
        CASE WHEN EXISTS (SELECT *
        FROM deleted) THEN 'UPDATE' ELSE 'INSERT' END,
        SYSDATETIME(),
        COALESCE(CAST(SESSION_CONTEXT(N'CurrentUserID') AS INT), 1)
    FROM inserted;

    INSERT INTO [21180105].[log_21180105]
        (TableName, OperationType, OperationDateTime, UserID)
    SELECT
        'Reservations',
        'DELETE',
        SYSDATETIME(),
        COALESCE(CAST(SESSION_CONTEXT(N'CurrentUserID') AS INT), 1)
    FROM deleted;
END;
GO

INSERT INTO [21180105].[Users]
    (Email, PasswordHash, Role)
VALUES
    ('admin', 'sa', 'Owner'),
    ('owner1@example.com', 'hashedpassword1', 'Owner'),
    ('user1@example.com', 'hashedpassword2', 'User');

INSERT INTO [21180105].[Hotels]
    (OwnerID, Name, Location)
VALUES
    (1, 'Хотел Рила', 'Боровец, България'),
    (1, 'Морски Бриз', 'Слънчев Бряг, България');

INSERT INTO [21180105].[Rooms]
    (HotelID, RoomNumber, Capacity, Price, RoomType, IsAvailable)
VALUES
    (1, '101', 2, 120.00, 'Стандартна', 1),
    (1, '102', 3, 150.00, 'Луксозна', 1),
    (2, '201', 2, 100.00, 'Стандартна', 1),
    (2, '202', 4, 180.00, 'Семеен апартамент', 1);

INSERT INTO [21180105].[Reservations]
    (UserID, RoomID, CheckInDate, CheckOutDate)
VALUES
    (2, 1, '2025-08-15', '2025-08-18'),
    (2, 3, '2025-09-01', '2025-09-05');
