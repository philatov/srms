CREATE TABLE [srms].[Lockout]
(
    [Uid] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [Timestamp] DATETIMEOFFSET NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [ManagerId] UNIQUEIDENTIFIER NOT NULL,
    [ReportId] UNIQUEIDENTIFIER NULL,
    [Till] DATETIMEOFFSET NOT NULL,
    [Comment] NVARCHAR(1000) NOT NULL,
    CONSTRAINT [FK_Lockout_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [srms].[User]([Uid]),
    CONSTRAINT [FK_Lockout_Manager_ManagerId] FOREIGN KEY ([ManagerId]) REFERENCES [srms].[Manager]([Uid]),
    CONSTRAINT [FK_Lockout_Report_ReportId] FOREIGN KEY ([ReportId]) REFERENCES [srms].[Report]([Uid])
)
