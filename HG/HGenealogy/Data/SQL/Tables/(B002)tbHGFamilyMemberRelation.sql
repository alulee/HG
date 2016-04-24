/*****************************************************************
* ScriptName...: tbHGFamilyMemberRelation (家族成員關聯資料表)
* Purpose......: Create HGFamilyMemberRelation Table
* Programmer...: Randy
* Created On...: 2016/04/06
* ****************************************************************/
/* Modification History:
* 2016/04/24 Randy First Cut
* ****************************************************************/

/*****************************************************************/
/* Start Create Table Here                                       */
/*****************************************************************/

IF OBJECT_ID('HGFamilyMemberRelation') IS NOT NULL
BEGIN
	PRINT '<<<DROP TABLE HGFamilyMemberRelation>>>'
	DROP TABLE HGFamilyMemberRelation
END
GO

CREATE TABLE HGFamilyMemberRelation
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FamilyMemberId] [int] NOT NULL 
		CONSTRAINT DF_HGFamilyMemberRelation_FamilyMemberId DEFAULT 0,
	[RelatedFamilyMemberId] [int] NOT NULL 
		CONSTRAINT DF_HGFamilyMemberRelation_RelatedFamilyMemberId DEFAULT 0,
	[RelationType] [varchar](10) NOT NULL
		CONSTRAINT DF_HGFamilyMemberRelation_Relation DEFAULT '',
	[CreatedOnUtc] [datetime2](7) NOT NULL	
		CONSTRAINT DF_HGFamilyMemberRelation_CreatedOnUtc DEFAULT Getdate(),
	[UpdatedOnUtc] [datetime2](7) NOT NULL		
		CONSTRAINT DF_HGFamilyMemberRelation_UpdatedOnUtc DEFAULT Getdate(),
	[CreatedWho] [nvarchar](20) NOT NULL
		CONSTRAINT DF_HGFamilyMemberRelation_CreatedWho DEFAULT '',
	[UpdatedWho] [nvarchar](20) NOT NULL
		CONSTRAINT DF_HGFamilyMemberRelation_UpdatedWho DEFAULT '',
	[LastChanged] TimeStamp	--資料更新旗標
)
GO
/*****************************************************************/
/* End Create Table Here                                         */
/*****************************************************************/

/*****************************************************************/
/* Start Authirity Table Here                                    */
/*****************************************************************/
IF OBJECT_ID('HGFamilyMemberRelation') IS NULL
BEGIN
	PRINT '<<<CREATION OF TABLE HGFamilyMemberRelation FAILED>>>'
END
ELSE
BEGIN
	PRINT '<<<CREATED TABLE HGFamilyMemberRelation>>>'
	--/* Grant Permissions */
	--	PRINT '<<<CREATED TABLE HGFamilyMemberRelation Grant Authority to HG>>>'
	--GRANT INSERT ON HGFamilyMemberRelation TO HG
	--GRANT UPDATE ON HGFamilyMemberRelation TO HG
	--GRANT DELETE ON HGFamilyMemberRelation TO HG
	--GRANT SELECT ON HGFamilyMemberRelation TO HG
	--/* End Grant Permissions */
END
/*****************************************************************/
/* End Authirity Table Here                                      */
/*****************************************************************/

/*****************************************************************/
/* Primary Key --- Start                                         */
/*****************************************************************/
IF NOT OBJECT_ID('PKHGFamilyMemberRelationKey') IS NULL
BEGIN
	PRINT '<<<Dropping Primary Key PKHGFamilyMemberRelationKey(Id) From Table HGFamilyMemberRelation>>>'
	ALTER TABLE HGFamilyMemberRelation
	DROP
		CONSTRAINT PKHGFamilyMemberRelationKey
END
GO

IF OBJECT_ID('PKHGFamilyMemberRelationKey') IS NULL
BEGIN
	PRINT '<<<Adding Primary Key PKHGFamilyMemberRelationKey(Id) To Table HGFamilyMemberRelation>>>'
	SET NOCOUNT ON
	ALTER TABLE HGFamilyMemberRelation
	ADD
		CONSTRAINT PKHGFamilyMemberRelationKey PRIMARY KEY CLUSTERED (Id) WITH FILLFACTOR=75 ON [Primary]
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

