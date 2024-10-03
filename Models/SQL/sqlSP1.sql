USE DotNetCourseDatabase
GO

-- CREATE PROCEDURE TutorialAppSchema.spUserSalary_Get
ALTER PROCEDURE TutorialAppSchema.spUserSalary_Get
/* EXEC TutorialAppSchema.spUserSalary_Get @userId=1 */
    @UserId INT = NULL,
    @Active BIT = NULL
AS
BEGIN
    /* OLD VERSION OF DROP TEMP TABLE IF EXISTS */
    -- IF OBJECT_ID('temp..#AverageDeptSalary') IS NOT NULL
    --     BEGIN
    --         DROP TABLE IF EXISTS #AverageDepSalary
    --     END

    DROP TABLE IF EXISTS #AverageDepSalary --NEW VERSION OF DROP TABLE


    SELECT UserJobInfo.Department,
        AVG(UserSalary.Salary) AS AvgSalary
        INTO #AverageDepSalary -- #<temporay table> that we can access from inside this sql ##<Global table>
    FROM TutorialAppSchema.Users AS Users
        LEFT JOIN TutorialAppSchema.UserSalary AS UserSalary
            ON UserSalary.UserId = Users.UserId
        LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
            ON UserJobInfo.UserId = Users.UserId
        GROUP BY UserJobInfo.Department

    CREATE CLUSTERED INDEX cix_AverageDeptSalary_Department ON #AverageDepSalary(Department)
    -- this clustered index will speed up the process time for bigger data

    SELECT [Users].[UserId],
        [Users].[FirstName],
        [Users].[LastName],
        [Users].[Email],
        [Users].[Gender],
        [Users].[Active],
        UserSalary.Salary,
        UserJobInfo.Department,
        UserJobInfo.JobTitle,
        -- UserSalary.AvgSalary,
        AvgSalary.AvgSalary
    FROM TutorialAppSchema.Users AS Users
        LEFT JOIN TutorialAppSchema.UserSalary AS UserSalary
            ON UserSalary.UserId = Users.UserId
        LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
            ON UserJobInfo.UserId = Users.UserId
        LEFT JOIN #AverageDepSalary AS AvgSalary  --User the #TEMP TABLE HERE!
            ON AvgSalary.Department = UserJobInfo.Department
        -- OUTER APPLY (
        --     SELECT UserJobInfo2.Department,
        --         AVG(UserSalary2.Salary) AS AvgSalary
        --     FROM TutorialAppSchema.Users AS Users
        --         LEFT JOIN TutorialAppSchema.UserSalary AS UserSalary2
        --             ON UserSalary2.UserId = Users.UserId
        --         LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo2
        --             ON UserJobInfo2.UserId = Users.UserId
        --         WHERE UserJobInfo2.Department = UserJobInfo.Department
        --         GROUP BY UserJobInfo2.Department
        -- ) AS AvgSalary
    WHERE Users.UserId = ISNULL(@UserId, Users.UserId)
        -- AND Users.Active = ISNULL(@Active, Users.Active)
        -- AND ISNULL(Users.Active, '') = ISNULL(@Active, Users.Active)
        -- AND ISNULL(Users.Active, 0) = ISNULL(@Active, Users.Active)
        -- AND ISNULL(Users.Active, 0) = ISNULL(@Active, ISNULL(Users.Active, 0))
        -- AND ISNULL(Users.Active, 0) = COALESCE(@Active, Users.Active, 0, 2, 3, 4, 5) -- demo for COSLESCE, for multiple parameters
        AND ISNULL(Users.Active, 0) = COALESCE(@Active, Users.Active, 0)
END

SELECT CASE WHEN NULL = NULL THEN 1 ELSE 0 END -- IF null = null
    , CASE WHEN NULL <> NULL THEN 1 ELSE 0 END
