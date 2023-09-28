CREATE TABLE [dbo].[Movie]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	Title nvarchar(50),
	Description nvarchar(MAX),
	ImageUrl nvarchar(MAX) NULL
)
