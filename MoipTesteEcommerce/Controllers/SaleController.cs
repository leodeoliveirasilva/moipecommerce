using MoipTesteEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoipTesteEcommerce.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        public ActionResult ConfirmPayment()
        {
            var sale = new Sale() { Id = 1, Name = "FIFA 14 XBOX ONE", Price = 1 };
            return View(sale);
        }

        public ActionResult Payment()
        {
            return View();
        }
    }
}