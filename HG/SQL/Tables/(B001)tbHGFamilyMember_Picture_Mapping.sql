/*****************************************************************
* ScriptName...: tbHGFamilyMember_Picture_Mapping (家族成員圖片基本資料)
* Purpose......: Create HGFamilyMember_Picture_Mapping Table
* Programmer...: Randy
* Created On...: 2016/04/06
* ****************************************************************/
/* Modification History:
* 2016/04/01  Randy First Cut
* ****************************************************************/

/*****************************************************************/
/* Start Create Table Here                                       */
/*****************************************************************/

IF OBJECT_ID('HGFamilyMember_Picture_Mapping') IS NOT NULL
BEGIN
	PRINT '<<<DROP TABLE HGFamilyMember_Picture_Mapping>>>'
	DROP TABLE HGFamilyMember_Picture_Mapping
END
GO

CREATE TABLE HGFamilyMember_Picture_Mapping
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HGFamilyMemberId] [int] NOT NULL 
		CONSTRAINT DF_HGFamilyMember_Picture_Mapping_HGFamilyMemberId DEFAULT 0,
	[PictureId] [int] NOT NULL 
		CONSTRAINT DF_HGFamilyMember_Picture_Mapping_PictureId DEFAULT 0,
	[DisplayOrder] [int] NOT NULL
		CONSTRAINT DF_HGFamilyMember_Picture_Mapping_DisplayOrder DEFAULT 0,
	[CreatedOnUtc] [datetime2](7) NOT NULL	
		CONSTRAINT DF_HGFamilyMember_Picture_Mapping_CreatedOnUtc DEFAULT Getdate(),
	[UpdatedOnUtc] [datetime2](7) NOT NULL		
		CONSTRAINT DF_HGFamilyMember_Picture_Mapping_UpdatedOnUtc DEFAULT Getdate(),
	[CreatedWho] [nvarchar](20) NOT NULL
		CONSTRAINT DF_HGFamilyMember_Picture_Mapping_CreatedWho DEFAULT '',
	[UpdatedWho] [nvarchar](20) NOT NULL
		CONSTRAINT DF_HGFamilyMember_Picture_Mapping_UpdatedWho DEFAULT '',
	[LastChanged] TimeStamp	--資料更新旗標
)
GO
/*****************************************************************/
/* End Create Table Here                                         */
/*****************************************************************/

/*****************************************************************/
/* Start Authirity Table Here                                    */
/*****************************************************************/
IF OBJECT_ID('HGFamilyMember_Picture_Mapping') IS NULL
BEGIN
	PRINT '<<<CREATION OF TABLE HGFamilyMember_Picture_Mapping FAILED>>>'
END
ELSE
BEGIN
	PRINT '<<<CREATED TABLE HGFamilyMember_Picture_Mapping>>>'
	--/* Grant Permissions */
	--	PRINT '<<<CREATED TABLE HGFamilyMember_Picture_Mapping Grant Authority to HG>>>'
	--GRANT INSERT ON HGFamilyMember_Picture_Mapping TO HG
	--GRANT UPDATE ON HGFamilyMember_Picture_Mapping TO HG
	--GRANT DELETE ON HGFamilyMember_Picture_Mapping TO HG
	--GRANT SELECT ON HGFamilyMember_Picture_Mapping TO HG
	--/* End Grant Permissions */
END
/*****************************************************************/
/* End Authirity Table Here                                      */
/*****************************************************************/

/*****************************************************************/
/* Primary Key --- Start                                         */
/*****************************************************************/
IF NOT OBJECT_ID('PKHGFamilyMember_Picture_MappingKey') IS NULL
BEGIN
	PRINT '<<<Dropping Primary Key PKHGFamilyMember_Picture_MappingKey(Id) From Table HGFamilyMember_Picture_Mapping>>>'
	ALTER TABLE HGFamilyMember_Picture_Mapping
	DROP
		CONSTRAINT PKHGFamilyMember_Picture_MappingKey
END
GO

IF OBJECT_ID('PKHGFamilyMember_Picture_MappingKey') IS NULL
BEGIN
	PRINT '<<<Adding Primary Key PKHGFamilyMember_Picture_MappingKey(Id) To Table HGFamilyMember_Picture_Mapping>>>'
	SET NOCOUNT ON
	ALTER TABLE HGFamilyMember_Picture_Mapping
	ADD
		CONSTRAINT PKHGFamilyMember_Picture_MappingKey PRIMARY KEY CLUSTERED (Id) WITH FILLFACTOR=75 ON [Primary]
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

