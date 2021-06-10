using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingYourComputer.Models
{
    public class HardDrive
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string TypeConnection { set; get; }
        public string Vender { set; get; }
        public string Value { set; get; }
        public int Rank { set; get; }

        public HardDrive(int id, string name, string type, string vender, string value, int rank)
        {
            Id = id;
            TypeConnection = type;
            Vender = vender;
            Name = name;
            Value = value;
            Rank = rank;
        }
    }
}
