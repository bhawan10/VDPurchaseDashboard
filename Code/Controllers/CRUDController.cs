using Microsoft.AspNetCore.Mvc;
using System.Data;
using TestForm.Repository;
using TestForm.Repository.DTOs;

namespace TestForm.Controllers
{
    public class CRUDController : Controller
    {
        private readonly Interface repo;

        public CRUDController(Interface _repository)
        {
            this.repo = _repository;
        }

        public IActionResult Vendor()
        {
            return View("Vendor");
        }
        public List<DropDownList> GetAllVendors()
        {
            string expeditorName = "Ankur.AD375";
            DataTable dt = repo.getVendors(expeditorName); 
            List<DropDownList> VendorList = new List<DropDownList>();
            foreach(DataRow row in dt.Rows)
            {
                DropDownList temp = new DropDownList();
                temp.Id = int.Parse(row["Id"].ToString());
                temp.text = row["Location"].ToString();
                VendorList.Add(temp);
                //result[0].VendorId.Add(row["VendorId"]);
                //result.Add(row["Location"]);
            }
            return VendorList;
        }

        public List<DropDownList> PurchaseOrder(string vendor)
        {
            //Console.WriteLine(vendor);
            DataTable dt = repo.getPOs(vendor);
            List<DropDownList> POs = new List<DropDownList>();
            foreach (DataRow row in dt.Rows)
            {
                DropDownList temp = new DropDownList();
                temp.Id = int.Parse(row["PkId"].ToString());
                temp.text = row["PONumber"].ToString();
                POs.Add(temp);

            }
            return POs;
        }

        public List<DropDownList> GetCategory(string POId)
        {
            DataTable dt = repo.getDistinctCategory(POId);
            List<DropDownList> Category = new List<DropDownList>();
            foreach (DataRow row in dt.Rows)
            {
                DropDownList temp = new DropDownList();
                temp.Id = int.Parse(row["id"].ToString());
                temp.text = row["operationName"].ToString();
                Category.Add(temp);

            }
            return Category;
        }

        public List<FormDataList> GetItemsFromOperationAndPO(string operationId, string poId)
        {
            Console.WriteLine(operationId + " " + poId);
            DataTable dt = repo.getItemsfromOperation(operationId, poId);
            List<FormDataList> Items = new List<FormDataList>();
            foreach(DataRow row in dt.Rows)
            {
                FormDataList temp = new FormDataList();
                temp.ItemId = int.Parse(row["ITEM_ID"].ToString());
                temp.ItemDescription = row["ITEM_DESCRIPTION"].ToString();
                temp.ToolNo = row["ToolNo"].ToString();
                temp.Station = row["Station"].ToString();
                temp.PositionNo = row["PositionNo"].ToString();
                temp.ReworkNo = row["ReworkNo"].ToString();
                Items.Add(temp);
            }
            return Items;
        }

    }
}
