using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingYourComputer.Models
{
    public class PhysicalMemory
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Type { set; get; }
        public int Value { set; get; }
        public int Frequency { set; get; }
        public int Rank { set; get; }

        public PhysicalMemory(int id, string name, string type, int value, int frequency, int rank)
        {
            Id = id;
            Name = name;
            Type = type;
            Value = value;
            Frequency = frequency;
            Rank = rank;
        }
    }
}
