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
	[FamilyName] [nvarchar](30) NOT NULL --姓
		CONSTRAINT DF_HGFamilyMember_FamilyName DEFAULT '',
	[GivenName] [nvarchar](50) NOT NULL	--名
		CONSTRAINT DF_HGFamilyMember_GivenName DEFAULT '',
	[Description] [nvarchar](Max) NOT NULL
		CONSTRAINT DF_HGFamilyMember_Description DEFAULT '',	
	[FatherMemberId] [int] NOT NULL	--父
		CONSTRAINT DF_HGFamilyMember_FatherMemberId DEFAULT 0,
	[MotherMemberId] [int] NOT NULL	--母
		CONSTRAINT DF_HGFamilyMember_MotherMemberId DEFAULT 0,
	[BirthDay] [date] NULL, --生日
	[CurrentAddressId] [int] NOT NULL --目前居住地址編號
		CONSTRAINT DF_HGFamilyMember_CurrentAddressId DEFAULT 0,
	[Email] [nvarchar](500) NOT NULL --Email
		CONSTRAINT DF_HGFamilyMember_Email DEFAULT '',
	[MobilePhone] [nvarchar](30) NOT NULL --手機號碼
		CONSTRAINT DF_HGFamilyMember_MobilePhone DEFAULT '',
	[Gender] [nvarchar](1) NOT NULL --性別
		CONSTRAINT DF_HGFamilyMember_Gender DEFAULT '',
	[GenerationNo] [int] NOT NULL --世代
		CONSTRAINT DF_HGFamilyMember_GenerationNo DEFAULT 0,
	[JobDescription] [nvarchar](Max) NOT NULL --工作職業
		CONSTRAINT DF_HGFamilyMember_JobDescription DEFAULT '',
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

