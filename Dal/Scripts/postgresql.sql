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
	"AwardId" int,
	"NominationId" int,
	"NominationsSelectionOptionId" int,

	constraint FK_PicturesToOther_Pictures foreign key ("PictureId") references "Pictures"("Id"),
	constraint "FK_PicturesToOther_Awards" foreign key ("AwardId") references "Awards"("Id"),
	constraint "FK_PicturesToOther_Nominations" foreign key ("NominationId") references "Nominations"("Id"),
	constraint "FK_PicturesToOther_NominationsSelectionOptions" foreign key ("NominationsSelectionOptionId") references "NominationsSelectionOptions"("Id"),
	CONSTRAINT "PicturesToOther_pkey" PRIMARY KEY ("Id")
);

alter table "Votes" add column "VoteTir" int;
alter table "Votes" add column "TelegramAvatar" character varying(10000);
alter table "Nominations" add column "OrderId" int;