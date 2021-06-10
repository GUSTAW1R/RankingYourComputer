using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingYourComputer.Models
{
    public class Videocard
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Vender { set; get; }
        public string Generation { set; get; }
        public int Rank { set; get; }
        public string IsIntegrated { set; get; }

        public Videocard(int id, string name, string vender, string generation, int rank, string isInt)
        {
            Id = id;
            Name = name;
            Vender = vender;
            Generation = generation;
            Rank = rank;
            IsIntegrated = isInt;
        }
    }
}
