﻿CREATE TABLE [dbo].[UsersDB]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Nickname] varchar(50) NOT NULL UNIQUE,
	[Email] varchar(50) NOT NULL UNIQUE,
	[PasswordHash] VARBINARY(64) NOT NULL,
	Salt VARCHAR(100),
	RoleId int DEFAULT 1 NOT NULL
)
