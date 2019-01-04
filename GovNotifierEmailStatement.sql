
-- =========================================
-- Create table template SQL Azure Database 
-- =========================================


IF OBJECT_ID('dbo.LBH_Ext_GovNotifyEmailStatements', 'U') IS NOT NULL
  DROP TABLE  dbo.LBH_Ext_GovNotifyEmailStatements
GO

CREATE TABLE dbo.LBH_Ext_GovNotifyEmailStatements
(
	[Id] [int] NOT NULL IDENTITY(1,1),
	[ContactId] [nvarchar](200) NOT NULL,
	[TenancyAgreementRef] [nvarchar](50) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[GovTemplateId]  [nvarchar](200) NOT NULL,
	[GovTemplateData]  [nvarchar](max) NULL,
	[EmailId]  [nvarchar](200) NULL,
	[CreatedDate] [datetime] DEFAULT(getdate()),
	[Status] int NULL,
	[StatusDescription] [nvarchar](100) NULL,
	[DebugErrorMessage] [nvarchar](max) NULL
)
GO
