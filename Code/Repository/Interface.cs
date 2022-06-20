using System.Data;

namespace TestForm.Repository
{
    public interface Interface
    {
        public DataTable getVendors(string expeditorName);
        public DataTable getPOs(string vendorId);
        public DataTable getDistinctCategory(string POId);
        public DataTable getItemsfromOperation(string operationId, string POId);
        

    }
}
