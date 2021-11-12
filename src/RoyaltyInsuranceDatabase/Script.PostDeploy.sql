/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
--Delete after prod
--IF not EXISTS(
--select  is_nullable 
--from    sys.columns 
--where   object_id = object_id('dbo.Commodity') 
--        and name = 'Name' and is_nullable = 0)
--BEGIN
--ALTER TABLE Commodity ALTER COLUMN [Name] nvarchar(50) NOT NULL
--END

--IF COL_LENGTH('dbo.Commodity', 'LoadValue') IS NOT NULL
--BEGIN
--ALTER TABLE [dbo].[Commodity] DROP COLUMN [LoadValue]
--END

--IF COL_LENGTH('dbo.UserGarages', 'FileName') IS NOT NULL
--BEGIN
--ALTER TABLE [dbo].[UserGarages] DROP COLUMN [FileName]
--END

--IF COL_LENGTH('dbo.Cargo', 'TrailerTypeId') IS NOT NULL
--BEGIN
--ALTER TABLE [dbo].[Cargo] DROP COLUMN [TrailerTypeId]
--END
--GO
--ALTER TABLE DriverInformation ALTER COLUMN [InsuredId] [int] NOT NULL
--IF COL_LENGTH('dbo.InsuredVehicle', 'TrackId') IS NOT NULL
--BEGIN
--ALTER TABLE [dbo].[InsuredVehicle] DROP COLUMN [TrackId]
--END
--IF COL_LENGTH('dbo.InsuredVehicle', 'TrailId') IS NOT NULL
--BEGIN
--ALTER TABLE [dbo].[InsuredVehicle] DROP COLUMN [TrailId]
--END
--GO
--IF not EXISTS(
--select  is_nullable 
--from    sys.columns 
--where   object_id = object_id('dbo.InsuredVehicle') 
--        and name = 'VehicleId' and is_nullable = 0)
--BEGIN
--ALTER TABLE InsuredVehicle ALTER COLUMN [VehicleId] [int] NOT NULL
--END
--GO
--IF not EXISTS(
--select  is_nullable 
--from    sys.columns 
--where   object_id = object_id('dbo.UserGarages') 
--        and name = 'FileFormatId' and is_nullable = 0)
--        BEGIN
--ALTER TABLE UserGarages ALTER COLUMN [FileFormatId] [tinyint] NOT NULL
--END
--GO
--END
GO

MERGE INTO [dbo].[FileFormats] AS Target
USING (VALUES
  (1, 'Images', 'Jpeg, Jpg, gif, bmp, png'),
  (2, 'Audio/Video','avi, wmv, mp4, mp3, mov'),
  (3, 'Documents','pdf, xlx, xlxs, xls, xlsx, doc, docx, dotx')
  
) AS Source ([Id],[Name], CodeType)
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[Name] <> Source.[Name]) THEN
    UPDATE SET
    [Name] = Source.[Name],
    [CodeType] = Source.[CodeType]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[Name], [CodeType])
    VALUES(Source.[Id],Source.[Name], Source.[CodeType])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
Go 

MERGE INTO [dbo].[DocumentType] AS Target
USING (VALUES
  (1, 'Royalty forms'),
  (2, 'Supplement'),
  (3, 'Accord Forms'),
  (4, 'Generated documents'),
  (5, 'Sharepoint shared'),
  (6, 'Storage Uploaded')
  
) AS Source ([Id],[Name])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[Name] <> Source.[Name]) THEN
    UPDATE SET
    [Name] = Source.[Name]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[Name])
    VALUES(Source.[Id],Source.[Name])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
Go

MERGE INTO [dbo].[_DatabaseVersion] AS Target
USING (VALUES
  (1, CONVERT(char(10), GetDate(),104))
) AS Source ([Id],[DbVersion])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[DbVersion] <> Source.[DbVersion]) THEN
    UPDATE SET
    [DbVersion] = Source.[DbVersion]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[DbVersion])
    VALUES(Source.[Id],Source.[DbVersion])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
Go

SET IDENTITY_INSERT dbo.Users ON
GO
IF NOT EXISTS(SELECT *
    FROM dbo.Users)
	BEGIN

INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [Password], [Iteration], [Salting], [IsActive], [PersonalAvatar], [ActivationExpiryDatetimeUtc], [ForgetPasswordCode], [ForgetPasswordDatetimeUtc], [LastModifiedUtc], [HomePhone], [CellPhone], [WorkPhone], [AdditionalPhone], [TwoFactorEnabled], [TemporaryPassword],[UserRoleId]) VALUES 
(1, N'Ruben', N'Petrosyan', N'admin@admin.com', 0x5154F7E2863C7827E57D291EA57897F28439CF715E7D9B75011FAE0E22EAAC3B5F6DDCB6F9BE3CC6DAE40521691B70F3E416589B8A1F3502CA85905A853D5585, 10000, 0x9465FBBD01D277C36E1A47FC2350E8EED78E7A62C131BC8B1B9DF586E6AACB0E4B33BE95BE2DA741B47679C27A90044424808FF5F5AC7006396062E43CCDF693, 1, NULL, NULL, NULL, NULL, CAST(N'2020-11-14T12:54:49.100' AS DateTime), NULL, N'123456', N'132456', NULL, 0, 0,1)
END
GO
SET IDENTITY_INSERT dbo.Users OFF

SET IDENTITY_INSERT dbo.Documents ON
GO

MERGE INTO [dbo].[Documents] AS Target
USING (VALUES
(1, 'CC Authorization.dotx', 'https://royaltyinsurance.sharepoint.com/sites/Testingsharedlib/_layouts/15/Doc.aspx?sourcedoc=%7BD836AD12-D40C-4211-B4F3-F9DAB1952FE2%7D&file=CC%20Authorization.dotx&action=default&mobileredirect=true',
'b86b657b-f22b-48b4-81ef-4f7b4e5f2504', '01CTUL4SASVU3NQDGUCFBLJ47Z3KYZKL7C', 2, 0, 1, 1, CAST('2021-06-03 09:55:14.303' AS DateTime), CAST('2021-06-03 09:55:14.303' AS DateTime)),
(2, 'Fleet Transportation.dotx', 'https://royaltyinsurance.sharepoint.com/sites/Testingsharedlib/_layouts/15/Doc.aspx?sourcedoc=%7B6F974A59-E3E9-44B6-A90F-C11CD032DF8E%7D&file=Fleet%20Transportation.dotx&action=default&mobileredirect=true',
'b86b657b-f22b-48b4-81ef-4f7b4e5f2504', '01CTUL4SCZJKLW72PDWZCKSD6BDTIDFX4O', 1, 0, 1, 1, CAST('2021-06-03 09:55:14.303' AS DateTime), CAST('2021-06-03 09:55:14.303' AS DateTime)),
(3, 'Accord 101.dotx', 'https://royaltyinsurance.sharepoint.com/sites/Testingsharedlib/_layouts/15/Doc.aspx?sourcedoc=%7B7C87D925-EE5F-4790-9376-AFAAC1067274%7D&file=Accord%20101.dotx&action=default&mobileredirect=true',
'b86b657b-f22b-48b4-81ef-4f7b4e5f2504', '01CTUL4SBF3GDXYX7OSBDZG5VPVLAQM4TU', 3, 0, 1, 1, CAST('2021-06-03 09:55:14.303' AS DateTime), CAST('2021-06-03 09:55:14.303' AS DateTime)),
(4, 'Acord 25.dotx', 'https://royaltyinsurance.sharepoint.com/sites/Testingsharedlib/_layouts/15/Doc.aspx?sourcedoc=%7BC27FB995-6C05-4EC4-80C2-6F71C096EBBD%7D&file=Acord%2025.dotx&action=default&mobileredirect=true',
'b86b657b-f22b-48b4-81ef-4f7b4e5f2504', '01CTUL4SEVXF74EBLMYRHIBQTPOHAJN255', 3, 0, 1, 1, CAST('2021-06-03 09:55:14.303' AS DateTime), CAST('2021-06-03 09:55:14.303' AS DateTime)),
(5, 'Acord 36.dotx', 'https://royaltyinsurance.sharepoint.com/sites/Testingsharedlib/_layouts/15/Doc.aspx?sourcedoc=%7BDC8460B8-861F-4190-B93B-DCFC16668702%7D&file=Acord%2036.dotx&action=default&mobileredirect=true',
'b86b657b-f22b-48b4-81ef-4f7b4e5f2504', '01CTUL4SFYMCCNYH4GSBA3SO647QLGNBYC', 3, 0, 1, 1, CAST('2021-06-03 09:55:14.303' AS DateTime), CAST('2021-06-03 09:55:14.303' AS DateTime))
)
AS Source ([Id], [DocumentName], [Path], [GroupId], [DriveItemId], [DocumentTypeId],[IsDeleted], [CreatedBy], [UpdatedBy], [CreateDatetimeUtc], [LastModifiedUtc])
ON (Target.[Id] = Source.[Id]) 
WHEN MATCHED AND (Target.[DocumentName] <> Source.[DocumentName] OR Target.[DocumentTypeId] <> Source.[DocumentTypeId]) THEN
    UPDATE SET
    [DocumentName] = Source.[DocumentName],
    [Path] = Source.[Path],
    [GroupId] = Source.[GroupId],
    [DriveItemId] = Source.[DriveItemId],
    [DocumentTypeId] = Source.[DocumentTypeId],
    [IsDeleted] = Source.[IsDeleted],
    [CreatedBy] = Source.[CreatedBy],
    [UpdatedBy]= Source.[UpdatedBy], 
    [CreateDatetimeUtc]= Source.[CreateDatetimeUtc], 
    [LastModifiedUtc]= Source.[LastModifiedUtc]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id], [DocumentName], [Path], [GroupId], [DriveItemId], [DocumentTypeId],[IsDeleted], [CreatedBy], [UpdatedBy], [CreateDatetimeUtc], [LastModifiedUtc])
    VALUES(Source.[Id], Source.[DocumentName], Source.[Path], Source.[GroupId], Source.[DriveItemId], Source.[DocumentTypeId], Source.[IsDeleted], Source.[CreatedBy], Source.[UpdatedBy], Source.[CreateDatetimeUtc], Source.[LastModifiedUtc]);
GO
SET IDENTITY_INSERT dbo.Documents OFF
GO

SET IDENTITY_INSERT dbo.UserPhones ON
GO

MERGE INTO [dbo].[UserPhones] AS Target
USING (VALUES
(1, '8888888888', '192.168.1.27', 1,
'555', 1, 1, CAST('2021-06-03 09:55:14.303' AS DateTime), 
CAST('2021-06-03 09:55:14.303' AS DateTime))) 
AS Source ([Id], [PhoneNumber], [IpAddress], [PhoneOwnerId], [Extension], [CreatedBy], [UpdatedBy], [CreateDatetimeUtc], [LastModifiedUtc])
ON (Target.[Id] = Source.[Id]) 
WHEN MATCHED AND (Target.[PhoneNumber] <> Source.[PhoneNumber]) THEN
    UPDATE SET
    [PhoneNumber] = Source.[PhoneNumber],
    [IpAddress] = Source.[IpAddress],
    [PhoneOwnerId] = Source.[PhoneOwnerId],
    [Extension] = Source.[Extension],
    [CreatedBy] = Source.[CreatedBy],
    [UpdatedBy]= Source.[UpdatedBy], 
    [CreateDatetimeUtc]= Source.[CreateDatetimeUtc], 
    [LastModifiedUtc]= Source.[LastModifiedUtc]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id], [PhoneNumber], [IpAddress], [PhoneOwnerId], [Extension], [CreatedBy], [UpdatedBy], [CreateDatetimeUtc], [LastModifiedUtc])
    VALUES(Source.[Id], Source.[PhoneNumber], Source.[IpAddress], Source.[PhoneOwnerId], Source.[Extension], Source.[CreatedBy], Source.[UpdatedBy], Source.[CreateDatetimeUtc], Source.[LastModifiedUtc])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
GO
SET IDENTITY_INSERT dbo.UserPhones OFF
GO

SET IDENTITY_INSERT dbo.Agencies ON
GO
MERGE INTO [dbo].[Agencies] AS Target
USING (VALUES
  (1, N'Royalty Insurance Services Inc.', N'14545 Victory Blvd', N'CA', N'Van Nuys', N'91411',
  N'+1 818-989-8999', N'+1 818-989-8999', 1, 1, --user id of Ruben, need to be replace to 1 on stage, 74 for dev
  CAST(N'2020-11-09T18:39:06.720' AS DateTime), 
  CAST(N'2020-11-09T18:39:06.720' AS DateTime))) AS Source ([Id], [Name], [Address], [State], [City], [Zip],[PhoneNumber], [FaxNumber], [CreatedBy], [UpdatedBy], [CreateDatetimeUtc], [LastModifiedUtc])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[Name] <> Source.[Name]) THEN
    UPDATE SET
    [Name] = Source.[Name],
    [Address] = Source.[Address],
    [State] = Source.[State],
    [City] = Source.[City],
    [Zip] =  Source.[Zip],
    [PhoneNumber] = Source.[PhoneNumber],
    [FaxNumber] = Source.[FaxNumber],
    [CreatedBy] = Source.[CreatedBy],
    [UpdatedBy]= Source.[UpdatedBy], 
    [CreateDatetimeUtc]= Source.[CreateDatetimeUtc], 
    [LastModifiedUtc]= Source.[LastModifiedUtc]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id], [Name], [Address], [State], [City], [Zip], [PhoneNumber], [FaxNumber], [CreatedBy], [UpdatedBy], [CreateDatetimeUtc], [LastModifiedUtc])
    VALUES(Source.[Id], Source.[Name], Source.[Address], Source.[State], Source.[City], Source.[Zip],  Source.[PhoneNumber], Source.[FaxNumber], Source.[CreatedBy], Source.[UpdatedBy], Source.[CreateDatetimeUtc], Source.[LastModifiedUtc])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
SET IDENTITY_INSERT dbo.Agencies OFF
GO
SET IDENTITY_INSERT dbo.LegalStatuses ON
MERGE INTO [dbo].[LegalStatuses] AS Target
USING (VALUES
  (1,N'Individual'),
  (2,N'Corporation')
) AS Source ([Id],[Name])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[Name] <> Source.[Name]) THEN
    UPDATE SET
    [Name] = Source.[Name]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[Name])
    VALUES(Source.[Id],Source.[Name])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
    SET IDENTITY_INSERT dbo.LegalStatuses OFF

    SET IDENTITY_INSERT dbo.Coverages ON
    GO

    MERGE INTO [dbo].[Coverages] AS Target
USING (VALUES
(1, N'AUTO LIABILITY',1000000),
(2, N'UNINSURED MOTORIST',10),
(3, N'CARGO LIMIT',100000),
(4, N'REEFER BREAKDOWN',20),
(5, N'PD DEDUCTIBLES',1000),
(6, N'TRAILER INTERCHANGE',300),
(7, N'GENERAL LIABILITY',2000),
(8, N'HIRED AUTO',0),
(9, N'NON-TRUCKING LIABILITY',0),
(10, N'OTHER',0)

) AS Source ([Id],[CoverageType], [CoverageLimit])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[CoverageType] <> Source.[CoverageType]) THEN
    UPDATE SET
    [CoverageType] = Source.[CoverageType],
    [CoverageLimit] = Source.[CoverageLimit]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[CoverageType], [CoverageLimit])
    VALUES(Source.[Id], Source.[CoverageType], Source.[CoverageLimit])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
    SET IDENTITY_INSERT dbo.Coverages OFF
    GO
    SET IDENTITY_INSERT dbo.UserStatus ON
    GO

MERGE INTO [dbo].[UserStatus] AS Target
USING (VALUES
  (1,N'Online'),
  (2,N'Offline'),
  (3,N'Away'),
  (4,N'Don''t disturbe'),
  (5, N'On the Phone'),
  (6, N'Custom')
) AS Source ([Id],[Name])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[Name] <> Source.[Name]) THEN
    UPDATE SET
    [Name] = Source.[Name]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[Name])
    VALUES(Source.[Id],Source.[Name])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
    GO
    SET IDENTITY_INSERT dbo.UserStatus OFF
    GO
    
    SET IDENTITY_INSERT dbo.InsuredStatuses ON
    GO
MERGE INTO [dbo].[InsuredStatuses] as Target 
USING (VALUES
(1,N'Prospect'),
(2,N'Insured'),
(3,N'Canceled'))
as Source ([Id],[Name])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[Name] <> Source.[Name]) THEN
    UPDATE SET
    [Name] = Source.[Name]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[Name])
    VALUES(Source.[Id],Source.[Name])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
    GO
    SET IDENTITY_INSERT dbo.InsuredStatuses OFF
    GO

    SET IDENTITY_INSERT dbo.AgentTaskStatuses ON
    GO
MERGE INTO [dbo].[AgentTaskStatuses] as Target 
USING (VALUES
(1,N'To Do', '2021-03-11 07:31:21.510'),
(2,N'In Progress', '2021-03-11 07:31:21.510'),
(3,N'Follow Up', '2021-03-11 07:31:21.510'),
(4,N'Canceled', '2021-03-11 07:31:21.510'),
(5,N'Completed', '2021-03-11 07:31:21.510'))
as Source ([Id],[Name], [CreateDatetimeUtc])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[Name] <> Source.[Name]) THEN
    UPDATE SET
    [Name] = Source.[Name],
    [CreateDatetimeUtc] = Source.[CreateDatetimeUtc]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[Name], [CreateDatetimeUtc])
    VALUES(Source.[Id],Source.[Name], Source.[CreateDatetimeUtc])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
    GO
    SET IDENTITY_INSERT dbo.AgentTaskStatuses OFF
    GO
MERGE INTO [dbo].[AgentTaskTypes] as Target 
USING (VALUES
(1,N'Endorsement'),
(2,N'New Venture'),
(3,N'Renew'))
as Source ([Id],[Name])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[Name] <> Source.[Name]) THEN
    UPDATE SET
    [Name] = Source.[Name]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[Name])
    VALUES(Source.[Id],Source.[Name])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
    GO

    SET IDENTITY_INSERT dbo.Roles ON
    GO
MERGE INTO [dbo].[Roles] as Target 
USING (VALUES
(1,N'SuperAdmin', 1),
(2,N'Agent', 2),
(3,N'Assistant',  3 ),
(4,N'Underwriter',4 ),
(5,N'Marketing', 5),
(6,N'IT',6),
(7,N'Accounting',7),
(8,N'Reviewer',8))
as Source ([Id],[Name],[Type])

ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[Name] <> Source.[Name]) THEN
    UPDATE SET
    [Name] = Source.[Name]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[Name],[Type])
    VALUES(Source.[Id],Source.[Name],Source.[Type])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
    GO
    SET IDENTITY_INSERT dbo.Roles OFF
    GO


    SET IDENTITY_INSERT dbo.LocationType ON
    GO

MERGE INTO [dbo].[LocationType] AS Target
USING (VALUES
  (1,N'City'),
  (2,N'State'),
  (3,N'Zip'),
  (4,N'Country'),
  (5, N'AreaCode')
) AS Source ([Id],[LocationType])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[LocationType] <> Source.[LocationType]) THEN
    UPDATE SET
    [LocationType] = Source.[LocationType]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[LocationType])
    VALUES(Source.[Id],Source.[LocationType])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
    GO
    SET IDENTITY_INSERT dbo.LocationType OFF
    GO

    SET IDENTITY_INSERT dbo.BasicAlert ON
    GO

MERGE INTO [dbo].[BasicAlert] AS Target
USING (VALUES
  (1,N'Unsafe Driving'),
  (2,N'Hours Of Service'),
  (3,N'Driver Fitness'),
  (4,N'Controlled Substances'),
  (5, N'Vehicle Maintenance'),
  (6, N'Hazmat Related'),
  (7, N'Crash Indicator')
) AS Source ([Id],[BasicAlert])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[BasicAlert] <> Source.[BasicAlert]) THEN
    UPDATE SET
    [BasicAlert] = Source.[BasicAlert]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[BasicAlert])
    VALUES(Source.[Id],Source.[BasicAlert])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
    GO
    SET IDENTITY_INSERT dbo.BasicAlert OFF
    GO

     SET IDENTITY_INSERT dbo.[Gvwr] ON
    GO

MERGE INTO [dbo].[Gvwr] AS Target
USING (VALUES
  (1,N'Class 1: <6,000 lbs'),
  (2,N'Class 2: 6,001-10,000 lbs'),
  (3,N'Class 3: 10,001-14,000 lbs'),
  (4,N'Class 4: 14,001-16,000 lbs'),
  (5,N'Class 5: 16,001-19,500 lbs'),
  (6,N'Class 6: 19,501-26,000 lbs'),
  (7,N'Class 7: 26,001-33,000 lbs'),
  (8,N'Class 8: >33,001 lbs')
) AS Source ([Id],[ClassDescription])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[ClassDescription] <> Source.[ClassDescription]) THEN
    UPDATE SET
    [ClassDescription] = Source.[ClassDescription]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[ClassDescription])
    VALUES(Source.[Id],Source.[ClassDescription])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
    GO
    SET IDENTITY_INSERT dbo.[Gvwr] OFF
    GO

    SET IDENTITY_INSERT dbo.[OperationType] ON
    GO
MERGE INTO [dbo].[OperationType] as Target 
USING (VALUES
(1,N'hhold', 'HHG'),
(2,N'Frt', 'Property'),
(3,N'pas', 'Passenger'))
as Source ([Id],[Type], [Value])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[Type] <> Source.[Type]) THEN
    UPDATE SET
    [Type] = Source.[Type],
    [Value] = Source.[Value]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[Type], [Value])
    VALUES(Source.[Id],Source.[Type], Source.[Value])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
    GO
    SET IDENTITY_INSERT dbo.[OperationType] OFF
    GO

        SET IDENTITY_INSERT dbo.[CommonAuthTypes] ON
    GO
MERGE INTO [dbo].[CommonAuthTypes] as Target 
USING (VALUES
(1,N'Active/Pending', 'a'),
(2,N'Inactive', 'i'),
(3,N'None', 'n'))
as Source ([Id],[Name], [Value])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[Name] <> Source.[Name]) THEN
    UPDATE SET
    [Name] = Source.[Name],
    [Value] = Source.[Value]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[Name], [Value])
    VALUES(Source.[Id],Source.[Name], Source.[Value])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
    GO
    SET IDENTITY_INSERT dbo.[CommonAuthTypes] OFF
    GO



-- ================================================
-- Template generated from Template Explorer using:
-- Create foreign keys (New Menu).SQL
--
-- ================================================
GO
SET IDENTITY_INSERT dbo.[CallTypes] ON
    GO

;
MERGE INTO [dbo].[CallTypes] AS Target
USING (VALUES
  (1,N'Incoming'),
  (2,N'Outgoing'),
  (3,N'Missed'),
  (4,N'Established'),
  (5, N'Terminated'),
  (6, N'Answered')
) AS Source ([Id],[Name])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[Name] <> Source.[Name]) THEN
    UPDATE SET
    [Name] = Source.[Name]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[Name])
    VALUES(Source.[Id],Source.[Name])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
    GO
   
   SET IDENTITY_INSERT dbo.[CallTypes] OFF
    GO

       SET IDENTITY_INSERT dbo.[States] ON
    GO

    MERGE INTO [dbo].[States] AS Target
USING (VALUES
  (1,N'AL'),
(2,N'AK'),
(3,N'AZ'),
(4,N'AR'),
(5,N'CA'),
(6,N'CO'),
(7,N'CT'),
(8,N'DE'),
(9,N'DC'),
(10,N'FL'),
(11,N'GA'),
(12,N'HI'),
(13,N'ID'),
(14,N'IL'),
(15,N'IN'),
(16,N'IA'),
(17,N'KS'),
(18,N'KY'),
(19,N'LA'),
(20,N'ME'),
(21,N'MD'),
(22,N'MA'),
(23,N'MI'),
(24,N'MN'),
(25,N'MS'),
(26,N'MO'),
(27,N'MT'),
(28,N'NE'),
(29,N'NV'),
(30,N'NH'),
(31,N'NJ'),
(32,N'NM'),
(33,N'NY'),
(34,N'NC'),
(35,N'ND'),
(36,N'OH'),
(37,N'OK'),
(38,N'OR'),
(39,N'PA'),
(40,N'RI'),
(41,N'SC'),
(42,N'SD'),
(43,N'TN'),
(44,N'TX'),
(45,N'VT'),
(46,N'VA'),
(47,N'WA'),
(48,N'WV'),
(49,N'WI'),
(50,N'WY')
) AS Source ([Id],[Name])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[Name] <> Source.[Name]) THEN
    UPDATE SET
    [Name] = Source.[Name]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[Name])
    VALUES(Source.[Id],Source.[Name])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
    GO
    SET IDENTITY_INSERT dbo.[States] OFF
    GO

    SET IDENTITY_INSERT dbo.[AgaveTransactionType] ON
    GO
    MERGE INTO [dbo].[AgaveTransactionType] AS Target
USING (VALUES
  (1,N'Sale'),
  (2,N'Refund'),
  (3,N'PreResponse'),
  (4,N'eCheckSale')
) AS Source ([Id],[Name])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[Name] <> Source.[Name]) THEN
    UPDATE SET
    [Name] = Source.[Name]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[Name])
    VALUES(Source.[Id],Source.[Name])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
    GO
    SET IDENTITY_INSERT dbo.AgaveTransactionType OFF
    GO


    SET IDENTITY_INSERT dbo.[AchTypes] ON
    GO
MERGE INTO [dbo].[AchTypes] as Target 
USING (VALUES
(1,N'PC', 'for personal checking'),
(2,N'PS', 'for personal savings'),
(3,N'PL', ''),
(4,N'BC','for business checking'),
(5,N'BS',' for business savings'),
(6,N'BL',''))
as Source ([Id],[Type], [Description])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[Type] <> Source.[Type]) THEN
    UPDATE SET
    [Type] = Source.[Type],
    [Description] = Source.[Description]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[Type], [Description])
    VALUES(Source.[Id],Source.[Type], Source.[Description])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
    GO
    SET IDENTITY_INSERT dbo.[AchTypes] OFF
    GO


    SET IDENTITY_INSERT dbo.[CoverageTypes] ON
    GO
    MERGE INTO [dbo].[CoverageTypes] AS Target
USING (VALUES
  (1,N'AutoLiability'),
  (2,N'UnInsuredMotorist'),
  (3,N'CargoLimit'),
  (4,N'ReeferBreakdown'),
  (5,N'PdDeductibles'),
  (6,N'TrailerInterchange'),
  (7,N'GeneralLiability'),
  (8,N'HiredAuto'),
  (9,N'NonTrackingLiability'),
  (10,N'Other')
) AS Source ([Id],[Name])
ON (Target.[Id] = Source.[Id])
WHEN MATCHED AND (Target.[Name] <> Source.[Name]) THEN
    UPDATE SET
    [Name] = Source.[Name]
WHEN NOT MATCHED BY TARGET THEN
    INSERT([Id],[Name])
    VALUES(Source.[Id],Source.[Name])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
    GO
    SET IDENTITY_INSERT dbo.[CoverageTypes] OFF
    GO



IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_Agencies_CreatedByUsers')
   AND parent_object_id = OBJECT_ID(N'dbo.Agencies')
)
BEGIN
ALTER TABLE [dbo].[Agencies] ADD  CONSTRAINT [FK_Agencies_CreatedByUsers] FOREIGN KEY([CreatedBy])
REFERENCES [Users] ([Id])
END

GO
ALTER TABLE [dbo].[Agencies] CHECK CONSTRAINT [FK_Agencies_CreatedByUsers]
GO

ALTER TABLE [dbo].[InsuredCoverages] ADD  CONSTRAINT [DF_InsuredCoverages_CreateDatetimeUtc]  DEFAULT (getutcdate()) FOR [CreateDatetimeUtc]
GO
ALTER TABLE [dbo].[InsuredCoverages] ADD  CONSTRAINT [DF_InsuredCoverages_LastModifiedUtc]  DEFAULT (getutcdate()) FOR [LastModifiedUtc]
GO
ALTER TABLE [dbo].[InsuredCoverages]  WITH CHECK ADD  CONSTRAINT [FK_InsuredCoverages_Coverages] FOREIGN KEY([CoverageId])
REFERENCES [dbo].[Coverages] ([Id])
GO
ALTER TABLE [dbo].[InsuredCoverages] CHECK CONSTRAINT [FK_InsuredCoverages_Coverages]
GO
ALTER TABLE [dbo].[InsuredCoverages]  WITH CHECK ADD  CONSTRAINT [FK_InsuredCoverages_Insureds] FOREIGN KEY([InsuredId])
REFERENCES [dbo].[Insureds] ([Id])
GO
ALTER TABLE [dbo].[InsuredCoverages] CHECK CONSTRAINT [FK_InsuredCoverages_Insureds]
GO
ALTER TABLE [dbo].[AgentTasks] ADD  CONSTRAINT [DF_AgentTasks_CreateDatetimeUtc]  DEFAULT (getutcdate()) FOR [CreateDatetimeUtc]
GO
ALTER TABLE [dbo].[AgentTaskStatuses] ADD  CONSTRAINT [DF_AgentTaskStatuses_CreateDatetimeUtc]  DEFAULT (getutcdate()) FOR [CreateDatetimeUtc]
GO

ALTER TABLE [dbo].[AgentTasks] ADD  CONSTRAINT [DF_AgentTasks_LastModifiedUtc]  DEFAULT (getutcdate()) FOR [LastModifiedUtc]
GO
ALTER TABLE [dbo].[Insureds] ADD  CONSTRAINT [DF_Insureds_CreateDatetimeUtc]  DEFAULT (getutcdate()) FOR [CreateDatetimeUtc]
GO
ALTER TABLE [dbo].[Insureds] ADD  CONSTRAINT [DF_Insureds_LastModifiedUtc]  DEFAULT (getutcdate()) FOR [LastModifiedUtc]
GO
IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_Agencies_UpdatedByUsers')
   AND parent_object_id = OBJECT_ID(N'dbo.Agencies')
)
BEGIN
ALTER TABLE [dbo].[Agencies]  WITH CHECK ADD  CONSTRAINT [FK_Agencies_UpdatedByUsers] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
END

GO
ALTER TABLE [dbo].[Agencies] CHECK CONSTRAINT [FK_Agencies_UpdatedByUsers]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_Insureds_MailingCities')
   AND parent_object_id = OBJECT_ID(N'dbo.Insureds')
)
BEGIN
	ALTER TABLE [dbo].[Insureds]  WITH CHECK ADD  CONSTRAINT [FK_Insureds_MailingCities] FOREIGN KEY([MailingCityId])
REFERENCES [dbo].[Cities] ([Id])
END
GO

ALTER TABLE [dbo].[Insureds] CHECK CONSTRAINT [FK_Insureds_MailingCities]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_Insureds_GaragingCities')
   AND parent_object_id = OBJECT_ID(N'dbo.Insureds')
)
BEGIN
ALTER TABLE [dbo].[Insureds]  WITH CHECK ADD  CONSTRAINT [FK_Insureds_GaragingCities] FOREIGN KEY([GaragingCityId])
REFERENCES [dbo].[Cities] ([Id])
END

GO
ALTER TABLE [dbo].[Insureds] CHECK CONSTRAINT [FK_Insureds_GaragingCities]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_Insureds_LegalStatuses')
   AND parent_object_id = OBJECT_ID(N'dbo.Insureds')
)
BEGIN
ALTER TABLE [dbo].[Insureds] ADD  CONSTRAINT [FK_Insureds_LegalStatuses] FOREIGN KEY([LegalStatusId])
REFERENCES [dbo].[LegalStatuses] ([Id])
END

GO
ALTER TABLE [dbo].[Insureds] CHECK CONSTRAINT [FK_Insureds_LegalStatuses]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_Insureds_MailingStates')
   AND parent_object_id = OBJECT_ID(N'dbo.Insureds')
)
BEGIN
ALTER TABLE [dbo].[Insureds]  WITH CHECK ADD  CONSTRAINT [FK_Insureds_MailingStates] FOREIGN KEY([MailingStateId])
REFERENCES [dbo].[States] ([Id])
END

GO
ALTER TABLE [dbo].[Insureds] CHECK CONSTRAINT [FK_Insureds_MailingStates]
GO

ALTER TABLE [dbo].[Insureds]  WITH CHECK ADD  CONSTRAINT [FK_Insureds_InsuredStatuses] FOREIGN KEY([InsuredStatusId])
REFERENCES [dbo].[InsuredStatuses] ([Id])
GO
ALTER TABLE [dbo].[Insureds] CHECK CONSTRAINT [FK_Insureds_InsuredStatuses]
GO
IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_Insureds_GaragingStates')
   AND parent_object_id = OBJECT_ID(N'dbo.Insureds')
)
BEGIN
ALTER TABLE [dbo].[Insureds]  WITH CHECK ADD  CONSTRAINT [FK_Insureds_GaragingStates] FOREIGN KEY([GaragingStateId])
REFERENCES [dbo].[States] ([Id])
END

GO
ALTER TABLE [dbo].[Insureds] CHECK CONSTRAINT [FK_Insureds_GaragingStates]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_Insureds_UpdatedByUsers')
   AND parent_object_id = OBJECT_ID(N'dbo.Insureds')
)
BEGIN
ALTER TABLE [dbo].[Insureds]  WITH CHECK ADD  CONSTRAINT [FK_Insureds_UpdatedByUsers] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
END

GO
ALTER TABLE [dbo].[Insureds] CHECK CONSTRAINT [FK_Insureds_UpdatedByUsers]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_Insureds_CreateByUsers')
   AND parent_object_id = OBJECT_ID(N'dbo.Insureds')
)
BEGIN
ALTER TABLE [dbo].[Insureds]  WITH CHECK ADD  CONSTRAINT [FK_Insureds_CreateByUsers] FOREIGN KEY([CreateBy])
REFERENCES [dbo].[Users] ([Id])
END

GO
ALTER TABLE [dbo].[Insureds] CHECK CONSTRAINT [FK_Insureds_CreateByUsers]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_Insureds_GaragingZipCode')
   AND parent_object_id = OBJECT_ID(N'dbo.Insureds')
)
BEGIN
ALTER TABLE [dbo].[Insureds]  WITH CHECK ADD  CONSTRAINT [FK_Insureds_GaragingZipCode] FOREIGN KEY([GaragingZipCodeId])
REFERENCES [dbo].[ZipCode] ([Id])
END

GO
ALTER TABLE [dbo].[Insureds] CHECK CONSTRAINT [FK_Insureds_GaragingZipCode]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_Insureds_MailingZipCode')
   AND parent_object_id = OBJECT_ID(N'dbo.Insureds')
)
BEGIN
ALTER TABLE [dbo].[Insureds]  WITH CHECK ADD  CONSTRAINT [FK_Insureds_MailingZipCode] FOREIGN KEY([MailingZipCodeId])
REFERENCES [dbo].[ZipCode] ([Id])
END

GO
ALTER TABLE [dbo].[Insureds] CHECK CONSTRAINT [FK_Insureds_MailingZipCode]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_Cities_States')
   AND parent_object_id = OBJECT_ID(N'dbo.Cities')
)
BEGIN
ALTER TABLE [dbo].[Cities]  WITH CHECK ADD  CONSTRAINT [FK_Cities_States] FOREIGN KEY([StateId])
REFERENCES [dbo].[States] ([Id])
END

GO
ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_Cities_States]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_ZipCode_Cities')
   AND parent_object_id = OBJECT_ID(N'dbo.ZipCode')
)
BEGIN
ALTER TABLE [dbo].[ZipCode]  WITH CHECK ADD  CONSTRAINT [FK_ZipCode_Cities] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([Id])
END

GO
ALTER TABLE [dbo].[ZipCode] CHECK CONSTRAINT [FK_ZipCode_Cities]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_GroupMembers_Members')
   AND parent_object_id = OBJECT_ID(N'dbo.GroupMembers')
)
BEGIN
ALTER TABLE [dbo].[GroupMembers]  WITH CHECK ADD  CONSTRAINT [FK_GroupMembers_Members] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Users] ([Id])
END

GO
ALTER TABLE [dbo].[GroupMembers] CHECK CONSTRAINT [FK_GroupMembers_Members]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_GroupMembers_Groups')
   AND parent_object_id = OBJECT_ID(N'dbo.GroupMembers')
)
BEGIN
ALTER TABLE [dbo].[GroupMembers]  WITH CHECK ADD  CONSTRAINT [FK_GroupMembers_Groups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([Id])
END

GO
ALTER TABLE [dbo].[GroupMembers] CHECK CONSTRAINT [FK_GroupMembers_Groups]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_Groups_Users')
   AND parent_object_id = OBJECT_ID(N'dbo.Groups')
)
BEGIN
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_Groups_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
END

GO
ALTER TABLE [dbo].[Groups] CHECK CONSTRAINT [FK_Groups_Users]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_Groups_UpdatedBy')
   AND parent_object_id = OBJECT_ID(N'dbo.Groups')
)
BEGIN
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_Groups_UpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
END

GO
ALTER TABLE [dbo].[Groups] CHECK CONSTRAINT [FK_Groups_UpdatedBy]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_Messages_Groups')
   AND parent_object_id = OBJECT_ID(N'dbo.Messages')
)
BEGIN
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Groups] FOREIGN KEY([RecipientGroupId])
REFERENCES [dbo].[Groups] ([Id])
END

GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Messages] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Messages] ([Id])
GO


GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Groups]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_Messages_Users')
   AND parent_object_id = OBJECT_ID(N'dbo.Messages')
)
BEGIN
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Users] FOREIGN KEY([SenderId])
REFERENCES [dbo].[Users] ([Id])
END

GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Users]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_UnReadMessagesMessages')
   AND parent_object_id = OBJECT_ID(N'dbo.UnReadMessages')
)
BEGIN
ALTER TABLE [dbo].[UnReadMessages]  WITH CHECK ADD  CONSTRAINT [FK_UnReadMessages_Messages] FOREIGN KEY([MessageId])
REFERENCES [dbo].[Messages] ([Id])
END
GO
ALTER TABLE [dbo].[UnReadMessages] CHECK CONSTRAINT [FK_UnReadMessages_Messages]
GO

ALTER TABLE [dbo].[UnReadMessages]  WITH CHECK ADD  CONSTRAINT [FK_UnReadMessages_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([Id])
GO
ALTER TABLE [dbo].[UnReadMessages] CHECK CONSTRAINT [FK_UnReadMessages_Group]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_UnReadMessages_Users')
   AND parent_object_id = OBJECT_ID(N'dbo.UnReadMessages')
)
BEGIN
ALTER TABLE [dbo].[UnReadMessages]  WITH CHECK ADD  CONSTRAINT [FK_UnReadMessages_Users] FOREIGN KEY([ReadUserId])
REFERENCES [dbo].[Users] ([Id])
END

GO
ALTER TABLE [dbo].[UnReadMessages] CHECK CONSTRAINT [FK_UnReadMessages_Users]
GO
ALTER TABLE [dbo].[UnReadMessages]  WITH CHECK ADD  CONSTRAINT [FK_UnReadMessages_SendUsers] FOREIGN KEY([SendUserId])
REFERENCES [dbo].[Users] ([Id])

GO
ALTER TABLE [dbo].[UnReadMessages] CHECK CONSTRAINT [FK_UnReadMessages_SendUsers]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_UsersProfiles_Users')
   AND parent_object_id = OBJECT_ID(N'dbo.UsersProfiles')
)
BEGIN
ALTER TABLE [dbo].[UsersProfiles]  WITH CHECK ADD  CONSTRAINT [FK_UsersProfiles_Users] FOREIGN KEY([Id])
REFERENCES [dbo].[Users] ([Id])
END

GO
ALTER TABLE [dbo].[UsersProfiles] CHECK CONSTRAINT [FK_UsersProfiles_Users]
GO

IF NOT EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'FK_UsersProfiles_UserStatus')
   AND parent_object_id = OBJECT_ID(N'dbo.UsersProfiles')
)
BEGIN
ALTER TABLE [dbo].[UsersProfiles]  WITH CHECK ADD  CONSTRAINT [FK_UsersProfiles_UserStatus] FOREIGN KEY([UserStatusId])
REFERENCES [dbo].[UserStatus] ([Id])
END
ALTER TABLE [dbo].[UserTrustedDevices] ADD  CONSTRAINT [DF_UserTrustedDevices_CreateDatetimeUtc]  DEFAULT (getutcdate()) FOR [CreateDatetimeUtc]
GO

ALTER TABLE [dbo].[UserTrustedDevices]  WITH CHECK ADD  CONSTRAINT [FK_UserTrustedDevices_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[UserTrustedDevices] CHECK CONSTRAINT [FK_UserTrustedDevices_Users]
GO
GO
ALTER TABLE [dbo].[UsersProfiles] CHECK CONSTRAINT [FK_UsersProfiles_UserStatus]
GO

ALTER TABLE [dbo].[UserActivityLogs]  WITH CHECK ADD  CONSTRAINT [FK_UserActivityLogs_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserActivityLogs] CHECK CONSTRAINT [FK_UserActivityLogs_Users]
GO
ALTER TABLE [dbo].[InsuredVehicle]  WITH CHECK ADD  CONSTRAINT [FK_InsuredVehicle_Insureds] FOREIGN KEY([InsuredId])
REFERENCES [dbo].[Insureds] ([Id])
GO
ALTER TABLE [dbo].[InsuredVehicle] CHECK CONSTRAINT [FK_InsuredVehicle_Insureds]
GO
ALTER TABLE [dbo].[InsuredVehicle]  WITH CHECK ADD  CONSTRAINT [FK_InsuredVehicle_VehicleInfo] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[VehicleInfo] ([Id])
GO

ALTER TABLE [dbo].[InsuredVehicle] CHECK CONSTRAINT [FK_InsuredVehicle_VehicleInfo]
GO


ALTER TABLE [dbo].[LossInformation] ADD  CONSTRAINT [DF_LossInformation_EffectiveDate]  DEFAULT (getutcdate()) FOR [EffectiveDate]
GO
ALTER TABLE [dbo].[LossInformation] ADD  CONSTRAINT [DF_LossInformation_ExpireDate]  DEFAULT (getutcdate()) FOR [ExpireDate]
GO
ALTER TABLE [dbo].[LossInformation]  WITH CHECK ADD  CONSTRAINT [FK_LossInformation_Insureds] FOREIGN KEY([InsuredId])
REFERENCES [dbo].[Insureds] ([Id])
GO
ALTER TABLE [dbo].[LossInformation] CHECK CONSTRAINT [FK_LossInformation_Insureds]
GO

ALTER TABLE [dbo].[DriverInformation] ADD  CONSTRAINT [DF_DriverInformation_CreateDatetimeUtc]  DEFAULT (getutcdate()) FOR [CreateDatetimeUtc]
GO
ALTER TABLE [dbo].[DriverInformation]  WITH CHECK ADD  CONSTRAINT [FK_DriverInformation_Insureds] FOREIGN KEY([InsuredId])
REFERENCES [dbo].[Insureds] ([Id])
GO
ALTER TABLE [dbo].[DriverInformation]  WITH CHECK ADD  CONSTRAINT [FK_DriverInformation_States] FOREIGN KEY([StateId])
REFERENCES [dbo].[States] ([Id])
GO
ALTER TABLE [dbo].[DriverInformation] CHECK CONSTRAINT [FK_DriverInformation_States]
GO
ALTER TABLE [dbo].[DriverInformation]  WITH CHECK ADD  CONSTRAINT [FK_DriverInformation_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[DriverInformation] CHECK CONSTRAINT [FK_DriverInformation_Users]
GO
ALTER TABLE [dbo].[DriverInformation]  WITH CHECK ADD  CONSTRAINT [FK_DriverInformation_Users1] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[DriverInformation] CHECK CONSTRAINT [FK_DriverInformation_Users1]
GO

ALTER TABLE [dbo].[Cargo] ADD  CONSTRAINT [DF_Cargo_CreateDatetimeUtc]  DEFAULT (getutcdate()) FOR [CreateDatetimeUtc]
GO

ALTER TABLE [dbo].[Cargo] ADD  CONSTRAINT [DF_Cargo_LastModifiedUtc]  DEFAULT (getutcdate()) FOR [LastModifiedUtc]
GO

ALTER TABLE [dbo].[Cargo]  WITH CHECK ADD  CONSTRAINT [FK_Cargo_Insureds] FOREIGN KEY([InsuredId])
REFERENCES [dbo].[Insureds] ([Id])
GO

ALTER TABLE [dbo].[Cargo] CHECK CONSTRAINT [FK_Cargo_Insureds]
GO

ALTER TABLE [dbo].[Cargo]  WITH CHECK ADD  CONSTRAINT [FK_Cargo_Users] FOREIGN KEY([CreateBy])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Cargo] CHECK CONSTRAINT [FK_Cargo_Users]
GO

ALTER TABLE [dbo].[Cargo]  WITH CHECK ADD  CONSTRAINT [FK_Cargo_Users1] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Cargo] CHECK CONSTRAINT [FK_Cargo_Users1]
GO

ALTER TABLE [dbo].[Commodity] ADD  CONSTRAINT [DF_Commodity_CreateDatetimeUtc]  DEFAULT (getutcdate()) FOR [CreateDatetimeUtc]
GO
ALTER TABLE [dbo].[Commodity] ADD  CONSTRAINT [DF_Commodity_LastModifiedUtc]  DEFAULT (getutcdate()) FOR [LastModifiedUtc]
GO
ALTER TABLE [dbo].[Commodity]  WITH CHECK ADD  CONSTRAINT [FK_Commodity_Users] FOREIGN KEY([CreateBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Commodity] CHECK CONSTRAINT [FK_Commodity_Users]
GO
ALTER TABLE [dbo].[Commodity]  WITH CHECK ADD  CONSTRAINT [FK_Commodity_Users1] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Commodity] CHECK CONSTRAINT [FK_Commodity_Users1]
GO

ALTER TABLE [dbo].[CargoCommodity]  WITH CHECK ADD  CONSTRAINT [FK_CargoCommodity_Cargo] FOREIGN KEY([CargoId])
REFERENCES [dbo].[Cargo] ([Id])
GO
ALTER TABLE [dbo].[CargoCommodity] CHECK CONSTRAINT [FK_CargoCommodity_Cargo]
GO
ALTER TABLE [dbo].[CargoCommodity]  WITH CHECK ADD  CONSTRAINT [FK_CargoCommodity_Commodity] FOREIGN KEY([CommodityId])
REFERENCES [dbo].[Commodity] ([Id])
GO
ALTER TABLE [dbo].[CargoCommodity] CHECK CONSTRAINT [FK_CargoCommodity_Commodity]
GO

ALTER TABLE [dbo].[UserPhones] ADD  CONSTRAINT [DF_UserPhones_CreateDatetimeUtc]  DEFAULT (getutcdate()) FOR [CreateDatetimeUtc]
GO

ALTER TABLE [dbo].[UserPhones] ADD  CONSTRAINT [DF_UserPhones_LastModifiedUtc]  DEFAULT (getutcdate()) FOR [LastModifiedUtc]
GO

ALTER TABLE [dbo].[UserPhones]  WITH CHECK ADD  CONSTRAINT [FK_UserPhones_UsersCreated] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[UserPhones] CHECK CONSTRAINT [FK_UserPhones_UsersCreated]
GO

ALTER TABLE [dbo].[UserPhones]  WITH CHECK ADD  CONSTRAINT [FK_UserPhones_UsersOwner] FOREIGN KEY([PhoneOwnerId])
REFERENCES [dbo].[Users] ([Id])

GO

ALTER TABLE [dbo].[UserPhones]  WITH CHECK ADD  CONSTRAINT [FK_UserPhones_UsersUpdated] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[UserPhones] CHECK CONSTRAINT [FK_UserPhones_UsersUpdated]
GO

ALTER TABLE [dbo].[MessageAttachments]  WITH CHECK ADD  CONSTRAINT [FK_MessageAttachments_Attachments] FOREIGN KEY([AttachmentId])
REFERENCES [dbo].[Attachments] ([Id])
GO

ALTER TABLE [dbo].[MessageAttachments] CHECK CONSTRAINT [FK_MessageAttachments_Attachments]
GO

ALTER TABLE [dbo].[MessageAttachments]  WITH CHECK ADD  CONSTRAINT [FK_MessageAttachments_Messages] FOREIGN KEY([MessageId])
REFERENCES [dbo].[Messages] ([Id])
GO

ALTER TABLE [dbo].[MessageAttachments] CHECK CONSTRAINT [FK_MessageAttachments_Messages]
GO
ALTER TABLE [dbo].[AgentTasks]  WITH CHECK ADD  CONSTRAINT [FK_AgentTasks_AgentTaskStatuses] FOREIGN KEY([AgentTaskStatusId])
REFERENCES [dbo].[AgentTaskStatuses] ([Id])
GO
ALTER TABLE [dbo].[AgentTasks]  WITH CHECK ADD  CONSTRAINT [FK_AgentTasks_AgentTaskTypes] FOREIGN KEY([AgentTaskTypeId])
REFERENCES [dbo].[AgentTaskTypes] ([Id])
GO
ALTER TABLE [dbo].[AgentTasks]  WITH CHECK ADD  CONSTRAINT [FK_AgentTasks_Insureds] FOREIGN KEY([InsuredId])
REFERENCES [dbo].[Insureds] ([Id])
GO

ALTER TABLE [dbo].[AgentTasks] CHECK CONSTRAINT [FK_AgentTasks_AgentTaskStatuses]
GO

ALTER TABLE [dbo].[AgentTasks]  WITH CHECK ADD  CONSTRAINT [FK_AgentTasks_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[AgentTasks] CHECK CONSTRAINT [FK_AgentTasks_Users]
GO

ALTER TABLE [dbo].[AgentTasks]  WITH CHECK ADD  CONSTRAINT [FK_AgentTasks_Users1] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[AgentTasks] CHECK CONSTRAINT [FK_AgentTasks_Users1]
GO

ALTER TABLE [dbo].[AgentTasks]  WITH CHECK ADD  CONSTRAINT [FK_AgentTasks_Users2] FOREIGN KEY([AssigneeId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[AgentTasks] CHECK CONSTRAINT [FK_AgentTasks_Users2]
GO

ALTER TABLE [dbo].[UsersProfiles]  WITH CHECK ADD  CONSTRAINT [FK_UsersProfiles_UserStatus1] FOREIGN KEY([UserLastStatusId])
REFERENCES [dbo].[UserStatus] ([Id])
GO

ALTER TABLE [dbo].[UsersProfiles] CHECK CONSTRAINT [FK_UsersProfiles_UserStatus1]
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [TwoFactorEnabled]
GO


ALTER TABLE [dbo].[Users]  ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([UserRoleId])
REFERENCES [dbo].[Roles] ([Id])
GO

ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
ALTER TABLE [dbo].[Attachments]  WITH CHECK ADD  CONSTRAINT [FK_Attachments_UserGarages] FOREIGN KEY([UserGarageId])
REFERENCES [dbo].[UserGarages] ([Id])
Go
-- ================================================
-- Template generated from Template Explorer using:
-- Create Index (New Menu).SQL
--
-- ================================================
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [Unique_name] ON [dbo].[Groups]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserTrustedDevices] ON [dbo].[UserTrustedDevices]
(
	[DeviceId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserPhoneCallHistory] ADD  CONSTRAINT [DF_UserPhoneCallHistory_CreateDatetimeUtc]  DEFAULT (getutcdate()) FOR [CreateDatetimeUtc]
GO
ALTER TABLE [dbo].[UserPhoneCallHistory]  WITH CHECK ADD  CONSTRAINT [FK_UserPhoneCallHistory_CallTypes] FOREIGN KEY([InitialCallTypeId])
REFERENCES [dbo].[CallTypes] ([Id])
GO

ALTER TABLE [dbo].[UserPhoneCallHistory] CHECK CONSTRAINT [FK_UserPhoneCallHistory_CallTypes]
GO

ALTER TABLE [dbo].[UserPhoneCallHistory]  WITH CHECK ADD  CONSTRAINT [FK_UserPhoneCallHistory_UserPhoneCallHistory] FOREIGN KEY([CurrentCallTypeId])
REFERENCES [dbo].[CallTypes] ([Id])
GO

ALTER TABLE [dbo].[UserPhoneCallHistory] CHECK CONSTRAINT [FK_UserPhoneCallHistory_UserPhoneCallHistory]
GO

ALTER TABLE [dbo].[UserPhoneCallHistory]  WITH CHECK ADD  CONSTRAINT [FK_UserPhoneCallHistory_Users] FOREIGN KEY([UserPhoneId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[UserPhoneCallHistory] CHECK CONSTRAINT [FK_UserPhoneCallHistory_Users]
GO

ALTER TABLE [dbo].[UnReadMessages] ADD  CONSTRAINT [IX_UnReadMessagesUniqieUserIdMessageId] UNIQUE NONCLUSTERED 
(
	[ReadUserId] ASC,
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GroupMembers] ADD  CONSTRAINT [IX_GroupMembers] UNIQUE NONCLUSTERED 
(
	[GroupId] ASC,
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

-- ================================================
-- Template generated from Template Explorer using:
-- Create Trigger (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- See additional Create Trigger templates for more
-- examples of different Trigger statements.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER TRIGGER UpdateLastModifiedUsers
ON [dbo].[Users]
AFTER UPDATE
AS BEGIN
   UPDATE dbo.Users
   SET LastModifiedUtc = GETUTCDATE()
   FROM INSERTED i
   WHERE i.Id = Users.Id
END
-- ================================================

GO
CREATE TRIGGER UpdateLastModifiedAgencies
ON dbo.Agencies
AFTER UPDATE
AS BEGIN
   UPDATE dbo.Agencies
   SET LastModifiedUtc = GETUTCDATE()
   FROM INSERTED i
   WHERE i.Id = Agencies.Id
END
GO
ALTEr TRIGGER [dbo].[UpdateLastModifiedInsureds]
ON [dbo].[Insureds]
AFTER UPDATE
AS BEGIN
   UPDATE dbo.Insureds
   SET LastModifiedUtc = GETUTCDATE()
   FROM INSERTED i
   WHERE i.Id = Insureds.Id
END
GO
CREATE TRIGGER [dbo].[UpdateLastModifiedGroups]
ON [dbo].[Groups]
AFTER UPDATE
AS BEGIN
   UPDATE dbo.Groups
   SET LastModifiedUtc = GETUTCDATE()
   FROM INSERTED i
   WHERE i.Id = Groups.Id
END
GO
CREATE TRIGGER [dbo].[UpdateLastModifiedUserPhones]
ON [dbo].[UserPhones]
AFTER UPDATE
AS BEGIN
   UPDATE dbo.UserPhones
   SET LastModifiedUtc = GETUTCDATE()
   FROM INSERTED i
   WHERE i.Id = UserPhones.Id
END
GO
Create TRIGGER [dbo].[UpdateLastModifiedAgentTasks]
ON [dbo].[AgentTasks]
AFTER UPDATE
AS BEGIN
   UPDATE dbo.AgentTasks
   SET LastModifiedUtc = GETUTCDATE()
   FROM INSERTED i
   WHERE i.Id = AgentTasks.Id
END
