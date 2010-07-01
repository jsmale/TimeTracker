CREATE TABLE [dbo].[ProjectTaskType]
(
[Id] [uniqueidentifier] NOT NULL,
[Name] [nvarchar] (255) NULL,
[Project_id] [uniqueidentifier] NOT NULL,
[Task_id] [uniqueidentifier] NOT NULL,
[Type_id] [uniqueidentifier] NOT NULL
) ON [PRIMARY]


