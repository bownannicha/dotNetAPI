USE DotNetCourseDatabase

CREATE TABLE TutorialAppSchema.Auth(
    Email NVARCHAR(50) -- nvarchar to match Email in Users table
    , PassWordHash VARBINARY(MAX)
    , PasswordSalt VARBINARY(MAX) -- TO COMPARE WITH PasswordHash

)

SELECT * FROM TutorialAppSchema.Users WHERE Email = 'test1@test.com'

SELECT * FROM TutorialAppSchema.Auth

