CREATE TABLE [dbo].[UsersDB]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Nickname] varchar(50) NOT NULL,
	[Email] varchar(50) NOT NULL,
	[Password] varchar(50) NOT NULL
)
