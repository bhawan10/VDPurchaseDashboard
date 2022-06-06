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

        public IActionResult Vendor(string expeditorName)
        {
            
            return View("Vendor",repo.getVendors(expeditorName));
        }
        public IActionResult PurchaseOrder(string vendorName)
        {
            return View("PurchaseOrder", repo.getPOs(vendorName));
        }
        public IActionResult ItemsFromOperation(string OperationId, string POId)
        {
            return View("Operation", repo.getItemsfromOperation(OperationId, POId));
        }
        public IActionResult Category()
        {
            string POId = "859";
            return View("RawMaterial", repo.getDistinctCategory(POId));
        }
    }
}
