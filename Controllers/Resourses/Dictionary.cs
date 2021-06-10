using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingYourComputer.Controllers.Resourses
{
    class DictElement
    {
        public string Target { set; get; }
        public dynamic Contition { set; get; }
        public string Result { set; get; }
    }
    class Dictionary
    {
        public List<DictElement> DictionarySituation = new List<DictElement>();
        public Dictionary()
        {
            SetElement();
        }

        void SetElement()
        {
            string[] lines = System.IO.File.ReadAllLines(Environment.CurrentDirectory + "\\Dictionary.txt");
            foreach (string line in lines)
            {
                string[] str = line.Split(';');
                DictionarySituation.Add(new DictElement
                {
                    Target = str[0],
                    Contition = str[1],
                    Result = str[2]
                });
            }
        }
    }
}
