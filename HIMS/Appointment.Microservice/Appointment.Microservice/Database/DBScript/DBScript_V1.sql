--Created new DBScript_V1
--Added on 14-06-22 at 04:30 PM by Tanmay Sadamast—--------Start—----

-- Scaffolding Command
--Scaffold-DbContext "Server=.;user Id=sa;password=123;Database=HIMSAppointmentDB;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -t AppointmentMst,MedicineMst, ProblemMst,DiagnosisMst,PatientDiagnosisDetails,PatientProblemDetails,PatientMedicationDetails -Force

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'AppointmentMst')
BEGIN 
	Create Table dbo.AppointmentMst
	(
		AppointmentId int Identity PRIMARY KEY NOT NULL,
		AilmentId int not null,
		DoctorId int not null,
		AppointmentDate date not null,
		AppointmentTime nvarchar(30) not null,
		Status nvarchar(50) not null,
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


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'MedicineMst')
BEGIN 
	Create Table dbo.MedicineMst
	(
		MedicineId int Identity PRIMARY KEY NOT NULL,
		MedicineName nvarchar(MAX) not null,
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

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'ProblemMst')
BEGIN 
	Create Table dbo.ProblemMst
	(
		ProblemId int Identity PRIMARY KEY NOT NULL,
		ProblemName nvarchar(MAX) not null,
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

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'DiagnosisMst')
BEGIN 
	Create Table dbo.DiagnosisMst
	(
		DiagnosisId int Identity PRIMARY KEY NOT NULL,
		DiagnosisName nvarchar(MAX) not null,
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

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'PatientDiagnosisDetails')
BEGIN 
	Create Table dbo.PatientDiagnosisDetails
	(
		PatientDiagnosisId int Identity PRIMARY KEY NOT NULL,
		AilmentId int NOT NULL,
		DiagnosisIds nvarchar(100) NOT NULL,
		Note nvarchar(MAX),
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

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'PatientProblemDetails')
BEGIN 
	Create Table dbo.PatientProblemDetails
	(
		PatientProblemId int Identity PRIMARY KEY NOT NULL,
		AilmentId int NOT NULL,
		ProblemIds nvarchar(100) NOT NULL,
		Note nvarchar(MAX),
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

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'PatientMedicationDetails')
BEGIN 
	Create Table dbo.PatientMedicationDetails
	(
		PatientMedicationId int Identity PRIMARY KEY NOT NULL,
		AilmentId int NOT NULL,
		MedicineId int NOT NULL,
		Time nvarchar(30) NOT NULL,
		Schedule nvarchar(30) NOT NULL,
		Note nvarchar(MAX),
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
