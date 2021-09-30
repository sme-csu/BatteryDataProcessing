/****** Object:  View [dbo].[v_100ADCR]    Script Date: 10/1/2021 12:09:05 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[v_100ADCR]
AS with target as (
select filepath,step, '100A '+ firstrow_status as status 
, lastrow_ma ,firstrow_rowid,firstrow_rowid-1 as prev_row from [dbo].[b_sohstepinfo]
where abs(abs(lastrow_ma)-100*1000)<100*1000*0.02 
--and filepath='cycle/cycle1/SOH1/200529t03-340.nda' 
),
first10sma as (
select filepath,step,avg(ma) as avg_ma
from p_details
where rtime<=<DCR sampling period>
group by filepath,step
)


select a.*
,b.v as s10v
,c.v as init_v
,d.avg_ma  
,(c.v-b.v)/d.avg_ma*1000 as dcr
from target a
inner join  p_details b on a.filepath=b.filepath and a.step=b.step and b.rtime=<DCR sampling period>
inner join  p_details c on a.filepath=c.filepath and a.prev_row=c.rowid
inner join first10sma d on a.filepath=d.filepath and a.step=d.step;
GO


