create database DNDHelper
alter database DNDHelper set recovery simple
go

use DNDHelper
go

create table Users
(
	Id int not null identity constraint PK_Users primary key,
	[Login] nvarchar(200) constraint Unique_Users_Login unique not null,
	[Password] nvarchar(max) not null,
	RoleId int not null,
	IsBlocked bit not null,
	RegistrationDate datetime not null,
)

--1qazxsw234
insert into Users values
('dev', 'edfd75fd2fdfa6f7a9ee8475ad570f74f7d2a29f4ac3893290b99c3f203d50286741e1d2a7f2734c382dbf42d3ce0d5b0c5fefe635df01d65dba424417fe8580', 0, 0, GETDATE()),
('admin', 'edfd75fd2fdfa6f7a9ee8475ad570f74f7d2a29f4ac3893290b99c3f203d50286741e1d2a7f2734c382dbf42d3ce0d5b0c5fefe635df01d65dba424417fe8580', 1, 0, GETDATE());
go

create table Locations(
	Id int not null identity constraint PK_Locations primary key,
	Title nvarchar(max) not null,
	FlavorText nvarchar(max),
)

create table LocationsToContents(
	Id int not null identity constraint PK_LocationsToContents primary key,
	Title nvarchar(max) not null,
	LocationId int not null constraint FK_LocationsToContents_Locations foreign key references Locations(Id),
)

create table Structures(
	Id int not null identity constraint PK_Structures primary key,
	Title nvarchar(max) not null,
	FlavorText nvarchar(max),
	LocationsToContentsId int constraint FK_Structures_LocationsToContents foreign key references LocationsToContents(Id)
)

create table Landscapes(
	Id int not null identity constraint PK_Landscapes primary key,
	Title nvarchar(max) not null,
	FlavorText nvarchar(max),
	LocationsToContentsId int constraint FK_Landscapes_LocationsToContents foreign key references LocationsToContents(Id)
)

create table Creatures(
	Id int not null identity constraint PK_Creatures primary key,
	Title nvarchar(max) not null,
	FlavorText nvarchar(max),
	--TO DO Charectiristic
	StructuresID int constraint FK_Creatures_Structures foreign key references Structures(Id),
)

create table Items(
	Id int not null identity constraint PK_Items primary key,
	Title nvarchar(max) not null,
	FlavorText nvarchar(max),
	CreaturesID int constraint FK_Items_Creatures foreign key references Creatures(Id),
	StructuresID int constraint FK_Items_Structures foreign key references Structures(Id),
)

create table Quests  (
	Id int not null identity constraint PK_Quests primary key,
	Title nvarchar(max) not null,
	FlavorText nvarchar(max),
	IsComplited bit not null default 0,
	NextQuestID int constraint FK_Quests_Quests foreign key references Quests(Id),
)

create table QuestsToItems (
	Id int not null identity constraint PK_QuestsToItems primary key,
	QuestId int not null  constraint FK_QuestsToItems_Quests foreign key references Quests(Id),
)

create table QuestsToCreatures (
	Id int not null identity constraint PK_QuestsToCreatures primary key,
	QuestId int not null  constraint FK_QuestsToCreatures_Quests foreign key references Quests(Id),
)

alter table Quests add QuestsToCreatureId int constraint FK_Quests_QuestsToCreatures foreign key references QuestsToCreatures(Id)
alter table Quests add QuestsToItemsId int constraint FK_Quests_QuestsToItems foreign key references QuestsToItems(Id)

create table PicturesToOther(
	Id int not null identity constraint PK_PicturesToOther primary key,
	ItemId int constraint FK_PicturesToOther_Items foreign key references Items(Id),
	CreatureId int constraint FK_PicturesToOther_Creatures foreign key references Creatures(Id),
	StructureId int constraint FK_PicturesToOther_Structures foreign key references Structures(Id),
)

create table Pictures (
	Id int not null identity constraint PK_Pictures primary key,
	Title nvarchar(max) not null,
	PicturePath nvarchar(max) not null,
	PicturesToOtherId int constraint FK_Pictures_PicturesToOther foreign key references PicturesToOther(Id)
)
