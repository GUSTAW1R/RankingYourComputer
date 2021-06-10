using RankingYourComputer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingYourComputer.Controllers
{
    class DataSingleton
    {
        private static DataSingleton instance;
        private static object syncRoot = new Object();
        public List<Computer> Computers { set; get; }
        public List<Game> Games { set; get; }
        public string ComputerName { set; get; }
        public int GamePosition { set; get; }

        private protected DataSingleton()
        {
            Computers = new List<Computer>();
            Games = new List<Game>();
            ComputerName = String.Empty;
            GamePosition = 0;
        }

        public static DataSingleton getInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new DataSingleton();
                }
            }
            return instance;
        }
    }
}
