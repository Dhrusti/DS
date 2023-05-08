IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  
			TABLE_NAME = 'PermissionMst')
BEGIN 
	Create table dbo.PermissionMst(
			Id int identity(1,1) primary key,
			PermissionName nvarchar(50) not null,
			PermissionCode nvarchar(50) not null,
			IsActive bit default(1) not null,
			IsDelete bit default(0) not null,
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
			TABLE_NAME = 'UserPermissions')
BEGIN 
	Create table dbo.UserPermissions(
			Id int identity(1,1) primary key,
			PermissionId int not null,
			UserId int not null,
			RoleId int not null,
			IsActive bit default(1) not null,
			IsDelete bit default(0) not null,
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
			TABLE_NAME = 'DefaultPermissions')
BEGIN 
	Create table dbo.DefaultPermissions(
			Id int identity(1,1) primary key,
			RoleId int not null,
			PermissionId int not null,
			IsActive bit default(1) not null,
			IsDelete bit default(0) not null,
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

--PermissionMst
--insert into dbo.PermissionMst values('Scheduling Admin Notification','Scheduling_Admin_Notification',1,0,0,0,getdate(),getdate())
--insert into dbo.PermissionMst values('Scheduling DO Notification','Scheduling_DO_Notification',1,0,0,0,getdate(),getdate())
--insert into dbo.PermissionMst values('Scheduling View','Scheduling_View',1,0,0,0,getdate(),getdate())
--insert into dbo.PermissionMst values('Scheduling Add','Scheduling_Add',1,0,0,0,getdate(),getdate())
--insert into dbo.PermissionMst values('Dashboard','Dashboard',1,0,0,0,getdate(),getdate())

--UserMst--------------------------------------
--insert into dbo.UserMst values('Nikunj','Pandya','Nikunj_Admin','123','20-Mar-1993','1234567890','nikunjp.archesoftronix@gmail.com',1,0,0,0,getdate(),getdate(),2)
--insert into dbo.UserMst values('Sonal','Patel','Sonal_Admin','123','15-Mar-1998','1234567890','sonalarche@gmail.com',1,0,0,0,getdate(),getdate(),2)

--insert into dbo.UserMst values('Nikunj','Pandya','Nikunj_DO','123','20-Mar-1993','1234567890','nikunjp.archesoftronix@gmail.com',1,0,0,0,getdate(),getdate(),3)
--insert into dbo.UserMst values('Sonal','Patel','Sonal_DO','123','15-Mar-1998','1234567890','sonalarche@gmail.com',1,0,0,0,getdate(),getdate(),3)

--DefaultPermissions-----------------------------

--Super Admin
insert into dbo.DefaultPermissions values(1,3,1,0,0,0,getdate(),getdate())
insert into dbo.DefaultPermissions values(1,4,1,0,0,0,getdate(),getdate())
insert into dbo.DefaultPermissions values(1,5,1,0,0,0,getdate(),getdate())

--Admin
insert into dbo.DefaultPermissions values(2,1,1,0,0,0,getdate(),getdate())
insert into dbo.DefaultPermissions values(2,3,1,0,0,0,getdate(),getdate())
insert into dbo.DefaultPermissions values(2,4,1,0,0,0,getdate(),getdate())
insert into dbo.DefaultPermissions values(2,5,1,0,0,0,getdate(),getdate())

--Data operator
insert into dbo.DefaultPermissions values(3,2,1,0,0,0,getdate(),getdate())
insert into dbo.DefaultPermissions values(3,3,1,0,0,0,getdate(),getdate())
insert into dbo.DefaultPermissions values(3,4,1,0,0,0,getdate(),getdate())
insert into dbo.DefaultPermissions values(3,5,1,0,0,0,getdate(),getdate())

--UserMst--------------------------------------
insert into dbo.UserMst values('SuperAdmin','SuperAdmin','SuperAdmin','123','15-Mar-1999','1234567890','superAdmin@gmail.com',1,0,0,0,getdate(),getdate(),1)


update dbo.PermissionMst set PermissionName='Scheduling Appointment', PermissionCode='Scheduling_Appointment' where id=3 and PermissionCode='Scheduling_View'

update dbo.PermissionMst set PermissionName='Scheduling Accept Reject', PermissionCode='Scheduling_Accept_Reject' where id=4 and PermissionCode='Scheduling_Add'

update dbo.UserMst set Password='12345' where id in (1,2,3)

