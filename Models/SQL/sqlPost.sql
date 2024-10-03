USE DotNetCourseDatabase

DROP TABLE IF EXISTS TutorialAppSchema.Posts

-- 
CREATE TABLE TutorialAppSchema.Posts (
    PostId INT IDENTITY(1,1), -- means first id = 1, 2nd id = 2, 3, ... if (1, 2) then 1, 3, 5, 7,..
    UserId INT,
    PostTitle NVARCHAR(255),
    PostContent NVARCHAR(MAX), -- about 4k with simbols accecption
    PostCreated DATETIME,  -- with DATETIME2 it will take up more space with more modular
    PostUpdated DATETIME
)

-- Clustered Index: 
CREATE CLUSTERED INDEX cix_Posts_UserId_PostId ON TutorialAppSchema.Posts(UserId, PostId)

SELECT [PostId],
[UserId],
[PostTitle],
[PostContent],
[PostCreated],
[PostUpdated] FROM TutorialAppSchema.Posts