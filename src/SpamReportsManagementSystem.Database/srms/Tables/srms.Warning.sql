CREATE TABLE [srms].[Warning]
(
    [Uid] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [Timestamp] DATETIMEOFFSET NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [ManagerId] UNIQUEIDENTIFIER NOT NULL,
    [ReportId] UNIQUEIDENTIFIER NULL,
    [Comment] NVARCHAR(1000) NOT NULL,
    CONSTRAINT [FK_Warning_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [srms].[User]([Uid]),
    CONSTRAINT [FK_Warning_Manager_ManagerId] FOREIGN KEY ([ManagerId]) REFERENCES [srms].[Manager]([Uid]),
    CONSTRAINT [FK_Warning_Report_ReportId] FOREIGN KEY ([ReportId]) REFERENCES [srms].[Report]([Uid])
)
