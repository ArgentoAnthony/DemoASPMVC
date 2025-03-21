﻿CREATE TABLE [dbo].[Game]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	Title VARCHAR(100) NOT NULL,
	Description VARCHAR(MAX),
	Genre_Id int, 
    CONSTRAINT [FK_Game] FOREIGN KEY ([Genre_Id]) REFERENCES [Genre]([Id])
)
