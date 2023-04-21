CREATE DATABASE DatabaseName;
GO

USE DatabaseName;
GO

--===================================================================================================================================================
--=================================================================== TableName =====================================================================

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'TableName')
BEGIN 
	Create table dbo.TableName(
			Id int identity(1,1) primary key,
			UserId nvarchar(100) not null,
			Token nvarchar(max) not null,
			RefreshToken nvarchar(max) not null,
			TockenExpiredOn datetime not null,
			RefreshTockenExpiredOn datetime not null,
			IsActive bit default(1) not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO