using System.Data;
using TestForm.Repository.DTOs;


namespace TestForm.Repository
{
    public interface Interface
    {
        public DataTable getVendors(string expeditorName);
        public DataTable getPOs(string vendorId);
        public DataTable getDistinctCategory(string POId);
        public DataTable getItemsfromOperation(string operationId, string POId);
        public void insertIntoExpeditorForm(UT_ExpeditorForm uT_ExpeditorForm);
        public void doInactivePrevEntries(Int64 POItemID);

    }
}
