using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingYourComputer.Models
{
    public class Requierement
    {
        public string Type { set; get; }
        public string ProcessorName { set; get; }
        public int ProcessorRank { set; get; }
        public string VideocardName { set; get; }
        public int VideocardRank { set; get; }
        public int MemoryValue { set; get; }
        public string HardValue { set; get; }
        public string SystemName { set; get; }
        public string xVersion { set; get; }
    }
}
