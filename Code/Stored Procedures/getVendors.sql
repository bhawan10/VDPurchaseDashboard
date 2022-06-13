CREATE PROCEDURE getVendors @Expeditor nvarchar(200)
AS
select Location from [dbo].[VendorExpeditor] with (NOLOCK)
where Expeditor=@Expeditor;
GO
