DECLARE @ReportStatus TABLE([Name] NVARCHAR(200) NOT NULL);

INSERT @ReportStatus ([Name]) values (N'Created');
INSERT @ReportStatus ([Name]) values (N'Processing');
INSERT @ReportStatus ([Name]) values (N'Processed');

MERGE [srms].[ReportStatus] AS t
USING (SELECT [Name] FROM @ReportStatus) AS s
ON (t.[Name] = s.[Name])
WHEN NOT MATCHED THEN
    INSERT ([Name]) VALUES (s.[Name])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
GO