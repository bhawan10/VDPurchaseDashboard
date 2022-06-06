using System.Data;
using System.Data.SqlClient;

namespace TestForm.Repository
{
    public class Repository : Interface
    {
        private SqlConnection conn;
        private void connection()
        {
            string connect = @"Data Source=.;Initial Catalog=FalconTest;Integrated Security=True";
            conn = new SqlConnection(connect);
        }
        public DataTable getVendors(string expeditorName)
        {
            connection();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("getVendorName", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Expeditor", expeditorName);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            conn.Open();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        public DataTable getPOs(string vendorName)
        {
            connection();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("getPurchaseOrder", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Location", vendorName);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            conn.Open();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        public DataTable getItemsfromOperation(string operationId, string POId)
        {
            connection();
            SqlCommand cmd = new SqlCommand("getItemsFromOperation", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OperationId", operationId);
            cmd.Parameters.AddWithValue("@POId", POId);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            conn.Open();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        public DataTable getDistinctCategory(string POId)
        {
            connection();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("getOperationsFromPO", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@POId", POId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            conn.Open();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
    }
}
