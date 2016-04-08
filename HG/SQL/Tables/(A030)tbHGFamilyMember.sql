/*****************************************************************
* ScriptName...: tbHGFamilyMember (家族成員基本資料)
* Purpose......: Create HGFamilyMember Table
* Programmer...: Randy
* Created On...: 2016/03/23
* ****************************************************************/
/* Modification History:
* 2016/04/01  Randy First Cut
* ****************************************************************/

/*****************************************************************/
/* Start Create Table Here                                       */
/*****************************************************************/

IF OBJECT_ID('HGFamilyMember') IS NOT NULL
BEGIN
	PRINT '<<<DROP TABLE HGFamilyMember>>>'
	DROP TABLE HGFamilyMember
END
GO

CREATE TABLE HGFamilyMember
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FamilyName] [nvarchar](30) NOT NULL	--姓
		CONSTRAINT DF_HGFamilyMember_FamilyName DEFAULT '',
	[GivenName] [nvarchar](50) NOT NULL	--名
		CONSTRAINT DF_HGFamilyMember_GivenName DEFAULT '',
	[Description] [nvarchar](Max) NOT NULL
		CONSTRAINT DF_HGFamilyMember_Description DEFAULT '',
	[PictureId] [int] NOT NULL
		CONSTRAINT DF_HGFamilyMember_PictureId DEFAULT 0,
	[IsPublic] [bit] NOT NULL				--是否公開
		CONSTRAINT DF_HGFamilyMember_IsPublic DEFAULT 0,	
	[IsPublished] [bit] NOT NULL			--是否發佈
		CONSTRAINT DF_HGFamilyMember_IsPublished DEFAULT 0,
	[IsDeleted] [bit] NOT NULL				--是否刪除
		CONSTRAINT DF_HGFamilyMember_IsDeleted DEFAULT 0,
	[DisplayOrder] [int] NOT NULL
		CONSTRAINT DF_HGFamilyMember_DisplayOrder DEFAULT 0,
	[CreatedOnUtc] [datetime2](7) NOT NULL	
		CONSTRAINT DF_HGFamilyMember_CreatedOnUtc DEFAULT Getdate(),
	[UpdatedOnUtc] [datetime2](7) NOT NULL		
		CONSTRAINT DF_HGFamilyMember_UpdatedOnUtc DEFAULT Getdate(),
	[CreatedWho] [nvarchar](20) NOT NULL
		CONSTRAINT DF_HGFamilyMember_CreatedWho DEFAULT '',
	[UpdatedWho] [nvarchar](20) NOT NULL
		CONSTRAINT DF_HGFamilyMember_UpdatedWho DEFAULT '',
	[LastChanged] TimeStamp	--資料更新旗標
)
GO
/*****************************************************************/
/* End Create Table Here                                         */
/*****************************************************************/

/*****************************************************************/
/* Start Authirity Table Here                                    */
/*****************************************************************/
IF OBJECT_ID('HGFamilyMember') IS NULL
BEGIN
	PRINT '<<<CREATION OF TABLE HGFamilyMember FAILED>>>'
END
ELSE
BEGIN
	PRINT '<<<CREATED TABLE HGFamilyMember>>>'
	--/* Grant Permissions */
	--	PRINT '<<<CREATED TABLE HGFamilyMember Grant Authority to HG>>>'
	--GRANT INSERT ON HGFamilyMember TO HG
	--GRANT UPDATE ON HGFamilyMember TO HG
	--GRANT DELETE ON HGFamilyMember TO HG
	--GRANT SELECT ON HGFamilyMember TO HG
	--/* End Grant Permissions */
END
/*****************************************************************/
/* End Authirity Table Here                                      */
/*****************************************************************/

/*****************************************************************/
/* Primary Key --- Start                                         */
/*****************************************************************/
IF NOT OBJECT_ID('PKHGFamilyMemberKey') IS NULL
BEGIN
	PRINT '<<<Dropping Primary Key PKHGFamilyMemberKey(Id) From Table HGFamilyMember>>>'
	ALTER TABLE HGFamilyMember
	DROP
		CONSTRAINT PKHGFamilyMemberKey
END
GO

IF OBJECT_ID('PKHGFamilyMemberKey') IS NULL
BEGIN
	PRINT '<<<Adding Primary Key PKHGFamilyMemberKey(Id) To Table HGFamilyMember>>>'
	SET NOCOUNT ON
	ALTER TABLE HGFamilyMember
	ADD
		CONSTRAINT PKHGFamilyMemberKey PRIMARY KEY CLUSTERED (Id) WITH FILLFACTOR=75 ON [Primary]
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

