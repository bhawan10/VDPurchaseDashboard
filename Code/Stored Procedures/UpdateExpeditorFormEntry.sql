CREATE PROCEDURE UpdateExpeditorFormEntry(@POItemID bigint)  
AS  
BEGIN  
UPDATE dbo.ExpeditorForm  
SET
	dbo.ExpeditorForm.isActive = 0
	WHERE dbo.ExpeditorForm.POItemID=@POItemID
END  
