CREATE SEQUENCE "Users_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

create table "Users"
(
	"Id" int not null DEFAULT nextval('public."Users_id_seq"'::regclass),
	"Login" character varying(255) ,
	"Password" character varying(10000) not null,
	"RoleId" int not null,
	"IsBlocked" boolean not null,
	"RegistrationDate" timestamp with time zone not null,

	CONSTRAINT "Users_pkey" PRIMARY KEY ("Id")
);

--1qazxsw234
--insert into "Users" ("Login", "Password", "RoleId", "IsBlocked", "RegistrationDate") values
--('dev', 'edfd75fd2fdfa6f7a9ee8475ad570f74f7d2a29f4ac3893290b99c3f203d50286741e1d2a7f2734c382dbf42d3ce0d5b0c5fefe635df01d65dba424417fe8580', 0, false, CURRENT_TIMESTAMP),
--('admin', 'edfd75fd2fdfa6f7a9ee8475ad570f74f7d2a29f4ac3893290b99c3f203d50286741e1d2a7f2734c382dbf42d3ce0d5b0c5fefe635df01d65dba424417fe8580', 1, false, CURRENT_TIMESTAMP);

CREATE SEQUENCE "Locations_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

create table "Locations"(
	"Id" int not null DEFAULT nextval('public."Locations_id_seq"'::regclass),
	"Title" character varying(10000) not null,
	"FlavorText" character varying(10000),

	CONSTRAINT "Locations_pkey" PRIMARY KEY ("Id")
);

CREATE SEQUENCE "Structures_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

create table "Structures"(
	"Id" int not null DEFAULT nextval('public."Structures_id_seq"'::regclass),
	"Title" character varying(10000) not null,
	"FlavorText" character varying(10000),

	CONSTRAINT "Structures_pkey" PRIMARY KEY ("Id")
);

CREATE SEQUENCE "Landscapes_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

create table "Landscapes"(
	"Id" int not null DEFAULT nextval('public."Landscapes_id_seq"'::regclass),
	"Title" character varying(10000) not null,
	"FlavorText" character varying(10000),

	CONSTRAINT "Landscapes_pkey" PRIMARY KEY ("Id")
);

CREATE SEQUENCE "LocationsToContents_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

create table "LocationsToContents"(
	"Id" int not null DEFAULT nextval('public."LocationsToContents_id_seq"'::regclass),
	"Title" character varying(10000) not null,
	"LocationId" int not null,
	"StructureId" int,
	"LandscapeId" int,

	constraint "FK_LocationsToContents_Locations" foreign key ("LocationId") references "Locations"("Id"),
	constraint "FK_LocationsToContents_Structures" foreign key ("StructureId") references "Structures"("Id"),
	constraint "FK_LocationsToContents_Landscapes" foreign key ("LandscapeId") references "Landscapes"("Id"),
	CONSTRAINT "LocationsToContents_pkey" PRIMARY KEY ("Id")
);

CREATE SEQUENCE "Creatures_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

create table "Creatures"(
	"Id" int not null DEFAULT nextval('public."Creatures_id_seq"'::regclass),
	"Title" character varying(10000) not null,
	"FlavorText" character varying(10000),

	CONSTRAINT "Creatures_pkey" PRIMARY KEY ("Id")
);

CREATE SEQUENCE "Items_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

create table "Items"(
	"Id" int not null DEFAULT nextval('public."Items_id_seq"'::regclass),
	"Title" character varying(10000) not null,
	"FlavorText" character varying(10000),
	"CreaturesID" int,
	"StructuresID" int,

	constraint "FK_Items_Creatures" foreign key ("CreaturesID") references "Creatures"("Id"),
	constraint "FK_Items_Structures" foreign key ("StructuresID") references "Structures"("Id"),
	CONSTRAINT "Items_pkey" PRIMARY KEY ("Id")
);

CREATE SEQUENCE "StructuresToItemsOrCreatures_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

create table "StructuresToItemsOrCreatures"(
	"Id" int not null DEFAULT nextval('public."StructuresToItemsOrCreatures_id_seq"'::regclass),
	"StructureID" int not null,
	"ItemId" int,
	"CreatureId" int,

	constraint "FK_StructuresToItemsOrCreatures_Structure" foreign key ("StructureID") references "Structures"("Id"),
	constraint "FK_StructuresToItemsOrCreatures_Items" foreign key ("ItemId") references "Items"("Id"),
	constraint "FK_StructuresToItemsOrCreatures_Creatures" foreign key ("CreatureId") references "Creatures"("Id"),
	CONSTRAINT "StructuresToItemsOrCreatures_pkey" PRIMARY KEY ("Id")
);

CREATE SEQUENCE "Quests_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

create table "Quests"(
	"Id" int not null DEFAULT nextval('public."Quests_id_seq"'::regclass),
	"Title" character varying(10000) not null,
	"FlavorText" character varying(10000),
	"IsComplited" boolean not null default true,
	"NextQuestID" int,

	constraint "FK_Quests_Quests" foreign key ("NextQuestID") references "Quests"("Id"),
	CONSTRAINT "Quests_pkey" PRIMARY KEY ("Id")
);

CREATE SEQUENCE "QuestsToItemsOrCreatures_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

create table "QuestsToItemsOrCreatures" (
	"Id" int not null DEFAULT nextval('public."QuestsToItemsOrCreatures_id_seq"'::regclass),
	"QuestId" int not null,
	"ItemId" int,
	"CreatureId" int,
	
	constraint "FK_QuestsToItems_Quests" foreign key ("QuestId") references "Quests"("Id"),
	constraint "FK_QuestsToItems_Items" foreign key ("ItemId") references "Items"("Id"),
	constraint "FK_QuestsToItems_Creatures" foreign key ("CreatureId") references "Creatures"("Id"),
	CONSTRAINT "QuestsToItemsOrCreatures_pkey" PRIMARY KEY ("Id")
);

CREATE SEQUENCE "Awards_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

create table "Awards"(
	"Id" int not null DEFAULT nextval('public."Awards_id_seq"'::regclass),
	"UserId" int not null,
	"Title" character varying(10000) not null,
	"Description" character varying(10000),

	constraint "FK_Awards_Users" foreign key ("UserId") references "Users"("Id"),
	CONSTRAINT "Awards_pkey" PRIMARY KEY ("Id")
);

CREATE SEQUENCE "Nominations_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

create table "Nominations"(
	"Id" int not null DEFAULT nextval('public."Nominations_id_seq"'::regclass),
	"Title" character varying(10000) not null,
	"Description" character varying(10000),
	"AwardsId" int not null,
	
	constraint "FK_Nominations_Awards" foreign key ("AwardsId") references "Awards"("Id"),
	CONSTRAINT "Nominations_pkey" PRIMARY KEY ("Id")
);

CREATE SEQUENCE "NominationsSelectionOptions_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

create table "NominationsSelectionOptions"(
	"Id" int not null DEFAULT nextval('public."NominationsSelectionOptions_id_seq"'::regclass),
	"UserId" int,
	"Title" character varying(10000) not null,
	"Description" character varying(10000),
	"NominationId" int not null,
	
	constraint "FK_NominationsSelectionOptions_Users" foreign key ("UserId") references "Users"("Id"),
	constraint "FK_NominationsSelectionOptions_Nominations" foreign key ("NominationId") references "Nominations"("Id"),
	CONSTRAINT "NominationsSelectionOptions_pkey" PRIMARY KEY ("Id")
);

CREATE SEQUENCE "Votes_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

create table "Votes"
(
	"Id" int not null DEFAULT nextval('public."Votes_id_seq"'::regclass),
	"UserId" int,
	"NominationsSelectionOptionsId" int not null,
	"TelegramUserName" character varying(10000) not null,
	"IsCanseld" boolean not null,
	
	constraint "FK_Votes_Users" foreign key ("UserId") references "Users"("Id"),
	constraint "FK_Votes_NominationsSelectionOptions" foreign key ("NominationsSelectionOptionsId") references "NominationsSelectionOptions"("Id"),
	CONSTRAINT "Votes_pkey" PRIMARY KEY ("Id")
);

CREATE SEQUENCE "AwardSessions_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

create table "AwardSessions"
(
	"Id" int not null DEFAULT nextval('public."AwardSessions_id_seq"'::regclass),
	"UserId" int not null,
	"ConnectionCode" character varying(10000) not null, 
	"State" int not null, 
	"NominationPassed" int not null,
	"AwardId" int not null,
	
	constraint "FK_AwardSessions_Users" foreign key ("UserId") references "Users"("Id"),
	constraint "FK_AwardSessions_Awards" foreign key ("AwardId") references "Awards"("Id"),
	CONSTRAINT "AwardSessions_pkey" PRIMARY KEY ("Id")
);


CREATE SEQUENCE "Pictures_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

create table "Pictures" (
	"Id" int not null DEFAULT nextval('public."Pictures_id_seq"'::regclass),
	"Title" character varying(10000) not null,
	"PicturePath" character varying(10000) not null,
	CONSTRAINT "Pictures_pkey" PRIMARY KEY ("Id")
);

CREATE SEQUENCE "PicturesToOther_id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

create table "PicturesToOther" (
	"Id" int not null DEFAULT nextval('public."PicturesToOther_id_seq"'::regclass),
	"PictureId" int,
	"ItemId" int,
	"CreatureId" int,
	"StructureId" int,
	"AwardId" int,
	"NominationId" int,
	"NominationsSelectionOptionId" int,

	constraint FK_PicturesToOther_Pictures foreign key ("PictureId") references "Pictures"("Id"),
	constraint "FK_PicturesToOther_Items" foreign key ("ItemId") references "Items"("Id"),
	constraint "FK_PicturesToOther_Creatures" foreign key ("CreatureId") references "Creatures"("Id"),
	constraint "FK_PicturesToOther_Structures" foreign key ("StructureId") references "Structures"("Id"),
	constraint "FK_PicturesToOther_Awards" foreign key ("AwardId") references "Awards"("Id"),
	constraint "FK_PicturesToOther_Nominations" foreign key ("NominationId") references "Nominations"("Id"),
	constraint "FK_PicturesToOther_NominationsSelectionOptions" foreign key ("NominationsSelectionOptionId") references "NominationsSelectionOptions"("Id"),
	CONSTRAINT "PicturesToOther_pkey" PRIMARY KEY ("Id")
);