using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwin_version_2
{
    public class ResponseAccount
    {
        public List<object> Error { get; set; }
        public int Code { get; set; }
        public Result Result { get; set; }
        public DateTime ReplyTime { get; set; }
    }
    public class Result
    {
        public bool MemberRegisterVerifySwitch { get; set; }
        public int AvailableMinutes { get; set; }
        public Token Token { get; set; }
    }
    public class Token
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
