/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT INTO Movie (Title,Description,ImageUrl)
VALUES ('Interstellar','.....',null),
       ('Forest Gump','Cours Forest cours!',null),
       ('Fast and furious 10','I''m fast as fuck boy...',null);

INSERT INTO Genre (Label)
VALUES('RPG'),
      ('Moba'),
      ('MMO')

INSERT INTO Game (Title, Description, Genre_Id)
VALUES ('WoW','Meilleur jeu', 3),
        ('LoL', 'Pire jeu', 2),
        ('Starfield','Surcoté',1);

EXEC UserRegister 'admin@mail.be', 'motdepasse', 'Arthur'
EXEC UserRegister 'user@mail.be', 'motdepasse', 'Merlin'

INSERT INTO Favoris VALUES (1,1), (1,2);

UPDATE UsersDB SET RoleId = 3 WHERE Id = 1