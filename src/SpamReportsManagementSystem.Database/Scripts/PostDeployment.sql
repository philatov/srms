﻿
PRINT N'Post-Deployment...';
GO

:r .\InitData\Include.sql

IF '$(ENVIRONMENT)' <> 'DEVELOPMENT'
    SET NOEXEC ON
GO

/* INSERT DEVELOPMENT SCRIPTS HERE */
:r .\TestData\Include.sql

SET NOEXEC OFF
GO

IF '$(ENVIRONMENT)' <> 'STAGING'
    SET NOEXEC ON
GO

/* INSERT STAGING SCRIPTS HERE */

SET NOEXEC OFF


IF '$(ENVIRONMENT)' <> 'PRODUCTION'
    SET NOEXEC ON
GO

/* INSERT PRODUCTION SCRIPTS HERE */

SET NOEXEC OFF
GO