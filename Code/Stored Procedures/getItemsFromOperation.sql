CREATE PROCEDURE getItemsFromOperation @OperationId bigint, @POId bigint
AS
select [dbo].[Item].ITEM_ID,[dbo].[Item].ITEM_DESCRIPTION,tbl_POItemMapping.ToolNo,tbl_POItemMapping.Station,tbl_POItemMapping.PositionNo,tbl_POItemMapping.ReworkNo 
from (select * from [dbo].[ProcessOperation] WITH (NOLOCK)
Where [dbo].[ProcessOperation].OperationId = @OperationId ) tbl_ProcessOperation
INNER JOIN [dbo].[Item] WITH (NOLOCK)
ON tbl_ProcessOperation.ProcessId=[dbo].[Item].RawMaterialCategoryId
INNER JOIN 
(select * from [Relate].[PurchaseOrderItemMapping] WITH (NOLOCK)
where [Relate].[PurchaseOrderItemMapping].POId= @POId) tbl_POItemMapping 
ON [dbo].[Item].ITEM_ID=tbl_POItemMapping.ItemId ;
GO
