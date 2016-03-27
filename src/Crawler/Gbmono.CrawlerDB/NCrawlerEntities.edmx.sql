
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/16/2015 15:12:20
-- Generated from EDMX file: C:\TFSN\WP-Source\trunk\src\WP.Crawler\GbmonoCrawlerDB\NCrawlerEntities.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [NCrawler];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CrawlHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CrawlHistory];
GO
IF OBJECT_ID(N'[dbo].[CrawlQueue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CrawlQueue];
GO
IF OBJECT_ID(N'[dbo].[CrawlDateTimes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CrawlDateTimes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CrawlHistory'
CREATE TABLE [dbo].[CrawlHistory] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Key] nvarchar(1024)  NOT NULL,
    [GroupId] int  NOT NULL
);
GO

-- Creating table 'CrawlQueue'
CREATE TABLE [dbo].[CrawlQueue] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GroupId] int  NOT NULL,
    [SerializedData] varbinary(max)  NULL,
    [Key] nvarchar(max)  NOT NULL,
    [Exclusion] bit  NOT NULL
);
GO

-- Creating table 'CrawlDateTimes'
CREATE TABLE [dbo].[CrawlDateTimes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CrawlHistoryId] int  NOT NULL,
    [CreateDateTime] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CrawlHistory'
ALTER TABLE [dbo].[CrawlHistory]
ADD CONSTRAINT [PK_CrawlHistory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CrawlQueue'
ALTER TABLE [dbo].[CrawlQueue]
ADD CONSTRAINT [PK_CrawlQueue]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CrawlDateTimes'
ALTER TABLE [dbo].[CrawlDateTimes]
ADD CONSTRAINT [PK_CrawlDateTimes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------