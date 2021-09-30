/****** Object:  Table [dbo].[b_details]    Script Date: 10/1/2021 12:06:37 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[b_details]
(
	[filepath] [nvarchar](100) NULL,
	[rowid] [bigint] NULL,
	[status] [nvarchar](20) NULL,
	[step] [int] NULL,
	[ma] [decimal](33, 13) NULL,
	[v] [decimal](27, 13) NULL,
	[mah] [decimal](33, 13) NULL,
	[mwh] [decimal](33, 13) NULL,
	[rtime] [time](7) NULL,
	[timestamp] [datetime2](0) NULL
)
WITH
(
	DISTRIBUTION = HASH ( [filepath] ),
	CLUSTERED COLUMNSTORE INDEX
)
GO


