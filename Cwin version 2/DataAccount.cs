using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwin_version_2
{
    public class DataAccount
    {
        public string account { get; set; }
        public string password { get; set; }
        public string confirm_Password { get; set; }
        public object moneyPassword { get; set; }
        public string name { get; set; }
        public string countryCode { get; set; }
        public object mobile { get; set; }
        public object email { get; set; }
        public object sex { get; set; }
        public object birthday { get; set; }
        public object idNumber { get; set; }
        public object qqAccount { get; set; }
        public object groupBank { get; set; }
        public object bankName { get; set; }
        public object bankProvince { get; set; }
        public object bankCity { get; set; }
        public object bankAccount { get; set; }
        public string checkCodeEncrypt { get; set; }
        public string checkCode { get; set; }
        public bool isRequiredMoneyPassword { get; set; }
        public object dealerAccount { get; set; }
        public string parentAccount { get; set; }
    }
}
