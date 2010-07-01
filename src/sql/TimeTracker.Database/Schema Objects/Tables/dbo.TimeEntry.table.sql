CREATE TABLE [dbo].[TimeEntry]
(
[Id] [uniqueidentifier] NOT NULL,
[StartTime] [datetime] NOT NULL,
[EndTime] [datetime] NULL,
[WorkItem_id] [uniqueidentifier] NOT NULL
) ON [PRIMARY]


