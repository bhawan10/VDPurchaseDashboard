CREATE PROCEDURE getPurchaseOrder @Location nvarchar(200)
AS
select PONumber from [Master].[PurchaseOrder] as tbl_PO with (NOLOCK)
where tbl_PO.SupplierId=
(select ID from [FalconTest].[dbo].[Vendor_Master_Job_work] with (NOLOCK)
where Vendor_Name=@Location) 
 and Status='Accepted by vendor';
GO

