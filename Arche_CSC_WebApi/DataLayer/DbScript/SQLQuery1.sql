--------------------------------CountryMst-------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'CountryMst')
BEGIN 
	Create table dbo.CountryMst(
			CountryId int identity(1,1) primary key,
			CountryName nvarchar(250) not null,
			Iso2 nvarchar(250) not null,
			DialingCode nvarchar(250) not null,
			IsActive bit not null,
			IsDeleted bit not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate DateTime not null,
			UpdatedDate DateTime not null
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

--------------------------------StateMst-------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'StateMst')
BEGIN 
	Create table dbo.StateMst(
			StateId int identity(1,1) primary key,
			CountryId int not null,
			StateName nvarchar(250) not null,
			Iso2 nvarchar(250) not null,
			IsActive bit not null,
			IsDeleted bit not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate DateTime not null,
			UpdatedDate DateTime not null
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

--------------------------------CityMst-------------------------------------

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'CityMst')
BEGIN 
	Create table dbo.CityMst(
			CityId int identity(1,1) primary key,
			StateId int not null,
			CityName nvarchar(250) not null,
			IsActive bit not null,
			IsDeleted bit not null,
			CreatedBy int not null,
			UpdatedBy int not null,
			CreatedDate DateTime not null,
			UpdatedDate DateTime not null
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

