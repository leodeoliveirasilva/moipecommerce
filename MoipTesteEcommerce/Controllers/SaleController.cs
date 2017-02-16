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
using System.Threading.Tasks;

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
                IEnumerable<string> teste = SerializationUtility.GetSavedBeanstalkWebHooks();

                if (Request.HttpMethod == "POST")
                {
                    _RequestLogger.CaptureRequestData(Request);
                    ViewData["Message"] = "Request Logged";
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public ActionResult Payment(string nome, int preco, string link, string orderid)
        {
            var sale = new Sale()
            {
                Id = 1,
                Name = nome,
                Price = 1,
                Url = link,
                OrderId = orderid
            };
            return View(sale);
        }

        public ActionResult ConfirmPayment(string nome, int preco, string link, string orderid, string paymentid)
        {
            string status = "AGUARDANDO APROVAÇÃO";
            if (SerializationUtility.OrderPaid(orderid))
            {
                status = "APROVADO";
            }
            var sale = new Sale()
            {
                Id = 1,
                Name = nome,
                Price = 1,
                Url = link,
                OrderId = orderid,
                PaymentId = paymentid,
                Status = status
            };
            return View(sale);
        }

        public void PaymentResult(string nome, int preco, string link, string orderid, string paymentid)
        {
            if (SerializationUtility.OrderPaid(orderid))
                //return ConfirmPayment(nome, preco, link, orderid, paymentid, "APROVADO");
                RedirectToAction("~/Sale/ConfirmPayment");
            else
                RedirectToAction("~/Sale/ConfirmPayment");
                //return View(ConfirmPayment(nome, preco, link, orderid, paymentid, "AGUARDANDO APROVAÇÃO"));
        }
    }
}