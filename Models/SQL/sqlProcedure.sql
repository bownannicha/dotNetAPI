USE DotNetCourseDatabase
GO

-- CREATE PROCEDURE TutorialAppSchema.spUsers_Get
ALTER PROCEDURE TutorialAppSchema.spUsers_Get
/* EXEC TutorialAppSchema.spUsers_Get */ -- get all users
/* EXEC TutorialAppSchema.spUsers_Get 3 */ -- pass in a parameter
/* EXEC TutorialAppSchema.spUsers_Get @userId=5  */ -- pass in a parameter
/* EXEC TutorialAppSchema.spUsers_Get @userId=5, @RunFilter=1  */ -- pass in a parameter
    -- @RunFilter BIT, 
    @UserId INT = NULL
AS 
BEGIN
    SELECT [Users].[UserId],
        [Users].[FirstName],
        [Users].[LastName],
        [Users].[Email],
        [Users].[Gender],
        [Users].[Active] 
    FROM TutorialAppSchema.Users AS Users
        -- WHERE Users.UserId = ISNULL(@UserId, 3) --If parameter isnull then the default is userid 3
        WHERE Users.UserId = ISNULL(@UserId, Users.UserId) --If parameter isnull then the default is all users
END
-- DONT WRITE anything after END