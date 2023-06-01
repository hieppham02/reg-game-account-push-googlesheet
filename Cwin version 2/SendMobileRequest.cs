using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwin_version_2
{
    class SendMobileRequest
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public List<object> data { get; set; }
    }
}

