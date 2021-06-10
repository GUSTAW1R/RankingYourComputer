using RankingYourComputer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RankingYourComputer.Controllers
{
    interface IStrategyForm
    {
        List<string> SetDataToForm(string param1, string param2, string type, string category);
        string SetDataToTextBox(string Type, string Param, string name);
        DataGrid SetDataToDataGrid(DataGrid dataGrid, string type, Computer computer, List<Game> games);
    }
}
