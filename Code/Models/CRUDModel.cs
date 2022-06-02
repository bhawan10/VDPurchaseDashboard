using System.Data;
using System.Data.SqlClient;

namespace TestForm.Models
{
    public class CRUDModel
    {
        public DataTable GetAllVendors(String ExpeditorName)
        {
            string connection = @"Data Source=.;Initial Catalog=FalconTest;Integrated Security=True";
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand("getVendorName", con);
            cmd.CommandType = CommandType.StoredProcedure;
            Console.WriteLine(ExpeditorName);
            cmd.Parameters.AddWithValue("@Expeditor", ExpeditorName);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(dt);
            con.Close();
            Console.WriteLine(dt.Rows[0]["Location"].ToString());
            return dt;
        }
        public DataTable GetAllPOs(string VendorName)
        {
            string connection = @"Data Source=.;Initial Catalog=FalconTest;Integrated Security=True";
            SqlConnection con = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand("getPurchaseOrder", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Location", VendorName);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public DataTable GetItemsFromOperation(string OperationId, string POId)
        {
            string connection = @"Data Source=.;Initial Catalog=FalconTest;Integrated Security=True";
            SqlConnection con = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand("getItemsFromOperation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OperationId", OperationId);
            cmd.Parameters.AddWithValue("@POId", POId);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(dt);
            con.Close();
            Console.WriteLine(dt.Rows[0]["ITEM_DESCRIPTION"].ToString());
            return dt;
        }
        public DataTable getDistinctCategoryIds(string POId)
        {
            {
                string connection = @"Data Source =.; Initial Catalog = FalconTest; Integrated Security = True";
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand("getDistinctCategoryIds", con);
                cmd.CommandType = CommandType.StoredProcedure;
                Console.WriteLine(POId);
                cmd.Parameters.AddWithValue("@POId", POId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(dt);
                con.Close();
                Console.WriteLine(dt.Rows[0]["RawMaterialCategoryId"].ToString());
                return dt;
            }

        }
    }
}
