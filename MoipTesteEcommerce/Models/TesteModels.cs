using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MoipTesteEcommerce.Models
{
    public class TesteModels
    {
        [DataMember(Name = "status")]
        public string status { get; set; }

        public string ToHtml()
        {
            return "<h2>" + status + "</h2>";
        }
    }
}
