USE [Restaurant]
GO
/****** Object:  StoredProcedure [dbo].[GetTables]    Script Date: 08.11.2020 22:45:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTables]
AS
BEGIN
	SELECT * 
	FROM dbo.Tables;
END
GO
