
 --CallTypeMst---------------------
insert into dbo.CallTypeMst values('New_Scheduling',1,0,0,0,getdate(),getdate())
insert into dbo.CallTypeMst values('Re-Scheduling',1,0,0,0,getdate(),getdate())
insert into dbo.CallTypeMst values('Other',1,0,0,0,getdate(),getdate())
insert into dbo.CallTypeMst values('Done',0,1,0,0,getdate(),getdate())
GO

--ExtensionMst----------------------
insert into dbo.ExtensionMst values('501',1,0,0,0,getdate(),getdate())
insert into dbo.ExtensionMst values('502',1,0,0,0,getdate(),getdate())
GO

--RoleMst---------------------------
insert into dbo.RoleMst values('Super Admin',1,0,0,0,getdate(),getdate())
insert into dbo.RoleMst values('Admin',1,0,0,0,getdate(),getdate())
insert into dbo.RoleMst values('Data Operator',1,0,0,0,getdate(),getdate())
Go

--UserMst---------------------------
insert into dbo.UserMst values('Nikunj','Pandya','Nikunj','123','20-Mar-1993','1234567890','nikunjp.archesoftronix@gmail.com',1,0,0,0,getdate(),getdate(),1)

insert into dbo.UserMst values('Sonal','Patel','Sonal','123','15-Mar-1998','1234567890','sonalarche@gmail.com',1,0,0,0,getdate(),getdate(),1)
GO

--PhysicianMst----------------------
INSERT into [dbo].[PhysicianMst] VALUES (N'Keneth', N'Kundelko', N'MD', N'phd', N'MN', N'Jessica', N'Edwards', N'Alexa@archesoftornix.com', N'7687875676', 1, 0, 0, 0, getdate(),getdate())
GO
INSERT [dbo].[PhysicianMst] VALUES (N'Melciej', N'poltarok', N'MD', N'phd', N'MBBS', N'thamesh', N'rahseeda', N'poltarok@hotmail.com', N'675686878', 1, 0, 0, 0,  getdate(),getdate())
GO

--INSERT [dbo].[PhysicianMst] VALUES (N'Sonal', N'Patel', N'MD', N'phd', N'MBBS', N'thamesh', N'rahseeda', N'sonal.patel@archesoftronix.com', N'675686878', 0, 1, 0, 0,  getdate(),getdate())
--GO
--INSERT [dbo].[PhysicianMst] VALUES (N'ajay', N'zala', N'MD', N'phd', N'MBBS', N'thamesh', N'rahseeda', N'ajay.zala@archesoftronix.com', N'675686878', 0, 1, 0, 0,  getdate(),getdate())
--GO
--INSERT [dbo].[PhysicianMst] VALUES (N'Nikunj', N'Pandya', N'MD', N'phd', N'MBBS', N'thamesh', N'rahseeda', N'nikunjp.archesoftornix@gmail.com', N'675686878', 0, 1, 0, 0,  getdate(),getdate())
--GO
--INSERT [dbo].[PhysicianMst] VALUES (N'Raj', N'Hanani', N'MD', N'phd', N'MBBS', N'thamesh', N'rahseeda', N'raj.hanani@archesoftronix.com', N'675686878', 0, 1, 0, 0,  getdate(),getdate())
--GO
--INSERT [dbo].[PhysicianMst] VALUES (N'Vraj', N'Brahmbhatt', N'MD', N'phd', N'MBBS', N'thamesh', N'rahseeda', N'vraj.brahmbhatt@archesoftronix.com', N'675686878', 0, 1, 0, 0,  getdate(),getdate())
--GO
--INSERT [dbo].[PhysicianMst] VALUES (N'Harshil', N'Sheth', N'MD', N'phd', N'MBBS', N'thamesh', N'rahseeda', N'harshil.sheth@archesoftronix.com', N'675686878', 0, 1, 0, 0,  getdate(),getdate())
--GO

--ClientMst--------------------------
INSERT into [dbo].[ClientMst] VALUES ( N'Laurel', N'1', N'MD office', 91, N'7350', N'van Dusen', N'Dusen Road suite 430 ', N'street5', N'Laurel MD ', 123, N'20707', N'abc@gmail.com', N'abc@gmail.com', N'abc@gmail.com', N'6677878756', N'678767', 1, 0, 0, 0,getdate(),getdate())
GO
INSERT into [dbo].[ClientMst] VALUES (N'Greenbelt', N'1', N'MD office', 91, N'7500', N'Hanover', N'Hanover pkwy', N'stree6', N'Greenbelt MD', 123, N'20770', N'info@neuronedpa.com', N'appoitment@neuronedpa.com', N'ALohr@neuromedpa.com', N'9989780089', N'301 441-8696', 1, 0, 0, 0, getdate(),getdate())
GO


