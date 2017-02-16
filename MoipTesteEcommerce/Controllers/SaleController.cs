using MoipTesteEcommerce.Models;
using MoipTesteEcommerce.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Collections.Specialized;
using MoipTesteEcommerce.WebHook;

namespace MoipTesteEcommerce.Controllers
{
    [HandleError]
    public class SaleController : Controller
    {
        private IRequestLogger _RequestLogger = new SerializationUtility();

        // GET: Sale
        public ActionResult ConfirmOrder()
        {
            var sale = new Sale() { Id = 1, Name = "FIFA 14 XBOX ONE", Price = 1 };
            return View(sale);
        }

        public bool WebHookMoip()
        {
            try
            {
                if (Request.HttpMethod == "POST")
                {
                    _RequestLogger.CaptureRequestData(Request);
                    ViewData["Message"] = "Request Logged";
                }
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        public ActionResult Payment()
        {
            return View();
        }

        public ActionResult ConfirmPayment()
        {
            return View();
        }

    }
}