using RankingYourComputer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingYourComputer.Controllers
{
    class ComputerController
    {
        public ComputerController(IStrategyComputer context)
        {
            Strategy = context;
        }
        public IStrategyComputer Strategy { private get; set; }

        public List<string> GetListComputers()
        {
            return Strategy.GetListComputers();
        }
        public Computer GetComputer(string Name)
        {
            return Strategy.GetPC(Name);
        }
        public void SetComputer(string pcName, string cpuName, string gpuName, string ramName, string driveName, string osName, string osVersion)
        {
            Strategy.SetInListPC(pcName, cpuName, gpuName, ramName, driveName, osName, osVersion);
        }
    }
}
