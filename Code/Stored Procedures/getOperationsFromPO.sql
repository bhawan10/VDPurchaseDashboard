CREATE PROCEDURE getOperationsFromPO @POId bigint
AS
select [FalconTest].[dbo].[Operation].operationName from
(select * from [FalconTest].[dbo].[ProcessOperation] p with (NOLOCK)
inner join (select distinct [FalconTest].[dbo].[Item].RawMaterialCategoryId
from [FalconTest].[dbo].[Item] with (NOLOCK)
inner join [FalconTest].[Relate].[PurchaseOrderItemMapping] with (NOLOCK) 
on [FalconTest].[Relate].[PurchaseOrderItemMapping].ItemId = [FalconTest].[dbo].[Item].ITEM_ID 
where [FalconTest].[Relate].[PurchaseOrderItemMapping].POId =@POId) s
on s.RawMaterialCategoryId = p.ProcessId) tbl_OperationIds
inner join [dbo].[Operation] with (NOLOCK) 
on [dbo].[Operation].id=tbl_OperationIds.OperationId;
GO
