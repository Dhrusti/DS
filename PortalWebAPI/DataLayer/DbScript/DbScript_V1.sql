CREATE TABLE `filemst` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FileName` varchar(200) NOT NULL,
  `FilePath` varchar(500) DEFAULT NULL,
  `FileSize` varchar(100) DEFAULT NULL,
  `FileFormat` varchar(100) DEFAULT NULL,
  `ClientIP` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `IsDeleted` tinyint(1) DEFAULT NULL,
  `CreatedBy` int NOT NULL,
  `UpdatedBy` int NOT NULL,
  `CreatedAt` datetime NOT NULL,
  `UpdatedAt` datetime NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci

------------------------------Added By Dhrusti 09-05-23-------------------------------

--------------------------------UserTokenMst-------------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'UserTokenMst')
BEGIN 
	Create table dbo.UserTokenMst(
			Id int identity(1,1) primary key,
			Username string not null,
			Token nvarchar(max) not null,
			RefreshToken nvarchar(max) not null,
			CreatedDate datetime not null,
			UpdatedDate datetime not null,
			ExpiredOn datetime not null
			);
	PRINT 'Table Created' 
END
ELSE
BEGIN 
	PRINT 'Table Already Exist' 
END
GO

