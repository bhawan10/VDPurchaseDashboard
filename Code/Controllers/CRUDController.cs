using Microsoft.AspNetCore.Mvc;
using System.Data;
using TestForm.Repository;
using TestForm.Repository.DTOs;

namespace TestForm.Controllers
{
    public class CRUDController : Controller
    {
        private readonly Interface repo;
        private readonly List<UT_ExpeditorForm> dataDump;
        public CRUDController(Interface _repository)
        {
            this.repo = _repository;
            this.dataDump = new List<UT_ExpeditorForm>();
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
            }
            return VendorList;
        }

        public List<DropDownList> PurchaseOrder(string vendor)
        {
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
            DataTable dt = repo.getItemsfromOperation(operationId, poId);
            List<FormDataList> Items = new List<FormDataList>();
            foreach(DataRow row in dt.Rows)
            {
                FormDataList temp = new FormDataList();
                temp.toolNo = row["ToolNo"].ToString();
                temp.station = row["Station"].ToString();
                temp.positionNo = row["PositionNo"].ToString();
                temp.reworkNo = row["ReworkNo"].ToString();
                temp.modelNo = row["MODEL"].ToString();
                if (string.IsNullOrEmpty(row["doneQuantity"].ToString()))
                {
                    temp.doneQuantity = 0;
                }
                else
                {
                    temp.doneQuantity = Int64.Parse(row["doneQuantity"].ToString());
                }
                temp.givenQuantity = Int64.Parse(row["totalQuantity"].ToString());
                temp.id = Int64.Parse(row["PkId"].ToString());
                if(temp.doneQuantity != temp.givenQuantity)
                {
                    Items.Add(temp);
                }
            }
            return Items;
        }

        [HttpPost]
        public void PutExpeditorData([FromForm] IEnumerable<UT_ExpeditorForm> forms)
        {
            var operationID = forms.ElementAt(0).OperationId.ToString();
            var poId = forms.ElementAt(0).POId.ToString();
            List <FormDataList> check = GetItemsFromOperationAndPO(operationID, poId);  
            IDictionary<long,long> data = new Dictionary<long, long>();
            foreach(var item in check)
            {
                data.Add(item.id, item.doneQuantity);
            }
            foreach (var form in forms)
            {
                long id = form.POItemID;
                if (data[id] < form.doneQuantity)
                {
                    repo.insertIntoExpeditorForm(form);
                    Console.WriteLine("hello world");
                }
            }
        }
    }
}
