IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'TblUserTokenMst')
BEGIN 
	CREATE TABLE TblUserTokenMst (
    Id int NOT NULL PRIMARY KEY IDENTITY(1,1),
	Username nvarchar(max),
	Password nvarchar(max),
	Token nvarchar(max),
	RefreshToken nvarchar(max),
	TokenCreated datetime2(7),
	TokenExpires datetime2(7),
	
)

PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END 


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'TblUserDocumentMst')
BEGIN 
	CREATE TABLE TblUserDocumentMst (
    DocumentId int NOT NULL PRIMARY KEY IDENTITY(1,1),
	UserId                      int,
	DocumentType				nvarchar(255),
	UploadDocument				nvarchar(255),
	CreatedBy					nvarchar(255),
	CreatedOn					datetime,
	UpdatedBy					nvarchar(255),
	UpdatedOn					datetime not null,
	IsActive                    bit not null default(0),
	IsDelete                    bit not null default(0),
)

PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'TblOTPMst')
BEGIN 
	CREATE TABLE TblOTPMst (
    OTPID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	ContactNumber  nvarchar(25),
	OneTimePassword nvarchar(25),
	OTPCreated datetime2(7),
	OTPExpires datetime2(7),
	
)

PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END 
