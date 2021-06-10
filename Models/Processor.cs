using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingYourComputer.Models
{
    public class Processor
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Vender { set; get; }
        public string Generation { set; get; }
        public string MemoryType { set; get; }
        public string IncludingGPU { set; get; }
        public int Rank { set; get; }

        public Processor(int id, string name, string vender, string generation, string memType, string incGPU, int rank)
        {
            Id = id;
            Name = name;
            Vender = vender;
            Generation = generation;
            MemoryType = memType;
            IncludingGPU = incGPU;
            Rank = rank;
        }
    }
}
