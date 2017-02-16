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
        public ActionResult ConfirmOrder(int preco, string nome, string link)
        {
            var sale = new Sale() { Id = 1, Name = nome, Price = preco, Url = link };
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

        public ActionResult Payment(string nome, int preco, string link, string orderid)
        {
            var sale = new Sale() {
                Id = 1,
                Name = nome,
                Price = 1, Url = link,
                OrderId = orderid    
            };
            return View(sale);
        }

        public ActionResult ConfirmPayment(string nome, int preco, string link, string orderid, string paymentid)
        {
            var sale = new Sale()
            {
                Id = 1,
                Name = nome,
                Price = 1,
                Url = link,
                OrderId = orderid,
                PaymentId = paymentid
            };
            return View(sale);
        }

    }
}