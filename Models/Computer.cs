using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingYourComputer.Models
{
    public class Computer
    {
        public string Name { set; get; }
        public Processor Processor { set; get; }
        public Videocard Videocard { set; get; }
        public PhysicalMemory Memory { set; get; }
        public HardDrive Drive { set; get; }
        public OperationSystem System { set; get; }
        public int RankPC { set; get; }
    }
}
