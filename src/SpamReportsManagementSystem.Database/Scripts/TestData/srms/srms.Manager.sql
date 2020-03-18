﻿MERGE [srms].[Manager] AS t
USING (VALUES 
    ('{AD7CFE79-1DDD-4B8F-B0A0-DE9B177F53CA}', N'Manager1'),
    ('{16DFFBC2-21C2-4F1A-AF5C-9D22BDFB401F}', N'Manager2'),
    ('{D4367840-9097-4438-8097-2C08BBE2CED7}', N'Manager3')
) s ([Uid], [Name])
ON (t.[Uid] = s.[Uid])
WHEN NOT MATCHED THEN
    INSERT ([Uid], [Name]) VALUES (s.[Uid], s.[Name])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;
GO