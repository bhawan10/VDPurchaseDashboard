CREATE PROCEDURE getPurchaseOrder @Id bigint
AS
Select PkId, PONumber from [Master].[PurchaseOrder] as tbl_PO with (NOLOCK)
where tbl_PO.SupplierId=@Id  and Status='Accepted by vendor';
GO
