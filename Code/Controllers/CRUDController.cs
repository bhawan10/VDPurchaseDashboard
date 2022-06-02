using Microsoft.AspNetCore.Mvc;
using TestForm.Models;
using System.Data;

namespace TestForm.Controllers
{
    public class CRUDController : Controller
    {
        public IActionResult Vendor()
        {
            CRUDModel model = new CRUDModel();
            string name = "Ankur.AD375";
            DataTable dt = model.GetAllVendors(name);
            
            return View("Vendor",dt);
        }
        public IActionResult PurchaseOrder()
        {
            CRUDModel model = new CRUDModel();
            string vendorSelected = "Toshi Automatic Systems Pvt. Ltd";
            DataTable dt = model.GetAllVendors(vendorSelected);
            Console.WriteLine(dt);
            return View("PurchaseOrder", dt);
        }
        public IActionResult ItemsFromOperation()
        {
            CRUDModel model = new CRUDModel();
            string OperationId = "10";
            string POId = "859";
            DataTable dt = model.GetItemsFromOperation(OperationId, POId);
            return View("PurchaseOrder", dt);
        }
        public IActionResult Category()
        {
            CRUDModel model = new CRUDModel();
            string POId = "859";
            DataTable dt = model.getDistinctCategoryIds(POId);
            return View("POId", dt);
        }
    }
}
