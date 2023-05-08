--Added by NP on 06-04-23--------------------------------------START---------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'OrganizationMst')
BEGIN 
	Create table dbo.OrganizationMst(
			Id int identity(1,1) primary key,
			OrganizationName nvarchar(50) not null,
			OrganizationDisplayName nvarchar(50) not null,
			[Address] nvarchar(500) null,
			Phone nvarchar(15) null,
			Mobile nvarchar(15) null,
			Email nvarchar(50) null,
			Website nvarchar(50) null,
			FaxNo nvarchar(50) null,
			ZipCode nvarchar(15) null,
			NPI nvarchar(50) null,
			BCN nvarchar(50) null,
			Sonarx nvarchar(50) null,
			TaxId nvarchar(50) null,
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
			TABLE_NAME = 'CompanyMst')
BEGIN 
	Create table dbo.CompanyMst(
			Id int identity(1,1) primary key,
			OrganizationId int not null,
			CompanyName nvarchar(50) not null,
			CompanyDisplayName nvarchar(50) not null,
			[Address] nvarchar(500) null,
			Phone nvarchar(15) null,
			Mobile nvarchar(15) null,
			Email nvarchar(50) null,
			Website nvarchar(50) null,
			FaxNo nvarchar(50) null,
			ZipCode nvarchar(15) null,
			NPI nvarchar(50) null,
			BCN nvarchar(50) null,
			Sonarx nvarchar(50) null,
			TaxId nvarchar(50) null,
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
			TABLE_NAME = 'DepartmentMst')
BEGIN 
	Create table dbo.DepartmentMst(
			Id int identity(1,1) primary key,
			OrganizationId int not null,
			CompanyId int not null,
			DepartmentName nvarchar(50) not null,
			DepartmentDisplayName nvarchar(50) not null,
			[Address] nvarchar(500) null,
			Phone nvarchar(15) null,
			Mobile nvarchar(15) null,
			Email nvarchar(50) null,
			Website nvarchar(50) null,
			FaxNo nvarchar(50) null,
			ZipCode nvarchar(15) null,
			NPI nvarchar(50) null,
			BCN nvarchar(50) null,
			Sonarx nvarchar(50) null,
			TaxId nvarchar(50) null,
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
--Added by NP on 06-04-23--------------------------------------END---------------------------
--Executed on NP Local 06-04-23--------------------------------------------------------------
--Executed on Dev 10-04-23--------------------------------------------------------------
--Executed on SP Local 10-04-23--------------------------------------------------------------

--insert into dbo.PermissionMst values('Default Permission View','Default_Permission_View',1,0,0,0,getdate(),getdate())

--insert into dbo.PermissionMst values('Aging Organization Add','Aging_Organization_Add',1,0,0,0,getdate(),getdate())

--Added by NP on 21-04-23--------------------------------------START---------------------------
insert into dbo.UserMst values('Vraj','Brahmbhatt','Vraj_SA','12345','15-Mar-1999','1234567890','vraj.brahmbhatt@archesoftronix.com',1,0,0,0,getdate(),getdate(),1)

insert into dbo.UserMst values('Raj','Hanani','Raj_SA','12345','15-Apr-1998','1234567890','raj.hanani@archesoftronix.com',1,0,0,0,getdate(),getdate(),1)
--Added by NP on 21-04-23--------------------------------------END---------------------------
--Executed on NP Local 21-04-23--------------------------------------------------------------
--Executed on Dev 21-04-23--------------------------------------------------------------