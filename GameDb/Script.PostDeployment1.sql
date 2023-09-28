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

INSERT INTO Game (Title, Description, Genre)
VALUES ('WoW','Meilleur jeu', 'MMORPG'),
        ('LoL', 'Pire jeu','MOBA'),
        ('Starfield','Surcoté','RPG');

EXEC UserRegister 'admin@mail.be', 'motdepasse', 'Arthur'
EXEC UserRegister 'user@mail.be', 'motdepasse', 'Merlin'

UPDATE UsersDB SET RoleId = 3 WHERE Id = 1