MERGE [srms].[Report] AS t
USING (VALUES 
    --('E9ED3583-4A71-4F89-A96D-EF07755AF013', N'User1'),
    ('3F0A727D-B462-4176-A606-7BE446126DF3','2020-03-15T11:00:00Z','892A391A-2C37-482B-86EE-AD5247C6CE31','E9ED3583-4A71-4F89-A96D-EF07755AF013','1680E46D-6CEB-4CB4-9FAA-239D300F71A2',1)
) s ([Uid],[Timestamp],[FromId],[AboutId],[MessageId],[StatusId])
ON (t.[Uid] = s.[Uid])
WHEN NOT MATCHED THEN
    INSERT ([Uid],[Timestamp],[FromId],[AboutId],[MessageId],[StatusId]) VALUES (s.[Uid],s.[Timestamp],s.[FromId],s.[AboutId],s.[MessageId],s.[StatusId])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
GO