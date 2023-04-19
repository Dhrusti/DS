--Created new DBScript_V1
--Added on 14-06-22 at 04:30 PM by Tanmay Sadamast—--------Start—----

-- Scaffolding Command
--Scaffold-DbContext "Server=.;user Id=sa;password=123;Database=HIMSUserDB;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -t PatientDetails, PatientsymptomDetails, RoleMst, SymptomMst, UserMst, AilmentImageDetails, DoctorDetails -Force

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'UserMst')
BEGIN 
	Create table dbo.UserMst (
	UserId int NOT NULL PRIMARY KEY IDENTITY(1,1),
	RoleId int  NOT NULL,
	UserName nvarchar(50) NOT NULL,
	Password nvarchar(MAX) NOT NULL,
	IsActive bit NOT NULL,
	IsDeleted bit NOT NULL,
	CreateBy int NOT NULL,
	UpdatedBy int NOT NULL,
	CreateAt datetime NOT NULL,
	UpdatedAt datetime NOT NULL,
	);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'RoleMst')
BEGIN 	
	Create table dbo.RoleMst (
	RoleId int NOT NULL PRIMARY KEY IDENTITY(1,1),
	RoleName nvarchar(50) NOT NULL,
	IsActive bit NOT NULL,
	IsDeleted bit NOT NULL,
	CreateBy int NOT NULL,
	UpdatedBy int NOT NULL,
	CreateAt datetime NOT NULL,
	UpdatedAt datetime NOT NULL,
	);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'PatientDetails')
BEGIN 
	Create Table dbo.PatientDetails
	(
		PatientId int Identity PRIMARY KEY NOT NULL,
		PatientCode nvarchar(255) NOT NULL,
		UserId int not null,
		FirstName nvarchar(255) not null,
		LastName nvarchar(255) not null,
		Gender varchar(10) not null,
		DOB datetime null,
		Email nvarchar(255) null,
		MobileNo varchar(10) not null,
		AlternateMobileNo varchar(10) null,
		Address nvarchar(max) null,
		StreetLandMark nvarchar(max) null,
		State nvarchar(255) null,
		City nvarchar(255) null,
		Pincode varchar(10) null,
		Image nvarchar(MAX) null,
		ImagePath nvarchar(MAX) null,
		IsActive bit not null,
		IsDelete bit not null,
		CreatedBy int not null,
		UpdateBy int not null,
		CreatedAt datetime not null,
		UpdatedAt datetime not null
	)
	PRINT 'Table Created'
END
ELSE
BEGIN
	PRINT 'Table ALready Exists'
END

INSERT INTO RoleMst VALUES('Doctor', 1, 0 , 1, 1, '2022-06-17 17:00:00.920', '2022-06-17 17:00:00.920');
INSERT INTO RoleMst VALUES('Patient', 1, 0 , 1, 1, '2022-06-17 17:01:00.920', '2022-06-17 17:01:00.920');
INSERT INTO RoleMst VALUES('ClinicalStaff', 1, 0 , 1, 1, '2022-06-17 17:02:00.920', '2022-06-17 17:02:00.920');

INSERT INTO UserMst VALUES(1, 'ajay.zala@archesoftronix.com', 'AJA2807' , 1, 0, 1, 1,  '2022-06-20 16:55:57.593', '2022-06-20 16:55:57.593');
INSERT INTO UserMst VALUES(3, 'yuvaraj.pawar@archesoftronix.com', 'YUV2106' , 1, 0, 1, 1,  '2022-06-17 17:00:00.920', '2022-06-17 17:00:00.920');


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'SymptomMst')
BEGIN 
	Create Table dbo.SymptomMst
	(
		SymptomId int Identity PRIMARY KEY NOT NULL,
		SymptomName nvarchar(255) not null,
		IsActive bit not null,
		IsDelete bit not null,
		CreatedBy int not null,
		UpdateBy int not null,
		CreatedAt datetime not null,
		UpdatedAt datetime not null
	)
	PRINT 'Table Created'
END
ELSE
BEGIN
	PRINT 'Table ALready Exists'
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'PatientSymptomDetails')
BEGIN 
	Create Table dbo.PatientSymptomDetails
	(
		PatientSymptomId int Identity PRIMARY KEY NOT NULL,
		SymptomId int NOT NULL,
		AilmentId int NOT NULL,
		IsActive bit not null,
		IsDelete bit not null,
		CreatedBy int not null,
		UpdateBy int not null,
		CreatedAt datetime not null,
		UpdatedAt datetime not null
	)
	PRINT 'Table Created'
END
ELSE
BEGIN
	PRINT 'Table ALready Exists'
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'AilmentImageDetails')
BEGIN 
	Create Table dbo.AilmentImageDetails
	(
		AilmentImageId int Identity PRIMARY KEY NOT NULL,
		AilmentId int NOT NULL,
		Image nvarchar(MAX) not null,
		IsDelete bit not null,
		CreatedBy int not null,
		UpdateBy int not null,
		CreatedAt datetime not null,
		UpdatedAt datetime not null
	)
	PRINT 'Table Created'
END
ELSE
BEGIN
	PRINT 'Table ALready Exists'
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'DoctorDetails')
BEGIN 
	Create Table dbo.DoctorDetails
	(
		DoctorId int Identity PRIMARY KEY NOT NULL,
		FirstName nvarchar(255) not null,
		LastName nvarchar(255) not null,
		Email nvarchar(255) null,
		IsActive bit not null,
		IsDelete bit not null,
		CreatedBy int not null,
		UpdateBy int not null,
		CreatedAt datetime not null,
		UpdatedAt datetime not null
	)
	PRINT 'Table Created'
END
ELSE
BEGIN
	PRINT 'Table ALready Exists'
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'AilmentMst')
BEGIN 
	Create Table dbo.AilmentMst
	(
		AilmentId int Identity PRIMARY KEY NOT NULL,
		PatientId int not null,
		IsActive bit not null,
		IsDelete bit not null,
		CreatedBy int not null,
		UpdateBy int not null,
		CreatedAt datetime not null,
		UpdatedAt datetime not null
	)
	PRINT 'Table Created'
END
ELSE
BEGIN
	PRINT 'Table ALready Exists'
END


--Executed on Local at 14-06-22 at 04:30 PM by Tanmay Sadamast
--Added on 06-06-22 at 11:00 AM by Tanmay Sadamast—--------End—----