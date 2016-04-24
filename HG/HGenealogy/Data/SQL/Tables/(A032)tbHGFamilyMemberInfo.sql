/*****************************************************************
* ScriptName...: tbHGFamilyMemberInfo (家族成員延申資料)
* Purpose......: Create HGFamilyMemberInfo Table
* Programmer...: Randy
* Created On...: 2016/04/13
* ****************************************************************/
/* Modification History:
* 2016/04/13  Randy First Cut
* ****************************************************************/

/*****************************************************************/
/* Start Create Table Here                                       */
/*****************************************************************/

IF OBJECT_ID('HGFamilyMemberInfo') IS NOT NULL
BEGIN
	PRINT '<<<DROP TABLE HGFamilyMemberInfo>>>'
	DROP TABLE HGFamilyMemberInfo
END
GO

CREATE TABLE HGFamilyMemberInfo
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FamilyMemberId] [nvarchar](30) NOT NULL	--家族成員編號
		CONSTRAINT DF_HGFamilyMemberInfo_FamilyMemberId DEFAULT 0,
	[InfoType] [nvarchar](10) NOT NULL	--延伸資料類別
		CONSTRAINT DF_HGFamilyMemberInfo_InfoType DEFAULT '',
	[InfoTitle] [nvarchar](30) NOT NULL --延伸資料抬頭
		CONSTRAINT DF_HGFamilyMemberInfo_InfoTitle DEFAULT '',
	[InfoContent] [nvarchar](max) NOT NULL --延伸資料內容
		CONSTRAINT DF_HGFamilyMemberInfo_InfoContent DEFAULT '',	
	[DisplayOrder] [int] NOT NULL
		CONSTRAINT DF_HGFamilyMemberInfo_DisplayOrder DEFAULT 0,
	[CreatedOnUtc] [datetime2](7) NOT NULL	
		CONSTRAINT DF_HGFamilyMemberInfo_CreatedOnUtc DEFAULT Getdate(),
	[UpdatedOnUtc] [datetime2](7) NOT NULL		
		CONSTRAINT DF_HGFamilyMemberInfo_UpdatedOnUtc DEFAULT Getdate(),
	[CreatedWho] [nvarchar](20) NOT NULL
		CONSTRAINT DF_HGFamilyMemberInfo_CreatedWho DEFAULT '',
	[UpdatedWho] [nvarchar](20) NOT NULL
		CONSTRAINT DF_HGFamilyMemberInfo_UpdatedWho DEFAULT '',
	[LastChanged] TimeStamp	--資料更新旗標
)
GO
/*****************************************************************/
/* End Create Table Here                                         */
/*****************************************************************/

/*****************************************************************/
/* Start Authirity Table Here                                    */
/*****************************************************************/
IF OBJECT_ID('HGFamilyMemberInfo') IS NULL
BEGIN
	PRINT '<<<CREATION OF TABLE HGFamilyMemberInfo FAILED>>>'
END
ELSE
BEGIN
	PRINT '<<<CREATED TABLE HGFamilyMemberInfo>>>'
	--/* Grant Permissions */
	--	PRINT '<<<CREATED TABLE HGFamilyMemberInfo Grant Authority to HG>>>'
	--GRANT INSERT ON HGFamilyMemberInfo TO HG
	--GRANT UPDATE ON HGFamilyMemberInfo TO HG
	--GRANT DELETE ON HGFamilyMemberInfo TO HG
	--GRANT SELECT ON HGFamilyMemberInfo TO HG
	--/* End Grant Permissions */
END
/*****************************************************************/
/* End Authirity Table Here                                      */
/*****************************************************************/

/*****************************************************************/
/* Primary Key --- Start                                         */
/*****************************************************************/
IF NOT OBJECT_ID('PKHGFamilyMemberInfoKey') IS NULL
BEGIN
	PRINT '<<<Dropping Primary Key PKHGFamilyMemberInfoKey(Id) From Table HGFamilyMemberInfo>>>'
	ALTER TABLE HGFamilyMemberInfo
	DROP
		CONSTRAINT PKHGFamilyMemberInfoKey
END
GO

IF OBJECT_ID('PKHGFamilyMemberInfoKey') IS NULL
BEGIN
	PRINT '<<<Adding Primary Key PKHGFamilyMemberInfoKey(Id) To Table HGFamilyMemberInfo>>>'
	SET NOCOUNT ON
	ALTER TABLE HGFamilyMemberInfo
	ADD
		CONSTRAINT PKHGFamilyMemberInfoKey PRIMARY KEY CLUSTERED (Id) WITH FILLFACTOR=75 ON [Primary]
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

