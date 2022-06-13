using Microsoft.AspNetCore.Mvc;
using TestForm.Models;
using System.Data;
using TestForm.Repository;


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
        public List<string> GetAllVendors()
        {
            string expeditorName = "Ankur.AD375";
            DataTable check = repo.getVendors(expeditorName); 
            List<string> result = new List<string>();
            foreach(DataRow row in check.Rows)
            {
                result.Add(row["Location"].ToString());
            }
            return result;
        }
        /*public IActionResult PurchaseOrder(string vendorName)
        {
            return View("PurchaseOrder", repo.getPOs(vendorName));
        }*/

        public List<string> PurchaseOrder(string vendor)
        {
            //string vendorName2 = "Micromatic Manufacturing systems Pvt Ltd";
            //Console.WriteLine("hello");
            //Console.WriteLine(vendor);
            DataTable check = repo.getPOs(vendor);
            List<string> POs = new List<string>();
            foreach (DataRow row in check.Rows)
            {
                POs.Add(row["PONumber"].ToString());
            }
            return POs;
        }

        public IActionResult ItemsFromOperation(string OperationId, string POId)
        {
            return View("Operation", repo.getItemsfromOperation(OperationId, POId));
        }
        public IActionResult Category(IFormCollection frm)
        {
            string POId = "859";
            return View("RawMaterial", repo.getDistinctCategory(POId));
        }
    }
}
