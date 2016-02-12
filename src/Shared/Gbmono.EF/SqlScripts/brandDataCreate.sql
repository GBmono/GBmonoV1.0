USE [gbmono]
GO

update [Brand] set [LogoUrl]='/content/images/demo/brand0.jpg' where brandId=1
INSERT INTO [dbo].[Brand]([Name],[ManufacturerId],[LogoUrl])VALUES('资生堂',1,'/content/images/demo/brand1.jpg')
INSERT INTO [dbo].[Brand]([Name],[ManufacturerId],[LogoUrl])VALUES('花王',1,'/content/images/demo/brand2.jpg')
INSERT INTO [dbo].[Brand]([Name],[ManufacturerId],[LogoUrl])VALUES('兰蔻',1,'/content/images/demo/brand3.jpg')
GO


