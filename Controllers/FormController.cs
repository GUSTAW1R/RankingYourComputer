using RankingYourComputer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RankingYourComputer.Controllers
{
    class FormController
    {
        public FormController(IStrategyForm context)
        {
            Strategy = context;
        }

        public IStrategyForm Strategy { private get; set; }

        public List<string> SetDataToForm(string param1, string param2, string type, string category)
        {
            return Strategy.SetDataToForm(param1, param2, type, category);
        }

        public string SetDataToTextBox(string Type, string Param, string name)
        {
            return Strategy.SetDataToTextBox(Type, Param, name);
        }

        public DataGrid SetDataToDataGrid(DataGrid dataGrid, string type, Computer computer, List<Game> games)
        {
            return Strategy.SetDataToDataGrid(dataGrid, type, computer, games);
        }
    }
}
