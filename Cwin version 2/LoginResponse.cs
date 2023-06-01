using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwin_version_2
{
    
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public object ErrorMessage { get; set; }
        public int LoginValidationType { get; set; }
        public int SmsLoginType { get; set; }
        public object MobilePartialMask { get; set; }
        public object ConfirmToken { get; set; }
        public int CheckType { get; set; }
        public string Token { get; set; }
        public int GoogleAuthCheckType { get; set; }
        public int EmailLoginType { get; set; }
        public object EmailPartialMask { get; set; }
        public object LoginValidationTypes { get; set; }
        public bool NeedAskBinding { get; set; }
        public object LoginToken { get; set; }
    }
}
