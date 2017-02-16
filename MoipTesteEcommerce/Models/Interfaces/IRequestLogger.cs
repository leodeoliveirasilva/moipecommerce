using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MoipTesteEcommerce.Models.Interfaces
{
    public interface IRequestLogger
    {
        void CaptureRequestData(HttpRequestBase request);
    }
}
