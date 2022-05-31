CREATE PROCEDURE getVendors @Expeditor nvarchar(50)
AS
select Location from [dbo].[VendorExpeditorMapping]
where Expeditor=@Expeditor;
GO


CREATE PROCEDURE getPurchaseOrder @Location nvarchar(200)
AS
select * from [FalconTest].[Master].[PurchaseOrder]
where Location=@Location and Status='Accepted by vendor';
GO

CREATE PROCEDURE getDistinctCategoryIds @POId nvarchar(30)
AS
select distinct [FalconTest].[dbo].[Item].RawMaterialCategoryId
from [FalconTest].[dbo].[Item]
inner join [FalconTest].[Relate].[PurchaseOrderItemMapping] on [FalconTest].[Relate].[PurchaseOrderItemMapping].ItemId = [FalconTest].[dbo].[Item].ITEM_ID
where [FalconTest].[Relate].[PurchaseOrderItemMapping].POId = @POId
GO


CREATE PROCEDURE getItemsFromOperation @Operation nvarchar(30) @POId nvarchar(30)
AS
select [dbo].[Item].ITEM_ID,[dbo].[Item].ITEM_DESCRIPTION
FROM ProcessOperation 
INNER JOIN [dbo].[Item] 
ON ProcessOperation.CategoryID=[dbo].[Item].RawMaterialCategoryId
INNER JOIN 
[Relate].[PurchaseOrderItemMapping]
ON [dbo].[Item].ITEM_ID=[Relate].[PurchaseOrderItemMapping].ItemId
Where ProcesOperation.Operation=@Operation and [Relate].[PurchaseOrderItemMapping].PoId = @POId
GO
