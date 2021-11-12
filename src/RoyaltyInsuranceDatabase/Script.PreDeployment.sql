/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


--This code will be deleted when it fix the iss on dev envirment

---------------------------------------------------------------
--GO
--IF COL_LENGTH('dbo.UserActivityLogs', 'DeviceIp') IS  NULL
--BEGIN
--DELETE FROM UserActivityLogs
--END
--GO
--IF EXISTS (SELECT * 
--           FROM sys.foreign_keys 
--           WHERE object_id = OBJECT_ID(N'[dbo].[TrailerTypeId]') 
--             AND parent_object_id = OBJECT_ID(N'[dbo].[Cargo'))
--BEGIN
--ALTER TABLE Cargo
--DROP CONSTRAINT  FK_Cargo_VehicleInfo
--END


GO
