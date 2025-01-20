USE DotNetCourseDatabase
GO

CREATE OR ALTER PROCEDURE TutorialAppSchema.spRegistration_Upsert
    @Email NVARCHAR(50),
	@PassWordHash VARBINARY(MAX),
	@PasswordSalt VARBINARY(MAX) 
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM TutorialAppSchema.Auth WHERE Email = @Email)
        BEGIN
            INSERT INTO TutorialAppSchema.Auth(
                [Email],
                [PassWordHash],
                [PasswordSalt]
            ) VALUES (
                @Email,
                @PassWordHash,
                @PasswordSalt
            )
        END
    ELSE
        BEGIN
            UPDATE TutorialAppSchema.Auth
                SET PassWordHash = @PassWordHash, 
                    PasswordSalt = @PasswordSalt
                WHERE Email = @EmailA
        END
END
