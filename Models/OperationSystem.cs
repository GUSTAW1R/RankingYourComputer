using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingYourComputer.Models
{
    public class OperationSystem
    {
        public string Name { set; get; }
        public string xVersion { set; get; }

        public OperationSystem(string name, string version)
        {
            Name = name;
            xVersion = version;
        }
    }
}
