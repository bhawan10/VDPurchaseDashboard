CREATE PROCEDURE getItemsFromOperation @OperationId bigint, @POId bigint
AS
select DISTINCT(tbl_POItemMapping.PkId),tbl_POItemMapping.ToolNo,tbl_POItemMapping.Station,tbl_POItemMapping.PositionNo,tbl_POItemMapping.ReworkNo,MODEL,doneQuantity,tbl_POItemMapping.Quantity as totalQuantity
from (select * from [dbo].[ProcessOperation] WITH (NOLOCK)
Where [dbo].[ProcessOperation].OperationId = @OperationId ) tbl_ProcessOperation
INNER JOIN [dbo].[Item] WITH (NOLOCK)
ON tbl_ProcessOperation.ProcessId=[dbo].[Item].RawMaterialCategoryId
INNER JOIN
(select * from [Relate].[PurchaseOrderItemMapping] WITH (NOLOCK)
where [Relate].[PurchaseOrderItemMapping].POId= @POId) tbl_POItemMapping
ON [dbo].[Item].ITEM_ID=tbl_POItemMapping.ItemId 
LEFT JOIN (select * from dbo.ExpeditorForm WITH (NOLOCK) WHERE isActive=1 AND isCompleted=0) tbl_ExpeditorForm 
ON tbl_POItemMapping.PkId=tbl_ExpeditorForm.POItemID
ORDER BY MODEL;
GO
