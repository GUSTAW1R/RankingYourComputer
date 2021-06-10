using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingYourComputer.Models
{
    public class Game
    {
        public int Rank { set; get; }
        public string Name { set; get; }
        public string Genre { set; get; }
        public Requierement minRequirements { set; get; }
        public Requierement recRequirements { set; get; }
    }
}
