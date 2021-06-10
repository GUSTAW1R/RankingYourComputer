using RankingYourComputer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RankingYourComputer.Controllers
{
    class GameInfo
    {
        public string Name { set; get; }
        public string Value { set; get; }
    }

    class ContextCurrentForm : IStrategyForm
    {
        public DataSingleton Singleton;

        public ContextCurrentForm()
        {
            Singleton = DataSingleton.getInstance();
        }

        public DataGrid SetDataToDataGrid(DataGrid dataGrid, string type, Computer computer = null, List<Game> games = null)
        {
            switch (type)
            {
                case "Min":
                    dataGrid.Items.Add(new GameInfo { Name = "Процессор", Value = Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(g => g.minRequirements.ProcessorName).FirstOrDefault() });
                    dataGrid.Items.Add(new GameInfo { Name = "Видеокарта", Value = Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(g => g.minRequirements.VideocardName).FirstOrDefault() });
                    dataGrid.Items.Add(new GameInfo { Name = "Оперативная память", Value = Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(g => g.minRequirements.MemoryValue).FirstOrDefault() + " GB" });
                    dataGrid.Items.Add(new GameInfo { Name = "Дисковое простанство", Value = Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(g => g.minRequirements.HardValue).FirstOrDefault() });
                    dataGrid.Items.Add(new GameInfo
                    {
                        Name = "Операционная система",
                        Value = Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(g => g.minRequirements.SystemName).FirstOrDefault()
                        + ", " + Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(g => g.minRequirements.xVersion).FirstOrDefault()
                    });
                    break;
                case "Rec":
                    dataGrid.Items.Add(new GameInfo { Name = "Процессор", Value = Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(g => g.recRequirements.ProcessorName).FirstOrDefault() });
                    dataGrid.Items.Add(new GameInfo { Name = "Видеокарта", Value = Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(g => g.recRequirements.VideocardName).FirstOrDefault() });
                    dataGrid.Items.Add(new GameInfo { Name = "Оперативная память", Value = Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(g => g.recRequirements.MemoryValue).FirstOrDefault() + " GB" });
                    dataGrid.Items.Add(new GameInfo { Name = "Дисковое простанство", Value = Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(g => g.recRequirements.HardValue).FirstOrDefault() });
                    dataGrid.Items.Add(new GameInfo
                    {
                        Name = "Операционная система",
                        Value = Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(g => g.recRequirements.SystemName).FirstOrDefault()
                        + ", " + Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(g => g.recRequirements.xVersion).FirstOrDefault()
                    });
                    break;
            }

            return dataGrid;
        }

        public List<string> SetDataToForm(string param1, string param2, string type = null, string category = null)
        {
            List<string> color = new List<string>();
            if (Convert.ToInt32(param1) > Convert.ToInt32(param2))
            {
                color.Add("#FF00FF00");
                color.Add("#FFFB6565");
            }
            if (Convert.ToInt32(param1) < Convert.ToInt32(param2))
            {
                color.Add("#FFFB6565");
                color.Add("#FF00FF00");
            }
            if (param1 == param2)
            {
                color.Add("#FF00FF00");
                color.Add("#FF00FF00");
            }
            return color;
        }

        public string SetDataToTextBox(string Type, string Param, string name)
        {
            string textBox = String.Empty;
            Computer computer = Singleton.Computers.Where(c => c.Name == name).FirstOrDefault();
            switch (Type)
            {
                case "Info":
                    switch (Param)
                    {
                        case "Name":
                            textBox = computer.Name;
                            break;
                        case "CPU":
                            textBox = computer.Processor.Name;
                            break;
                        case "GPU":
                            textBox = computer.Videocard.Name;
                            break;
                        case "RAM":
                            textBox = computer.Memory.Name;
                            break;
                        case "HARD":
                            textBox = computer.Drive.Name + ", " + computer.Drive.Value;
                            break;
                        case "OS":
                            textBox = computer.System.Name + ", " + computer.System.xVersion;
                            break;
                    }
                    break;
                case "Rank":
                    switch (Param)
                    {
                        case "CPU":
                            textBox = computer.Processor.Rank.ToString();
                            break;
                        case "GPU":
                            textBox = computer.Videocard.Rank.ToString();
                            break;
                        case "RAM":
                            textBox = computer.Memory.Rank.ToString();
                            break;
                        case "HARD":
                            textBox = computer.Drive.Rank.ToString();
                            break;
                        case "RANK":
                            textBox = computer.RankPC.ToString();
                            break;
                    }
                    break;
            }
            return textBox;
        }
    }
}
