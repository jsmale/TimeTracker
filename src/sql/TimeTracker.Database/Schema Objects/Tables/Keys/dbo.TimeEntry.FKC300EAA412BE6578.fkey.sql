﻿ALTER TABLE [dbo].[TimeEntry] ADD
CONSTRAINT [FKC300EAA412BE6578] FOREIGN KEY ([WorkItem_id]) REFERENCES [dbo].[WorkItem] ([Id])

