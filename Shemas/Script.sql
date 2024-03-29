/*
Created		25.09.2023
Modified		26.03.2024
Project		
Model			
Company		
Author		
Version		
Database		MS SQL 2005 
*/


Create table [Product]
(
	[ID] Integer Identity NOT NULL,
	[CategoryID] Integer NOT NULL,
	[Name] Nvarchar(200) NOT NULL,
	[Description] Nvarchar(3000) NOT NULL,
	[ThemeID] Integer NOT NULL,
	[Arhived] Bit NULL,
Primary Key ([ID])
) 
go

Create table [Category]
(
	[ID] Integer Identity NOT NULL,
	[Name] Nvarchar(30) NOT NULL, UNIQUE ([Name]),
Primary Key ([ID])
) 
go

Create table [Cost]
(
	[ProductID] Integer NOT NULL,
	[DateOfSetting] Datetime NOT NULL,
	[Size] Money NOT NULL,
Primary Key ([ProductID],[DateOfSetting])
) 
go

Create table [Photo]
(
	[ID] Integer Identity NOT NULL,
	[Photopath] Nvarchar(200) NOT NULL,
	[ProductID] Integer NOT NULL,
Primary Key ([ID])
) 
go

Create table [Client]
(
	[ID] Integer Identity NOT NULL,
	[LastName] Nvarchar(50) NOT NULL,
	[FirstName] Nvarchar(50) NOT NULL,
	[Email] Nvarchar(3500) NOT NULL, UNIQUE ([Email]),
	[Birthday] Datetime NOT NULL,
Primary Key ([ID])
) 
go

Create table [ClientsPassword]
(
	[ClientID] Integer NOT NULL,
	[Password] Nvarchar(3500) NOT NULL,
Primary Key ([ClientID])
) 
go

Create table [Order]
(
	[ID] Integer Identity NOT NULL,
	[DateOrder] Datetime NOT NULL,
	[ClientID] Integer NOT NULL,
	[Completed] Bit NOT NULL,
	[Address] nvarchar(1000),
Primary Key ([ID],[DateOrder])
) 
go

Create table [Theme]
(
	[ID] Integer Identity NOT NULL,
	[Name] Nvarchar(50) NOT NULL, UNIQUE ([Name]),
Primary Key ([ID])
) 
go

Create table [TablePart]
(
	[ID] Integer Identity NOT NULL,
	[OrderID] Integer NOT NULL,
	[DateOrder] Datetime NOT NULL,
	[ProductID] Integer NOT NULL,
	[Quantity] Integer NOT NULL,
Primary Key ([ID],[OrderID],[DateOrder],[ProductID])
) 
go

Create table [Discount]
(
	[ID] Integer Identity NOT NULL,
	[DateOfSetting] Datetime NOT NULL,
	[ProductID] Integer NOT NULL,
	[Size] Integer NOT NULL,
	[During] Integer NOT NULL,
Primary Key ([ID],[DateOfSetting],[ProductID])
) 
go

Create table [Busket]
(
	[ID] Integer Identity NOT NULL,
	[ClientID] Integer NOT NULL,
	[ProductID] Integer NOT NULL,
	[Quantity] Integer NOT NULL,
Primary Key ([ID],[ClientID],[ProductID])
) 
go

Create table [Admin]
(
	[ID] int Identity NOT NULL,
	[FirstName] Nvarchar(50) NOT NULL,
	[LastName] Nvarchar(50) NOT NULL,
	[Login] Nvarchar(200) NOT NULL, UNIQUE ([Login]),
Primary Key ([ID])
) 
go

Create table [AdminPassword]
(
	[ID] int NOT NULL,
	[Password] Varbinary(3500) NOT NULL,
Primary Key ([ID])
) 
go

Create table [Comments]
(
	[ID] Integer Identity NOT NULL,
	[ClientID] Integer NOT NULL,
	[ProductID] Integer NOT NULL,
	[Comment] Nvarchar(1000) NOT NULL,
	[Mark] Integer NOT NULL,
Primary Key ([ID])
) 
go

Create table [Chat]
(
	[Id] Integer Identity NOT NULL,
	[ClientId] Integer NOT NULL,
	[AdminId] int NOT NULL,
Primary Key ([Id])
) 
go

Create table [Message]
(
	[Id] Integer Identity NOT NULL,
	[MesText] Nvarchar(2000) NOT NULL,
	[Who] Bit NOT NULL,
	[ChatId] Integer NOT NULL,
Primary Key ([Id])
) 
go

Create table [Material]
(
	[Id] Integer Identity NOT NULL,
	[Name] Nvarchar(100) NOT NULL, UNIQUE ([Name]),
Primary Key ([Id])
) 
go

Create table [ProductMaterials]
(
	[ID] Integer NOT NULL,
	[MaterialId] Integer NOT NULL,
Primary Key ([ID],[MaterialId])
) 
go


Alter table [Cost] add  foreign key([ProductID]) references [Product] ([ID])  on update no action on delete no action 
go
Alter table [Photo] add  foreign key([ProductID]) references [Product] ([ID])  on update no action on delete no action 
go
Alter table [TablePart] add  foreign key([ProductID]) references [Product] ([ID])  on update no action on delete no action 
go
Alter table [Busket] add  foreign key([ProductID]) references [Product] ([ID])  on update no action on delete no action 
go
Alter table [Discount] add  foreign key([ProductID]) references [Product] ([ID])  on update no action on delete no action 
go
Alter table [Comments] add  foreign key([ProductID]) references [Product] ([ID])  on update no action on delete no action 
go
Alter table [ProductMaterials] add  foreign key([ID]) references [Product] ([ID])  on update no action on delete no action 
go
Alter table [Product] add  foreign key([CategoryID]) references [Category] ([ID])  on update no action on delete no action 
go
Alter table [ClientsPassword] add  foreign key([ClientID]) references [Client] ([ID])  on update no action on delete no action 
go
Alter table [Order] add  foreign key([ClientID]) references [Client] ([ID])  on update no action on delete no action 
go
Alter table [Busket] add  foreign key([ClientID]) references [Client] ([ID])  on update no action on delete no action 
go
Alter table [Comments] add  foreign key([ClientID]) references [Client] ([ID])  on update no action on delete no action 
go
Alter table [Chat] add  foreign key([ClientId]) references [Client] ([ID])  on update no action on delete no action 
go
Alter table [TablePart] add  foreign key([OrderID],[DateOrder]) references [Order] ([ID],[DateOrder])  on update no action on delete no action 
go
Alter table [Product] add  foreign key([ThemeID]) references [Theme] ([ID])  on update no action on delete no action 
go
Alter table [AdminPassword] add  foreign key([ID]) references [Admin] ([ID])  on update no action on delete no action 
go
Alter table [Chat] add  foreign key([AdminId]) references [Admin] ([ID])  on update no action on delete no action 
go
Alter table [Message] add  foreign key([ChatId]) references [Chat] ([Id])  on update no action on delete no action 
go
Alter table [ProductMaterials] add  foreign key([MaterialId]) references [Material] ([Id])  on update no action on delete no action 
go


Set quoted_identifier on
go


Set quoted_identifier off
go


