CREATE PROCEDURE [dbo].[CreateDirectorySp]
	@ParentDirID INT = null,
	@Name VARCHAR(255),
	@Path VARCHAR(MAX)
AS
	IF NOT EXISTS (SELECT 1 FROM [dbo].[Directories] WHERE [Path] = @Path)
			BEGIN
				DECLARE @Size AS INT = 0;
				DECLARE @DateNowUTC AS DATETIME = GETUTCDATE();

				INSERT INTO 
					[dbo].[Directories] 
				VALUES
					(@ParentDirID, @Name, @Path, @Size, @DateNowUTC, @DateNowUTC);
			END;
			BEGIN
				SELECT SCOPE_IDENTITY() as ID;
			END;