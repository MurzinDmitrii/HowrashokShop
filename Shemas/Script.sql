/*
Created		25.09.2023
Modified		27.02.2024
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
	[Email] Nvarchar(3500) NOT NULL,
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
	[ID] Nvarchar(100) NOT NULL,
	[FirstName] Nvarchar(50) NOT NULL,
	[LastName] Nvarchar(1) Identity NOT NULL,
Primary Key ([ID])
) 
go

Create table [AdminPassword]
(
	[ID] Nvarchar(100) NOT NULL,
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
Alter table [TablePart] add  foreign key([OrderID],[DateOrder]) references [Order] ([ID],[DateOrder])  on update no action on delete no action 
go
Alter table [Product] add  foreign key([ThemeID]) references [Theme] ([ID])  on update no action on delete no action 
go
Alter table [AdminPassword] add  foreign key([ID]) references [Admin] ([ID])  on update no action on delete no action 
go


Set quoted_identifier on
go


Set quoted_identifier off
go


Create role [Admin]
go
Create role [Client]
go
Create role [UnregisteredClient]
go


/* Roles permissions */
Grant select on [Product] to [Admin]
go
Grant update on [Product] to [Admin]
go
Grant delete on [Product] to [Admin]
go
Grant insert on [Product] to [Admin]
go
Grant references on [Product] to [Admin]
go
Grant select on [Category] to [Admin]
go
Grant update on [Category] to [Admin]
go
Grant delete on [Category] to [Admin]
go
Grant insert on [Category] to [Admin]
go
Grant references on [Category] to [Admin]
go
Grant select on [Cost] to [Admin]
go
Grant update on [Cost] to [Admin]
go
Grant delete on [Cost] to [Admin]
go
Grant insert on [Cost] to [Admin]
go
Grant references on [Cost] to [Admin]
go
Grant select on [Photo] to [Admin]
go
Grant update on [Photo] to [Admin]
go
Grant delete on [Photo] to [Admin]
go
Grant insert on [Photo] to [Admin]
go
Grant references on [Photo] to [Admin]
go
Grant select on [Client] to [Admin]
go
Grant update on [Client] to [Admin]
go
Grant delete on [Client] to [Admin]
go
Grant insert on [Client] to [Admin]
go
Grant references on [Client] to [Admin]
go
Grant select on [Client] to [Client]
go
Grant update on [Client] to [Client]
go
Grant delete on [Client] to [Client]
go
Grant insert on [Client] to [Client]
go
Grant references on [Client] to [Client]
go
Grant select on [Client] to [UnregisteredClient]
go
Grant update on [Client] to [UnregisteredClient]
go
Grant delete on [Client] to [UnregisteredClient]
go
Grant insert on [Client] to [UnregisteredClient]
go
Grant references on [Client] to [UnregisteredClient]
go
Grant select on [ClientsPassword] to [Admin]
go
Grant update on [ClientsPassword] to [Admin]
go
Grant delete on [ClientsPassword] to [Admin]
go
Grant insert on [ClientsPassword] to [Admin]
go
Grant references on [ClientsPassword] to [Admin]
go
Grant select on [ClientsPassword] to [Client]
go
Grant update on [ClientsPassword] to [Client]
go
Grant delete on [ClientsPassword] to [Client]
go
Grant insert on [ClientsPassword] to [Client]
go
Grant references on [ClientsPassword] to [Client]
go
Grant select on [ClientsPassword] to [UnregisteredClient]
go
Grant update on [ClientsPassword] to [UnregisteredClient]
go
Grant delete on [ClientsPassword] to [UnregisteredClient]
go
Grant insert on [ClientsPassword] to [UnregisteredClient]
go
Grant references on [ClientsPassword] to [UnregisteredClient]
go
Grant select on [Order] to [Admin]
go
Grant update on [Order] to [Admin]
go
Grant delete on [Order] to [Admin]
go
Grant insert on [Order] to [Admin]
go
Grant references on [Order] to [Admin]
go
Grant select on [Order] to [Client]
go
Grant update on [Order] to [Client]
go
Grant delete on [Order] to [Client]
go
Grant insert on [Order] to [Client]
go
Grant references on [Order] to [Client]
go
Grant select on [Theme] to [Admin]
go
Grant update on [Theme] to [Admin]
go
Grant delete on [Theme] to [Admin]
go
Grant insert on [Theme] to [Admin]
go
Grant references on [Theme] to [Admin]
go
Grant select on [TablePart] to [Admin]
go
Grant update on [TablePart] to [Admin]
go
Grant delete on [TablePart] to [Admin]
go
Grant insert on [TablePart] to [Admin]
go
Grant references on [TablePart] to [Admin]
go
Grant select on [TablePart] to [Client]
go
Grant update on [TablePart] to [Client]
go
Grant delete on [TablePart] to [Client]
go
Grant insert on [TablePart] to [Client]
go
Grant references on [TablePart] to [Client]
go
Grant select on [Discount] to [Admin]
go
Grant update on [Discount] to [Admin]
go
Grant delete on [Discount] to [Admin]
go
Grant insert on [Discount] to [Admin]
go
Grant references on [Discount] to [Admin]
go
Grant select on [Busket] to [Admin]
go
Grant update on [Busket] to [Admin]
go
Grant delete on [Busket] to [Admin]
go
Grant insert on [Busket] to [Admin]
go
Grant references on [Busket] to [Admin]
go
Grant select on [Busket] to [Client]
go
Grant update on [Busket] to [Client]
go
Grant delete on [Busket] to [Client]
go
Grant insert on [Busket] to [Client]
go
Grant references on [Busket] to [Client]
go


/* Users permissions */


