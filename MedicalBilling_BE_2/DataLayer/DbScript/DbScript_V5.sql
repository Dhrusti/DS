--Added by NP on 24-04-23--------------------------------------START---------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'ClaimStatusMst')
BEGIN 
	Create table dbo.ClaimStatusMst(
			Id int identity(1,1) primary key,
			OrganizationId int not null,
			CompanyId int not null,
			DepartmentId int not null,
			ClaimStatusName nvarchar(50) not null,
			IsActive bit not null,
			IsDelete bit not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FileCategoryHistoryMst')
BEGIN 
	Create table dbo.FileCategoryHistoryMst(
			Id int identity(1,1) primary key,
			FileCategoryName nvarchar(100) not null,
			OrganizationId int not null,
			CompanyId int not null,
			DepartmentId int not null,
			FileFormatPath nvarchar(max) not null,
			IsActive bit not null,
			IsDelete bit not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FileHistoryMst')
BEGIN 
	Create table dbo.FileHistoryMst(
			Id int identity(1,1) primary key,
			FileCategoryId int not null,
			FileName nvarchar(100) not null,
			FileExtension nvarchar(10) not null,
			FileSize nvarchar(100) not null,
			FilePath nvarchar(max) not null,
			StartDate datetime null,
			EndDate datetime null,
			IsActive bit not null,
			IsDelete bit not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'FileDataMst')
BEGIN 
	Create table dbo.FileDataMst(
			Id int identity(1,1) primary key,
			FileCategoryHistoryId int not null,
			FileHistoryId int not null,
			PayerName nvarchar(100) not null,
			PayerCode nvarchar(50) null,
			RenderingFullName nvarchar(100) null,
			RefferingFullName nvarchar(100) null,
			PatientName nvarchar(100) not null,
			PatientCode nvarchar(50) null,
			PatientDOB datetime null,
			MedicalRecordCode nvarchar(50) null,
			EAIBCode nvarchar(50) null,
			Componant nvarchar(100) null,
			PayerPhone nvarchar(50) null,
			PolicyCode nvarchar(50) not null,
			ClaimStatus nvarchar(50) not null,
			ClaimCode nvarchar(50) null,
			DateOfService datetime null,
			ServiceCPT nvarchar(50) null,
			ServiceCode nvarchar(50) null,
			Modifier nvarchar(100) null,
			DiagnosisCode1 nvarchar(50) null,
			DiagnosisCode2 nvarchar(50) null,
			DiagnosisCode3 nvarchar(50) null,
			DiagnosisCode4 nvarchar(50) null,
			COB nvarchar(50) null,
			InsuranceAmount1 decimal(38,8) null,
			InsuranceAmount2 decimal(38,8) null,
			InsuranceAmount3 decimal(38,8) null,
			InsuranceAmount4 decimal(38,8) null,
			ChargeAmount decimal(38,8) not null,
			LineItemAmount decimal(38,8) null,
			LastBillDate datetime null,
			IsActive bit not null,
			IsDelete bit not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'PayerMst')
BEGIN 
	Create table dbo.PayerMst(
			Id int identity(1,1) primary key,
			PayerName nvarchar(100) not null,
			PayerCode nvarchar(50) null,
			Componant nvarchar(100) null,
			Address nvarchar(max) null,
			Phone nvarchar(50) null,
			Mobile nvarchar(50) null,
			Email nvarchar(50) null,
			Website nvarchar(50) null,	
			IsActive bit not null,
			IsDelete bit not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'PatientMst')
BEGIN 
	Create table dbo.PatientMst(
			Id int identity(1,1) primary key,
			OrganizationId int not null,
			CompanyId int not null,
			DepartmentId int not null,
			PayerId int not null,
			Address nvarchar(max) null,
			Phone nvarchar(50) null,
			Mobile nvarchar(50) null,
			Email nvarchar(50) null,
			RenderingFullName nvarchar(100) null,
			RefferingFullName nvarchar(100) null,
			PatientName nvarchar(100) not null,
			PatientCode nvarchar(50) null,
			PatientDOB datetime null,
			MedicalRecordCode nvarchar(50) null,
			EAIBCode nvarchar(50) null,
			IsActive bit not null,
			IsDelete bit not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'PolicyMst')
BEGIN 
	Create table dbo.PolicyMst(
			Id int identity(1,1) primary key,
			OrganizationId int not null,
			CompanyId int not null,
			DepartmentId int not null,
			PayerId int not null,
			PatientId int not null,
			PolicyCode nvarchar(50) null,
			IsActive bit not null,
			IsDelete bit not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'ClaimMst')
BEGIN 
	Create table dbo.ClaimMst(
			Id int identity(1,1) primary key,
			OrganizationId int not null,
			CompanyId int not null,
			DepartmentId int not null,
			PayerId int not null,
			PatientId int not null,
			PolicyId int not null,
			ClaimCode nvarchar(50) null,
			ClaimStatusId int not null,
			LastBillDate Datetime null,
			IsActive bit not null,
			IsDelete bit not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'ServiceMst')
BEGIN 
	Create table dbo.ServiceMst(
			Id int identity(1,1) primary key,
			DateOfService datetime null,
			ServiceCPT nvarchar(50) null,
			ServiceCode nvarchar(50) null,
			Modifier nvarchar(100) null,
			DiagnosisCode1 nvarchar(50) null,
			DiagnosisCode2 nvarchar(50) null,
			DiagnosisCode3 nvarchar(50) null,
			DiagnosisCode4 nvarchar(50) null,
			COB nvarchar(50) null,
			InsuranceAmount1 decimal(38,8) null,
			InsuranceAmount2 decimal(38,8) null,
			InsuranceAmount3 decimal(38,8) null,
			InsuranceAmount4 decimal(38,8) null,
			ChargeAmount decimal(38,8) not null,
			LineItemAmount decimal(38,8) null,
			IsActive bit not null,
			IsDelete bit not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'ActionStatusMst')
BEGIN 
	Create table dbo.ActionStatusMst(
			Id int identity(1,1) primary key,
			StatusName nvarchar(50) not null,
			IsActive bit not null,
			IsDelete bit not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END


insert into dbo.DefaultPermissions values(1,6,1,0,0,0,getdate(),getdate())
--Added by NP on 24-04-23--------------------------------------END---------------------------
--Executed on NP Local 24-04-23--------------------------------------------------------------
--Executed on SP Local 24-04-23--------------------------------------------------------------
--Executed on Dev 24-04-23-------------------------------------------------------------------

--Added by NP on 26-04-23--------------------------------------START-------------------------
update dbo.UserMst set UserName='Nikunj_SA' where id=1
update dbo.UserMst set UserName='Sonal_SA' where id=2

insert into dbo.UserMst values('Dhrusti','Suthar','Dhrusti_SA','12345','15-Apr-1998','7412367890','Dhrusti.arche@gmail.com',1,0,0,0,getdate(),getdate(),1)
insert into dbo.UserMst values('Ajay','Zala','Ajay_SA','12345','15-Jan-1991','7412367890','Ajay.arche@gmail.com',1,0,0,0,getdate(),getdate(),1)

insert into dbo.UserMst values('Nikunj','Pandya','Nikunj_A','12345','18-Mar-1993','0178756789','nikunjp.archesoftronix@gmail.com',1,0,0,0,getdate(),getdate(),2)
insert into dbo.UserMst values('Sonal','Patel','Sonal_A','12345','14-Apr-1998','3412367890','sonalarche@gmail.com',1,0,0,0,getdate(),getdate(),2)
insert into dbo.UserMst values('Dhrusti','Suthar','Dhrusti_A','12345','15-Apr-1998','7412367890','Dhrusti.arche@gmail.com',1,0,0,0,getdate(),getdate(),2)
insert into dbo.UserMst values('Ajay','Zala','Ajay_A','12345','15-Jan-1991','7412367890','Ajay.arche@gmail.com',1,0,0,0,getdate(),getdate(),2)
insert into dbo.UserMst values('Vraj','Brahmbhatt','Vraj_A','12345','15-Mar-1999','1234567890','vraj.brahmbhatt@archesoftronix.com',1,0,0,0,getdate(),getdate(),2)
insert into dbo.UserMst values('Raj','Hanani','Raj_A','12345','15-Apr-1998','1234567890','raj.hanani@archesoftronix.com',1,0,0,0,getdate(),getdate(),2)

insert into dbo.UserMst values('Nikunj','Pandya','Nikunj_DO','12345','18-Mar-1993','0178756789','nikunjp.archesoftronix@gmail.com',1,0,0,0,getdate(),getdate(),3)
insert into dbo.UserMst values('Sonal','Patel','Sonal_DO','12345','14-Apr-1998','3412367890','sonalarche@gmail.com',1,0,0,0,getdate(),getdate(),3)
insert into dbo.UserMst values('Dhrusti','Suthar','Dhrusti_DO','12345','15-Apr-1998','7412367890','Dhrusti.arche@gmail.com',1,0,0,0,getdate(),getdate(),3)
insert into dbo.UserMst values('Ajay','Zala','Ajay_DO','12345','15-Jan-1991','7412367890','Ajay.arche@gmail.com',1,0,0,0,getdate(),getdate(),3)
insert into dbo.UserMst values('Vraj','Brahmbhatt','Vraj_DO','12345','15-Mar-1999','1234567890','vraj.brahmbhatt@archesoftronix.com',1,0,0,0,getdate(),getdate(),3)
insert into dbo.UserMst values('Raj','Hanani','Raj_DO','12345','15-Apr-1998','1234567890','raj.hanani@archesoftronix.com',1,0,0,0,getdate(),getdate(),3)
--Added by NP on 26-04-23--------------------------------------END-------------------------
--Executed on SP Local 26-04-23--------------------------------------------------------------
--Executed by NP on DEV 26-04-23--------------------------------------------------------------
--Executed by NP on QA 26-04-23--------------------------------------------------------------

--Added by NP on 28-04-23--------------------------------------START-------------------------
insert into dbo.DefaultPermissions values(1,19,1,0,0,0,getdate(),getdate())
insert into dbo.DefaultPermissions values(1,20,1,0,0,0,getdate(),getdate())
insert into dbo.DefaultPermissions values(1,21,1,0,0,0,getdate(),getdate())
insert into dbo.DefaultPermissions values(1,22,1,0,0,0,getdate(),getdate())
insert into dbo.DefaultPermissions values(1,23,1,0,0,0,getdate(),getdate())

insert into dbo.DefaultPermissions values(2,19,1,0,0,0,getdate(),getdate())
insert into dbo.DefaultPermissions values(2,20,1,0,0,0,getdate(),getdate())
insert into dbo.DefaultPermissions values(2,21,1,0,0,0,getdate(),getdate())
insert into dbo.DefaultPermissions values(2,22,1,0,0,0,getdate(),getdate())
insert into dbo.DefaultPermissions values(2,23,1,0,0,0,getdate(),getdate())
--Added by NP on 28-04-23--------------------------------------END-------------------------
--Executed by NP on Local 28-04-23--------------------------------------------------------------
--Executed by NP on DEV 28-04-23--------------------------------------------------------------


--Added by NP on 02-05-23--------------------------------------START-------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'AgingMst')
BEGIN 
	Create table dbo.AgingMst(
			Id int identity(1,1) primary key,
			FileCategoryId int not null,
			FileHistoryId int not null,
			FileDataId int not null,
			PayerName nvarchar(100) not null,
			PayerCode nvarchar(50) null,
			RenderingFullName nvarchar(100) null,
			RefferingFullName nvarchar(100) null,
			PatientName nvarchar(100) not null,
			PatientCode nvarchar(50) null,
			PatientDOB datetime null,
			MedicalRecordCode nvarchar(50) null,
			EAIBCode nvarchar(50) null,
			Componant nvarchar(100) null,
			PayerPhone nvarchar(50) null,
			PolicyCode nvarchar(50) not null,
			ClaimStatus nvarchar(50) not null,
			ClaimCode nvarchar(50) null,
			DateOfService datetime null,
			ServiceCPT nvarchar(50) null,
			ServiceCode nvarchar(50) null,
			Modifier nvarchar(100) null,
			DiagnosisCode1 nvarchar(50) null,
			DiagnosisCode2 nvarchar(50) null,
			DiagnosisCode3 nvarchar(50) null,
			DiagnosisCode4 nvarchar(50) null,
			COB nvarchar(50) null,
			InsuranceAmount1 decimal(38,8) null,
			InsuranceAmount2 decimal(38,8) null,
			InsuranceAmount3 decimal(38,8) null,
			InsuranceAmount4 decimal(38,8) null,
			ChargeAmount decimal(38,8) not null,
			LineItemAmount decimal(38,8) null,
			LastBillDate datetime null,
			IsActive bit not null,
			IsDelete bit not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END

drop table dbo.ServiceMst
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'ServiceMst')
BEGIN 
	Create table dbo.ServiceMst(
			Id int identity(1,1) primary key,
			OrganizationId int not null,
			CompanyId int not null,
			DepartmentId int not null,
			PayerId int not null,
			PatientId int not null,
			PolicyId int not null,
			ClaimId int not null,
			DateOfService datetime null,
			ServiceCPT nvarchar(50) null,
			ServiceCode nvarchar(50) null,
			Modifier nvarchar(100) null,
			DiagnosisCode1 nvarchar(50) null,
			DiagnosisCode2 nvarchar(50) null,
			DiagnosisCode3 nvarchar(50) null,
			DiagnosisCode4 nvarchar(50) null,
			COB nvarchar(50) null,
			InsuranceAmount1 decimal(38,8) null,
			InsuranceAmount2 decimal(38,8) null,
			InsuranceAmount3 decimal(38,8) null,
			InsuranceAmount4 decimal(38,8) null,
			ChargeAmount decimal(38,8) not null,
			LineItemAmount decimal(38,8) null,
			IsActive bit not null,
			IsDelete bit not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END


--Added by NP on 02-05-23--------------------------------------END-------------------------
--Executed by NP on Local 02-05-23---------------------------------------------------------