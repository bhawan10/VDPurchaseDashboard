using Microsoft.Azure.Documents;
using System.Data;
using System.Data.SqlClient;
using TestForm.Repository.DTOs;


namespace TestForm.Repository
{
    public class Repository : Interface
    {
        private SqlConnection conn;
        private Int64 pkId;
        private bool callOnce = false;
        private void connection()
        {
            string connect = @"Data Source=.;Initial Catalog=FalconTest;Integrated Security=True";
            conn = new SqlConnection(connect);
        }
        public DataTable getVendors(string expeditorName)
        {
            connection();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("getVendors", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Expeditor", expeditorName);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            conn.Open();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        public DataTable getPOs(string vendorId)
        {
            connection();
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(vendorId))
            {
                SqlCommand cmd = new SqlCommand("getPurchaseOrder", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id",Int64.Parse(vendorId));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Open();
                
                da.Fill(dt);
                conn.Close();
            }
            return dt;
        }

        public DataTable getDistinctCategory(string POId)
        {
            connection();
            DataTable dt = new DataTable();
            if(!string.IsNullOrEmpty(POId))
            {
                SqlCommand cmd = new SqlCommand("getOperationsFromPO", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@POId", Int64.Parse(POId));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Open();
                da.Fill(dt);
                conn.Close();
            }
            return dt;
        }

        public DataTable getItemsfromOperation(string operationId, string POId)
        {
            connection();
            DataTable dt = new DataTable();
            if(!string.IsNullOrEmpty(operationId) && !string.IsNullOrEmpty(POId))
            {
                SqlCommand cmd = new SqlCommand("getItemsFromOperation", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OperationId", operationId);
                cmd.Parameters.AddWithValue("@POId", POId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Open();
                da.Fill(dt);
                conn.Close();
            }
            return dt;
        }


        public void getExpeditorFormTableSize()
        {
            connection();
            SqlCommand cmd = new SqlCommand("getCurrentTableSize", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            conn.Open();
            da.Fill(dt);
            conn.Close();
       
            pkId = Int64.Parse(dt.Rows[0][0].ToString());
            
        }

        public void doInactivePrevEntries(Int64 POItemID)
        {
            connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("UpdateExpeditorFormEntry", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@POItemID", POItemID);
            cmd.ExecuteNonQuery();
            conn.Close();
        }




        public void insertIntoExpeditorForm(UT_ExpeditorForm formEntry)
        {

            doInactivePrevEntries(formEntry.POItemID);
            if (formEntry.doneQuantity == formEntry.totalQuantity)
                formEntry.isCompleted = true;

            formEntry.isActive = true;


            DataTable DT = new DataTable();
            DT.Columns.Add("POItemID", typeof(Int64));
            DT.Columns.Add("OperationId", typeof(int));
            DT.Columns.Add("entryBy", typeof(string));
            DT.Columns.Add("entryDate", typeof(DateTime));
            DT.Columns.Add("totalQuantity", typeof(int));
            DT.Columns.Add("doneQuantity", typeof(int));
            DT.Columns.Add("isActive", typeof(bool));
            DT.Columns.Add("isCompleted", typeof(bool));

            DataRow DR = DT.NewRow();

            DR["POItemID"] = formEntry.POItemID;
            DR["OperationId"] = formEntry.OperationId;
            DR["entryBy"] = formEntry.entryBy;
            DR["entryDate"] = formEntry.entryDate;
            DR["totalQuantity"] = formEntry.totalQuantity;
            DR["doneQuantity"] = formEntry.doneQuantity;
            DR["isActive"] = formEntry.isActive;
            DR["isCompleted"] = formEntry.isCompleted;
            
            

            DT.Rows.Add(DR);


            //Console.WriteLine("---------------------");
            //Console.WriteLine(formEntry.POItemID);
            //Console.WriteLine(formEntry.OperationId);
            //Console.WriteLine(formEntry.entryBy);
            //Console.WriteLine(formEntry.entryDate);
            //Console.WriteLine(formEntry.totalQuantity);
            //Console.WriteLine(formEntry.doneQuantity);
            //Console.WriteLine(formEntry.isActive);
            //Console.WriteLine(formEntry.isCompleted);



            connection();
            

            //DataTable dt = new DataTable();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            conn.Open();
            SqlCommand cmd = new SqlCommand("InsertExpeditorFormEntry", conn);
            cmd.Parameters.AddWithValue("@formEntry", DT);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            conn.Close();



        }

    }
}
