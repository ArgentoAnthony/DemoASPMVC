CREATE PROCEDURE [dbo].[ProcFavoris]
	@id int
AS
	SELECT g.Id, g.Title, g.Description, ge.Label
	FROM Game g 
	JOIN Favoris f ON g.Id = f.Id_Jeu
	JOIN UsersDB u ON u.Id = f.Id_User
	JOIN Genre ge ON g.Id = ge.Id
	WHERE Id_User = @id;
