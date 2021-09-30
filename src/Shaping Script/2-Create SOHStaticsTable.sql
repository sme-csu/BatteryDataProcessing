/****** Object:  View [dbo].[vw_sohstepInfo]    Script Date: 10/1/2021 12:07:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_sohstepInfo]
AS with mydata
AS
(
    select * from b_details
where filepath <is soh file path>
),lastrow as (
select b.* from (
select filepath,rowid,step,row_number() over (partition by filepath,step order by rowid desc) as rid from mydata) a
inner join mydata b on a.rowid=b.rowid and a.step=b.step and a.filepath=b.filepath
where a.rid=1
),
firstrow as (
select b.* from (
select filepath,rowid,step,row_number() over (partition by filepath,step order by rowid asc) as rid from mydata) a
inner join mydata b on a.rowid=b.rowid and a.step=b.step and a.filepath=b.filepath
where a.rid=1
)

select  a.filepath
,a.step as step
,a.rowid as firstrow_rowid
,a.status as firstrow_status
,a.ma as firstrow_ma
,a.v as firstrow_v
,a.mah as firstrow_mah
,a.mwh as firstrow_mwh
,a.rtime as firstrow_rtime
,a.timestamp as firstrow_timestamp

,b.rowid as lastrow_rowid
,b.status as lastrow_status
,b.ma as lastrow_ma
,b.v as lastrow_v
,b.mah as lastrow_mah
,b.mwh as lastrow_mwh
,b.rtime as lastrow_rtime
,b.timestamp as lastrow_timestamp


from firstrow a inner join lastrow  b on a.filepath=b.filepath  and a.step=b.step;
GO


