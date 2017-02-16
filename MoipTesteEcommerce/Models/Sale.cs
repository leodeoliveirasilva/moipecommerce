using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoipTesteEcommerce.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Url { get; set; }
        public string OrderId = "";
        public string PaymentId = "";
        public string Status = "";
    }
}