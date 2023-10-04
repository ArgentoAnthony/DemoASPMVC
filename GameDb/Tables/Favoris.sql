CREATE TABLE [dbo].[Favoris]
(
	Id_Jeu INT NOT NULL,
	Id_User INT NOT NULL,
    CONSTRAINT [FK_Favoris_Game] FOREIGN KEY ([Id_Jeu]) REFERENCES [Game]([Id]),
	CONSTRAINT [FK_Favoris_User] FOREIGN KEY ([Id_User]) REFERENCES [UsersDB]([Id]),
	CONSTRAINT PK_Favoris PRIMARY KEY (Id_Jeu, Id_User)
)
