--Created new DBScript_V1
--Added on 16-06-22 at 12:00 PM by Tanmay Sadamast—--------Start—----

-- Scaffolding Command
--Scaffold-DbContext "Server=.;user=sa;password=123;Database=HIMSAuthenticationDB;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'TokenMst')
BEGIN 
	Create table dbo.TokenMst (
	TokenId int NOT NULL PRIMARY KEY IDENTITY(1,1),
	UserName nvarchar(50) NOT NULL,
	Token nvarchar(MAX) NOT NULL,
	RefreshToken nvarchar(MAX) NOT NULL,
	CreateAt datetime NOT NULL,
	UpdatedAt datetime NOT NULL,
	ExpiredOn datetime NOT NULL
	);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

--Executed on Local at 16-06-22 at 12:00 PM by Tanmay Sadamast
--Added on 16-06-22 at 12:00 AM by Tanmay Sadamast—--------End—----