use master 
go

EXEC master.dbo.xp_create_subdir N'c:\RdsLazylog'
go


if exists (select * from master.dbo.sysdatabases where name ='LazyLog')
begin 
	exec ('alter database LazyLog set single_user with rollback immediate')
	exec ('drop database LazyLog')
end 
go

CREATE DATABASE [LazyLog]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LazyLog', FILENAME = N'c:\RdsLazylog\LazyLog.mdf' , SIZE = 100MB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LazyLog_log', FILENAME = N'c:\RdsLazylog\LazyLog_log.ldf' , SIZE = 100MB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [LazyLog] SET RECOVERY SIMPLE WITH NO_WAIT
go

use lazylog
go


if object_id('CounterDetailsFilterInfo') is not null
drop table CounterDetailsFilterInfo
go

CREATE TABLE [dbo].[CounterDetailsFilterInfo](
	[Idx] [int] IDENTITY(1,1) NOT NULL,
	[FilterType] [varchar](10) NULL,
	[ObjectName] [varchar](1024) NULL
) ON [PRIMARY]
GO

INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN','LogicalDisk')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':Buffer Manager')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':CLR')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':Databases')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':Latches')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN','Processor')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':SQL Statistics')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':Database Replica')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':Availability Replica')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN','SQLServer:General Statistics')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','Network Adapter')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','Hyper-V')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','Per Processor Network')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','Processor Information')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','Physical Network Interface Card Activity')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN','System')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN','Memory')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN','Network Interface')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':Memory Manager')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':Locks')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('IN',':General Statistics')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','.NET CLR Memory')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','NUMA Node Memory')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','Security System-Wide Statistics')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','SQL Server 2016 XTP Phantom Processor')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','SQLAgent:SystemJobs')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','SQLServer:Availability Replica')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','SQLServer:CLR')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','SQLServer:Database Replica')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','SQLServer:Databases')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','SQLServer:Memory Broker Clerks')
INSERT INTO CounterDetailsFilterInfo ( FilterType , ObjectName)VALUES('NOTIN','SQLServer:Memory Node')
GO

if object_id('CounterDetailsAutoUpdated') is not null
drop table [CounterDetailsAutoUpdated]
go

CREATE TABLE [dbo].[CounterDetailsAutoUpdated](
	[MachineName] [varchar](100) NULL,
	[ObjectName] [varchar](100) NULL,
	[CounterName] [varchar](100) NULL,
	[CounterType] [int] NULL,
	[DefaultScale] [int] NULL,
	[InstanceName] [varchar](100) NULL,
	[InstanceIndex] [int] NULL,
	[ParentName] [varchar](1024) NULL,
	[ParentObjectId] [int] NULL,
	[TimeBaseA] [int] NULL,
	[TimeBaseB] [int] NULL,
	[IsEnabledYN] [varchar](1) NULL
) ON [PRIMARY]
GO

/****** Object:  Index [ucl_CounterDetailsAutoUpdated]    Script Date: 12/6/2018 2:25:46 PM ******/
CREATE UNIQUE CLUSTERED INDEX [ucl_CounterDetailsAutoUpdated] ON [dbo].[CounterDetailsAutoUpdated]
(
	[MachineName] ASC,
	[ObjectName] ASC,
	[CounterName] ASC,
	[InstanceName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


if object_id('CounterDetails') is not null
drop table [CounterDetails]
go

CREATE TABLE [dbo].[CounterDetails](
	[CounterID] [int] IDENTITY(1,1) NOT NULL,
	[MachineName] [varchar](100) NOT NULL,
	[ObjectName] [varchar](100) NOT NULL,
	[CounterName] [varchar](100) NOT NULL,
	[CounterType] [int] NOT NULL,
	[DefaultScale] [int] NOT NULL,
	[InstanceName] [varchar](100) NULL,
	[InstanceIndex] [int] NULL,
	[ParentName] [varchar](100) NULL,
	[ParentObjectID] [int] NULL,
	[TimeBaseA] [int] NULL,
	[TimeBaseB] [int] NULL
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [nc_CounterDetails_01] ON [dbo].[CounterDetails]
(
	[MachineName] ASC,
	[CounterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

if object_id('DisplayToIDOrigin') is not null
drop table [DisplayToIDOrigin]
go

CREATE TABLE [dbo].[DisplayToIDOrigin](
	[GUID] [uniqueidentifier] NOT NULL,
	[RunID] [int] NULL,
	[DisplayString] [varchar](100) NOT NULL,
	[LogStartTime] [char](24) NULL,
	[LogStopTime] [char](24) NULL,
	[NumberOfRecords] [int] NULL,
	[MinutesToUTC] [int] NULL,
	[TimeZoneName] [char](32) NULL
) ON [PRIMARY]
GO

/****** Object:  Index [nc_DisplayToID_01]    Script Date: 12/6/2018 5:38:47 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [nc_DisplayToIDOrigin_01] ON [dbo].[DisplayToIDOrigin]
(
	[DisplayString] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


if object_id('CounterData') is not null
drop view [CounterData]
go

create view [dbo].[CounterData]
as 

select 
1 GUID 
, 1 CounterID 
, 1 RecordIndex
, 1 CounterDateTime
, 1 CounterValue
, 1 FirstValueA
, 1 FirstValueB
, 1 SecondValueA
, 1 SecondValueB
, 1 MultiCount
go

if object_id('DisplayToID') is not null
drop view [DisplayToID]
go

create view DisplayToID 
as
with CounterDataOriginCte
as
(
select 
	min(isnull(CounterDateTime, convert(char(19), getdate(), 121))) as LogStartTime
	, convert(char(19), getdate(), 121) as LogStopTime 
	, count(distinct RecordIndex) NumberOfRecords
from dbo.CounterData with (nolock)
)
select 
	[GUID], [RunID], [DisplayString], b.[LogStartTime], b.[LogStopTime], b.NumberOfRecords, [MinutesToUTC], [TimeZoneName]
from DisplayToIDOrigin a
cross join CounterDataOriginCte b
go



if object_id('CounterDataOriginSendHist') is not null
drop table CounterDataOriginSendHist
go

create table CounterDataOriginSendHist
(
RecordIndex int 
, CounterID int 
, LastSendDate datetime
)
go

CREATE TABLE [dbo].[Config](
	[type] [nvarchar](200) NULL,
	[Name] [nvarchar](200) NULL,
	[Value] [nvarchar](1024) NULL
) ON [PRIMARY]
GO


CREATE UNIQUE CLUSTERED INDEX [cl_config] ON [dbo].[Config]
(
	[type] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

INSERT INTO Config ( type , Name , Value)VALUES('CLA','LogTypes','CDB_MSSQL')
GO
