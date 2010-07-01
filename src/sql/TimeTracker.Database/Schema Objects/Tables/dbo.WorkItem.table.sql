CREATE TABLE [dbo].[WorkItem]
(
[Id] [uniqueidentifier] NOT NULL,
[Name] [nvarchar] (255) NULL,
[ProjectTaskType_id] [uniqueidentifier] NOT NULL,
[User_id] [uniqueidentifier] NOT NULL
) ON [PRIMARY]


