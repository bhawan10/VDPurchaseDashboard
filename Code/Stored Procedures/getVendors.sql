CREATE PROCEDURE getVendors @Expeditor nvarchar(200)
AS
select ID,Location from 
(select Location from [dbo].[VendorExpeditor] with (NOLOCK)
where Expeditor=@Expeditor) tbl_VE
Inner Join (select Vendor_Name, ID from [dbo].[Vendor_Master_Job_work] ) tbl_VendorMaster
on tbl_VE.Location=tbl_VendorMaster.Vendor_Name;
GO

