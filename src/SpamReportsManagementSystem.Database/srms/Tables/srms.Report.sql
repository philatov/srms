CREATE TABLE [srms].[Report]
(
    [Uid] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [Timestamp] DATETIMEOFFSET NOT NULL,
    [FromId] UNIQUEIDENTIFIER NOT NULL,
    [AboutId] UNIQUEIDENTIFIER NOT NULL,
    [MessageId] UNIQUEIDENTIFIER NULL,
    [StatusId] INT NOT NULL,
    [ManagerId] UNIQUEIDENTIFIER NULL,
    [ProcessingStarted] DATETIMEOFFSET NULL,
    [ProcessingCompleted] DATETIMEOFFSET NULL,
    CONSTRAINT [FK_Report_User_FromUserId] FOREIGN KEY ([FromId]) REFERENCES [srms].[User]([Uid]),
    CONSTRAINT [FK_Report_User_AboutUserId] FOREIGN KEY ([AboutId]) REFERENCES [srms].[User]([Uid]),
    CONSTRAINT [FK_Report_Manager_ManagerId] FOREIGN KEY ([ManagerId]) REFERENCES [srms].[Manager]([Uid]),
    CONSTRAINT [FK_Report_Message_MessageId] FOREIGN KEY ([MessageId]) REFERENCES [srms].[Message]([Uid]),
    CONSTRAINT [FK_Report_Status_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [srms].[ReportStatus]([Id])
)
