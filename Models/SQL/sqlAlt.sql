USE DotNetCourseDatabase
GO

SELECT [UserId],
[FirstName],
[LastName],
[Email],
[Gender],
[Active] FROM TutorialAppSchema.Users

INSERT INTO TutorialAppSchema.Users(
    [FirstName],
    [LastName],
    [Email],
    [Gender],
    [Active]
) VALUES (
    'TestUser',
    'TestUser',
    'TestUser@gg.com',
    'Female',
    'true'
)

UPDATE TutorialAppSchema.Users
    SET   [FirstName] = 'Alberto',
    [LastName] = 'O''Finan',
    [Email] = 'alberto.OFinan@gmail.com',
    [Gender] = 'Male',
    [Active] = 1
    WHERE UserId = 1;

SELECT * FROM TutorialAppSchema.UserSalary WHERE UserId = 2;


SELECT * FROM TutorialAppSchema.UserSalary WHERE UserId = 7

SELECT COUNT(*) FROM TutorialAppSchema.Users

DELETE FROM TutorialAppSchema.UserSalary WHERE UserId = 1001


SELECT * FROM TutorialAppSchema.UserSalary ORDER BY UserId DESC
SELECT * FROM TutorialAppSchema.UserSalary WHERE Salary = 14000

ALTER TABLE TutorialAppSchema.UserSalary
ALTER COLUMN AvgSalary DECIMAL(15, 4) NULL;

SELECT * FROM TutorialAppSchema.UserSalary

-- UPDATE TutorialAppSchema.UserSalary
-- SET AvgSalary = 0
-- WHERE AvgSalary IS NULL;