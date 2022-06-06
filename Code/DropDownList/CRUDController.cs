using Microsoft.AspNetCore.Mvc;
using System.Data;
using Expeditor_Form.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Expeditor_Form.Controllers
{
    public class CRUDController : Controller
    {
        public IActionResult Vendor()
        {
            CRUD model = new CRUD();
            string name = "Ankur.AD375";    
            DataTable dt = model.GetAllVendors(name);

            List<string> vendorList = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                vendorList.Add(dr["Location"].ToString());
            }

            ViewBag.VendorList=new SelectList(vendorList);

            return View();
        }
        
    }
}
