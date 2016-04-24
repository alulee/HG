/*****************************************************************
* ScriptName...: tbHGAddress (地址)
* Purpose......: Create HGAddress Table
* Programmer...: Randy
* Created On...: 2016/04/13
* ****************************************************************/
/* Modification History:
* 2016/04/13  Randy First Cut
* ****************************************************************/

/*****************************************************************/
/* Start Create Table Here                                       */
/*****************************************************************/

IF OBJECT_ID('HGAddress') IS NOT NULL
BEGIN
	PRINT '<<<DROP TABLE HGAddress>>>'
	DROP TABLE HGAddress
END
GO

CREATE TABLE HGAddress
(
	[Id] int IDENTITY(1,1) NOT NULL,
	[CountryId] int NOT NULL	--國別
		CONSTRAINT DF_HGAddress_CountryId DEFAULT 0,
	[StateProvinceId] int NOT NULL
		CONSTRAINT DF_HGAddress_StateProvinceId DEFAULT 0,
	[City] nvarchar(Max) NOT NULL
		CONSTRAINT DF_HGAddress_City DEFAULT '',	
	[Address1] nvarchar(Max) NOT NULL
		CONSTRAINT DF_HGAddress_Address1 DEFAULT '',
	[Address2] nvarchar(Max) NOT NULL
		CONSTRAINT DF_HGAddress_Address2 DEFAULT '',
	[ZipPostalCode] nvarchar(100) NOT NULL
		CONSTRAINT DF_HGAddress_ZipPostalCode DEFAULT '',
	[ContactName] nvarchar(100) NOT NULL --地址聯絡人
		CONSTRAINT DF_HGAddress_ContactName DEFAULT '',
	[ContactEmail] nvarchar(100) NOT NULL --地址聯絡人Email
		CONSTRAINT DF_HGAddress_ContactEmail DEFAULT '',
	[ContactPhone] nvarchar(100) NOT NULL --地址聯絡人電話
		CONSTRAINT DF_HGAddress_ContactPhone DEFAULT '',
	[ContactFax] nvarchar(100) NOT NULL --地址聯絡人電話
		CONSTRAINT DF_HGAddress_ContactFax DEFAULT '',
	[Longitude] decimal(28,6) NULL, --地址經度 
	[Latitude] decimal(28,6) NULL, --地址緯度  
	[CustomAttributes] nvarchar(max) NULL
		CONSTRAINT DF_HGAddress_CustomAttributes DEFAULT '',

	[IsPublic] bit NOT NULL				--是否公開
		CONSTRAINT DF_HGAddress_IsPublic DEFAULT 0,	
	[IsDeleted] bit NOT NULL				--是否刪除
		CONSTRAINT DF_HGAddress_IsDeleted DEFAULT 0,
	[DisplayOrder] int NOT NULL
		CONSTRAINT DF_HGAddress_DisplayOrder DEFAULT 0,
	[CreatedOnUtc] datetime2(7) NOT NULL	
		CONSTRAINT DF_HGAddress_CreatedOnUtc DEFAULT Getdate(),
	[UpdatedOnUtc] datetime2(7) NOT NULL		
		CONSTRAINT DF_HGAddress_UpdatedOnUtc DEFAULT Getdate(),
	[CreatedWho] nvarchar(20) NOT NULL
		CONSTRAINT DF_HGAddress_CreatedWho DEFAULT '',
	[UpdatedWho] nvarchar(20) NOT NULL
		CONSTRAINT DF_HGAddress_UpdatedWho DEFAULT '',
	[LastChanged] TimeStamp	--資料更新旗標
)
GO
/*****************************************************************/
/* End Create Table Here                                         */
/*****************************************************************/

/*****************************************************************/
/* Start Authirity Table Here                                    */
/*****************************************************************/
IF OBJECT_ID('HGAddress') IS NULL
BEGIN
	PRINT '<<<CREATION OF TABLE HGAddress FAILED>>>'
END
ELSE
BEGIN
	PRINT '<<<CREATED TABLE HGAddress>>>'
	--/* Grant Permissions */
	--	PRINT '<<<CREATED TABLE HGAddress Grant Authority to HG>>>'
	--GRANT INSERT ON HGAddress TO HG
	--GRANT UPDATE ON HGAddress TO HG
	--GRANT DELETE ON HGAddress TO HG
	--GRANT SELECT ON HGAddress TO HG
	--/* End Grant Permissions */
END
/*****************************************************************/
/* End Authirity Table Here                                      */
/*****************************************************************/

/*****************************************************************/
/* Primary Key --- Start                                         */
/*****************************************************************/
IF NOT OBJECT_ID('PKHGAddressKey') IS NULL
BEGIN
	PRINT '<<<Dropping Primary Key PKHGAddressKey(Id) From Table HGAddress>>>'
	ALTER TABLE HGAddress
	DROP
		CONSTRAINT PKHGAddressKey
END
GO

IF OBJECT_ID('PKHGAddressKey') IS NULL
BEGIN
	PRINT '<<<Adding Primary Key PKHGAddressKey(Id) To Table HGAddress>>>'
	SET NOCOUNT ON
	ALTER TABLE HGAddress
	ADD
		CONSTRAINT PKHGAddressKey PRIMARY KEY CLUSTERED (Id) WITH FILLFACTOR=75 ON [Primary]
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

