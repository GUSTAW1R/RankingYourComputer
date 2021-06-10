using RankingYourComputer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingYourComputer.Controllers
{
    interface IStrategyComputer
    {
        Computer GetPC(string name);
        List<String> GetListComputers();
        void SetInListPC(string name, string processor, string videocard, string memory, string drive, string systemName, string systemVersion);
    }
}
