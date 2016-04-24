/*****************************************************************
* ScriptName...: tbHGPedigreeMeta (族譜基本資料)
* Purpose......: Create HGPedigreeMeta Table
* Programmer...: Randy
* Created On...: 2016/03/23
* ****************************************************************/
/* Modification History:
* 2016/03/23  Randy First Cut
* ****************************************************************/

/*****************************************************************/
/* Start Create Table Here                                       */
/*****************************************************************/

IF OBJECT_ID('HGPedigreeMeta') IS NOT NULL
BEGIN
	PRINT '<<<DROP TABLE HGPedigreeMeta>>>'
	DROP TABLE HGPedigreeMeta
END
GO

CREATE TABLE HGPedigreeMeta
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL
		CONSTRAINT DF_HGPedigreeMeta_Title DEFAULT '',
	[Editor] [nvarchar](100) NULL,
	[Description] [nvarchar](100) NOT NULL
		CONSTRAINT DF_HGPedigreeMeta_Description DEFAULT '',
	[Image] [image] NULL,
	[PublishDate] [datetime2](7) NULL,
	[Volumes] [int] NOT NULL
		CONSTRAINT DF_HGPedigreeMeta_Volumes DEFAULT 0,
	[Pages] [int] NOT NULL
		CONSTRAINT DF_HGPedigreeMeta_Pages DEFAULT 0,
	[FamilyName] [nvarchar](30) NOT NULL
		CONSTRAINT DF_HGPedigreeMeta_FamilyName DEFAULT '',
	[OriginalAncestor] [nvarchar](30) NULL,
	[DateMoveToTaiwan] [nvarchar](10) NULL,
	[AncestorToTaiwan] [nvarchar](30) NULL,
	[OriginalAddress] [nvarchar](30) NULL,
	[TotalGenerations] [int] NOT NULL
		CONSTRAINT DF_HGPedigreeMeta_TotalGenerations DEFAULT 0,
	[GenerationToTaiwan] [int] NOT NULL
		CONSTRAINT DF_HGPedigreeMeta_GenerationToTaiwan DEFAULT 0,
	[LivingAreaInTaiwan] [nvarchar](255) NULL,
	[OriginalCollector] [nvarchar](50) NULL,
	[ContentNotes] [nvarchar](max) NULL,
	[TangName] [nvarchar](50) NULL,
	[IsPublic] [bit] NOT NULL
		CONSTRAINT DF_HGPedigreeMeta_IsPublic DEFAULT 0,
	[IsPublished] [bit] NOT NULL
		CONSTRAINT DF_HGPedigreeMeta_IsPublished DEFAULT 0,
	[IsDeleted] [bit] NOT NULL				--是否刪除
		CONSTRAINT DF_HGPedigreeMeta_IsDeleted DEFAULT 0,
	[CreatedOnUtc] [datetime2](7) NOT NULL	
		CONSTRAINT DF_HGPedigreeMeta_CreatedOnUtc DEFAULT Getdate(),
	[UpdatedOnUtc] [datetime2](7) NOT NULL		
		CONSTRAINT DF_HGPedigreeMeta_UpdatedOnUtc DEFAULT Getdate(),
	[CreatedWho] [nvarchar](20) NOT NULL
		CONSTRAINT DF_HGPedigreeMeta_CreatedWho DEFAULT '',
	[UpdatedWho] [nvarchar](20) NOT NULL
		CONSTRAINT DF_HGPedigreeMeta_UpdatedWho DEFAULT '',
	[LastChanged] TimeStamp	--資料更新旗標
)
GO
/*****************************************************************/
/* End Create Table Here                                         */
/*****************************************************************/

/*****************************************************************/
/* Start Authirity Table Here                                    */
/*****************************************************************/
IF OBJECT_ID('HGPedigreeMeta') IS NULL
BEGIN
	PRINT '<<<CREATION OF TABLE HGPedigreeMeta FAILED>>>'
END
ELSE
BEGIN
	PRINT '<<<CREATED TABLE HGPedigreeMeta>>>'
	--/* Grant Permissions */
	--	PRINT '<<<CREATED TABLE HGPedigreeMeta Grant Authority to HG>>>'
	--GRANT INSERT ON HGPedigreeMeta TO HG
	--GRANT UPDATE ON HGPedigreeMeta TO HG
	--GRANT DELETE ON HGPedigreeMeta TO HG
	--GRANT SELECT ON HGPedigreeMeta TO HG
	--/* End Grant Permissions */
END
/*****************************************************************/
/* End Authirity Table Here                                      */
/*****************************************************************/

/*****************************************************************/
/* Primary Key --- Start                                         */
/*****************************************************************/
IF NOT OBJECT_ID('PKHGPedigreeMetaKey') IS NULL
BEGIN
	PRINT '<<<Dropping Primary Key PKHGPedigreeMetaKey(Id) From Table HGPedigreeMeta>>>'
	ALTER TABLE HGPedigreeMeta
	DROP
		CONSTRAINT PKHGPedigreeMetaKey
END
GO

IF OBJECT_ID('PKHGPedigreeMetaKey') IS NULL
BEGIN
	PRINT '<<<Adding Primary Key PKHGPedigreeMetaKey(Id) To Table HGPedigreeMeta>>>'
	SET NOCOUNT ON
	ALTER TABLE HGPedigreeMeta
	ADD
		CONSTRAINT PKHGPedigreeMetaKey PRIMARY KEY CLUSTERED (Id) WITH FILLFACTOR=75 ON [Primary]
END
GO
/*****************************************************************/
/* Primary Key --- End                                           */
/*****************************************************************/

/*****************************************************************/
/* Foreign Key --- Start                                         */
/*****************************************************************/

/*****************************************************************/
/* Foreign Key --- End                                           */
/*****************************************************************/

/*****************************************************************/
/* Index Key --- Start                                           */
/*****************************************************************/

/*****************************************************************/
/* Index Key --- End                                             */
/*****************************************************************/

