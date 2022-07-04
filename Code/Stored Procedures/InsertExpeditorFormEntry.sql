CREATE PROCEDURE InsertExpeditorFormEntry(@formEntry [dbo].[UT_ExpeditorForm] ReadOnly)  
AS  
BEGIN  
INSERT INTO dbo.ExpeditorForm  
SELECT * FROM @formEntry  
END  
