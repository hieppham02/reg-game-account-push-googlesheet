using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwin_version_2
{
    public class BetRecordInfo
    {
        public string wagersTimeBegin { get; set; }
        public string wagersTimeEnd { get; set; }
        public string payoffTimeBegin { get; set; }
        public string payoffTimeEnd { get; set; }
        public int searchTimeOption { get; set; }
        public bool unpayOnly { get; set; }
        public object gameCategories { get; set; }
    }
    public class BetRecordRes
    {
        public int State { get; set; }
        public int Count { get; set; }
        public double TotalBetAmount { get; set; }
        public double TotalCommissionable { get; set; }
        public double TotalPayoff { get; set; }
    }
}
