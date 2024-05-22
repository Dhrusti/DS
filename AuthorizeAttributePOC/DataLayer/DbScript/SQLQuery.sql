--=============================================================================================================--
--========================================== ActivityLogMst ===================================================--
--=============================================================================================================--


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'ActivityLogMst')
BEGIN 
CREATE TABLE dbo.ActivityLogMst (
				Id int PRIMARY KEY IDENTITY(1,1),
				ExecutionDate Datetime not null,
				APIURL nvarchar(MAX),
				MethodType nvarchar(10),
				Request nvarchar(MAX),
				Response nvarchar(MAX)
				);
	PRINT 'ActivityLogMst Table Created' 
END
ELSE
BEGIN 
	PRINT 'ActivityLogMst Table Already Exist' 
END
GO


--=============================================================================================================--
--========================================== ExceptionLogMst ==================================================--
--=============================================================================================================--


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'ExceptionLogMst')
BEGIN 
CREATE TABLE dbo.ExceptionLogMst (
				Id int PRIMARY KEY IDENTITY(1,1),
				ExecutionDate Datetime not null,
				APIURL nvarchar(MAX),
				MethodType nvarchar(10),
				Message nvarchar(MAX),
				StackTrace nvarchar(MAX)
				);
	PRINT 'ExceptionLogMst Table Created' 
END
ELSE
BEGIN 
	PRINT 'ExceptionLogMst Table Already Exist' 
END
GO

--=============================================================================================================--
--============================================== UserMst ======================================================--
--=============================================================================================================--

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'UserMst')
BEGIN 
CREATE TABLE dbo.UserMst (
				Id int PRIMARY KEY IDENTITY(1,1),
				FirstName nvarchar(100) not null,
				LastName nvarchar(100) not null,
				Email nvarchar(MAX) not null,
				Password nvarchar(50) not null,
				UserType nvarchar(100) not null,
				UserStatusId int not null,
				Address nvarchar(MAX) not null,
				DepartmentId int not null,
				DesignationId int not null,
				ContactNo nvarchar(20) not null,
				IsActive bit not null,
				IsDelete bit not null,
				CreatedBy int not null,
				UpdatedBy int not null,
				CreatedDate Datetime not null,
				UpdatedDate Datetime not null
				);
	PRINT 'UserMst Table Created' 
END
ELSE
BEGIN 
	PRINT 'UserMst Table Already Exist' 
END
GO

Insert into UserMst Values('Dhrusti', 'Suthar', 'dhrusti@yopmail.com', 'Dhrusti@123', 'Admin', 1, 'India', 1, 1, '8788890098', 1, 0, 1, 1, GETDATE(), GETDATE());

--=============================================================================================================--
--========================================== UserStatusMst ====================================================--
--=============================================================================================================--


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'UserStatusMst')
BEGIN 
	CREATE TABLE dbo.UserStatusMst(
			Id int identity(1,1) primary key NOT NULL,
			IsActive bit NOT NULL,
			IsDelete bit NOT NULL,
			CreatedBy int NOT NULL,
			UpdatedBy int NOT NULL,
			CreatedDate datetime NOT NULL,
			UpdatedDate datetime NOT NULL,
			UserStatus nvarchar(20) NOT NULL
	        );
PRINT 'UserStatusMst Table Created' 
END
ELSE
BEGIN 
	PRINT 'UserStatusMst Table Already Exist' 
END


SET IDENTITY_INSERT [dbo].[UserStatusMst] ON 
GO
INSERT [dbo].[UserStatusMst] ([Id], [IsActive], [IsDelete], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate], [UserStatus]) VALUES (1, 1, 0, 1, 1, GETDATE(), GETDATE(), N'Active')
GO
INSERT [dbo].[UserStatusMst] ([Id], [IsActive], [IsDelete], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate], [UserStatus]) VALUES (2, 1, 0, 1, 1, GETDATE(), GETDATE(), N'InActive')
GO
INSERT [dbo].[UserStatusMst] ([Id], [IsActive], [IsDelete], [CreatedBy], [UpdatedBy], [CreatedDate], [UpdatedDate], [UserStatus]) VALUES (3, 1, 0, 1, 1, GETDATE(), GETDATE(), N'Suspend')
GO
SET IDENTITY_INSERT [dbo].[UserStatusMst] OFF
GO

--=============================================================================================================--
--========================================== UserTokenMst =====================================================--
--=============================================================================================================--


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'UserTokenMst')
BEGIN 
	CREATE TABLE dbo.UserTokenMst(
		    Id int identity(1,1) primary key NOT NULL,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			UserId int NOT NULL,
			ForMobile bit NOT NULL,
			Token nvarchar(max) NOT NULL,
			TokenExpiryTime datetime NOT NULL,
			RefreshToken nvarchar(max) NOT NULL,
			RefreshTokenExpiryTime datetime NOT NULL,
			);
	PRINT 'UserTokenMst Table Created' 
END
ELSE
BEGIN 
	PRINT 'UserTokenMst Table Already Exist' 
END