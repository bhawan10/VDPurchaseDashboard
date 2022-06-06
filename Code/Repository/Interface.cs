using System.Data;

namespace TestForm.Repository
{
    public interface Interface
    {
        public DataTable getVendors(string expeditorName);
        public DataTable getPOs(string vendorName);
        public DataTable getItemsfromOperation(string operationId, string POId);
        public DataTable getDistinctCategory(string POId);

    }
}
