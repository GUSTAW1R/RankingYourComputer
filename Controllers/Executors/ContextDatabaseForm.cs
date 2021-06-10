using RankingYourComputer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RankingYourComputer.Controllers
{
    class OutputTable
    {
        public int Rank { set; get; }
        public string Name { set; get; }
        public string Genre { set; get; }
        public string Minimal { set; get; }
        public string Recomended { set; get; }
        public string ColorSet { set; get; }

        public OutputTable(int rank, string name, string genre, string min, string rec, string color)
        {
            Rank = rank;
            Name = name;
            Genre = genre;
            Minimal = min;
            Recomended = rec;
            ColorSet = color;
        }
    }

    class ContextDatabaseForm : IStrategyForm
    {
        public DataSingleton Singleton;
        SQLiteConnection connection;
        static string sPath = Environment.CurrentDirectory + "\\ComputerDB.db";
        static string ConnectionString = @"Data Source=" + sPath + ";Version=3;Cache=Shared;Mode=Read";

        public ContextDatabaseForm()
        {
            Singleton = DataSingleton.getInstance();
            connection = new SQLiteConnection(ConnectionString);
        }

        public DataGrid SetDataToDataGrid(DataGrid dataGrid, string type, Computer computer, List<Game> games)
        {
            dataGrid.Items.Clear();
            List<int> CPUminRank = games.Select(g => g.recRequirements.ProcessorRank).Take(25).ToList();
            List<int> CPUrecRank = games.Select(g => g.recRequirements.ProcessorRank).Take(25).ToList();
            List<int> GPUminRank = games.Select(g => g.minRequirements.VideocardRank).Take(25).ToList();
            List<int> GPUrecRank = games.Select(g => g.recRequirements.VideocardRank).Take(25).ToList();
            List<int> RAMminvalue = games.Select(g => g.minRequirements.MemoryValue).Take(25).ToList();
            List<int> RAMrecvalue = games.Select(g => g.recRequirements.MemoryValue).Take(25).ToList();
            List<string> HARDminvalue = games.Select(g => g.minRequirements.HardValue).Take(25).ToList();
            List<string> HARDrecvalue = games.Select(g => g.recRequirements.HardValue).Take(25).ToList();
            List<string> OSNameminvalue = games.Select(g => g.minRequirements.SystemName).Take(25).ToList();
            List<string> OSNamerecvalue = games.Select(g => g.recRequirements.SystemName).Take(25).ToList();
            List<string> OSxVersionminvalue = games.Select(g => g.minRequirements.xVersion).Take(25).ToList();
            List<string> OSxVersionrecvalue = games.Select(g => g.recRequirements.xVersion).Take(25).ToList();

            switch (type)
            {
                case "Start Info":
                    for (int i = 0; i < 25; i++)
                    {
                        dataGrid.Items.Add(new OutputTable(games.Select(g => g.Rank).ElementAt(i),
                                games.Select(g => g.Name).ElementAt(i),
                                games.Select(g => g.Genre).ElementAt(i),
                                "", "", "White"));
                    }
                    break;
                case "Compare Info":
                    connection.Open();
                    string[] str = computer.System.Name.Split(' ');
                    string sql = "select distinct Gen from OS where Name like '%" + str[0] + " " + str[1] + "%'";
                    SQLiteCommand command = new SQLiteCommand(sql, connection);
                    int GenComputer = Convert.ToInt32(command.ExecuteScalar());
                    for (int i = 0; i < 25; i++)
                    {
                        double CompSize = 0;
                        double GameSize = 0;
                        double.TryParse(string.Join("", games.Select(g => g.minRequirements.HardValue).ToString().Where(c => char.IsDigit(c))), out GameSize);
                        double.TryParse(string.Join("", computer.Drive.Value.ToString().Where(c => char.IsDigit(c))), out CompSize);
                        if (computer.Drive.Value.Contains("TB"))
                        {
                            CompSize = CompSize * 1024;
                        }
                        int computerVersion = 0;
                        int GameVersion = 0;
                        int.TryParse(string.Join("", OSxVersionminvalue[i].ToString().Where(c => char.IsDigit(c))), out GameVersion);
                        int.TryParse(string.Join("", computer.System.xVersion.ToString().Where(c => char.IsDigit(c))), out computerVersion);



                        str = OSNameminvalue[i].Split(' ');
                        sql = "select distinct Gen from OS where Name like '%" + str[0] + " " + str[1] + "%'";
                        command = new SQLiteCommand(sql, connection);
                        int GenGame = Convert.ToInt32(command.ExecuteScalar());
                        if (computer.Processor.Rank >= games.Select(g => g.minRequirements.ProcessorRank).ElementAt(i) && computer.Videocard.Rank >= GPUminRank[i] && computer.Memory.Value >= RAMminvalue[i] && CompSize >= GameSize && GenComputer >= GenGame && computerVersion >= GameVersion)
                        {
                            str = OSNamerecvalue[i].Split(' ');
                            sql = "select Gen from OS where Name like '%" + str[0] + str[1] + "%'";
                            command = new SQLiteCommand(sql, connection);
                            GenGame = Convert.ToInt32(command.ExecuteScalar());
                            GameVersion = 0;
                            int.TryParse(string.Join("", OSxVersionrecvalue[i].ToString().Where(c => char.IsDigit(c))), out GameVersion);
                            if (computer.Processor.Rank >= CPUrecRank[i] && computer.Videocard.Rank >= GPUrecRank[i] && computer.Memory.Value >= RAMrecvalue[i] && CompSize >= GameSize && GenComputer >= GenGame && computerVersion >= GameVersion)
                            {
                                dataGrid.Items.Add(new OutputTable(games.Select(g => g.Rank).ElementAt(i),
                                games.Select(g => g.Name).ElementAt(i),
                                games.Select(g => g.Genre).ElementAt(i),
                                "Удовлетворяет", "Удовлетворяет", "Lime"));
                            }
                            else
                            {
                                dataGrid.Items.Add(new OutputTable(games.Select(g => g.Rank).ElementAt(i),
                                games.Select(g => g.Name).ElementAt(i),
                                games.Select(g => g.Genre).ElementAt(i),
                                "Удовлетворяет", "Не удовлетворяет", "Yellow"));
                            }
                        }
                        else
                        {
                            dataGrid.Items.Add(new OutputTable(games.Select(g => g.Rank).ElementAt(i),
                                games.Select(g => g.Name).ElementAt(i),
                                games.Select(g => g.Genre).ElementAt(i),
                                "Не удовлетворяет", "Не удовлетворяет", "Red"));
                        }
                    }
                    connection.Close();
                    break;
            }
            return dataGrid;
        }

        public List<string> SetDataToForm(string param1, string param2, string type, string category)
        {
            SQLiteCommand command;
            SQLiteDataReader dataReader;
            string sql = "";
            connection.Open();
            List<String> data = new List<string>();
            switch (type)
            {
                case "InfoCPU":
                    switch (category)
                    {
                        case "Vender":
                            sql = "select distinct Vender from InfoCPU";
                            command = new SQLiteCommand(sql, connection);
                            dataReader = command.ExecuteReader();
                            while (dataReader.Read())
                            {
                                data.Add(dataReader.GetString(0));
                            }
                            break;
                        case "Generation":
                            sql = "select distinct Generation from InfoCPU where Vender = '" + param1 + "'";
                            command = new SQLiteCommand(sql, connection);
                            dataReader = command.ExecuteReader();
                            while (dataReader.Read())
                            {
                                data.Add(dataReader.GetString(0));
                            }
                            break;
                        case "Name":
                            sql = "select distinct Name from InfoCPU where Generation = '" + param1 + "'";
                            command = new SQLiteCommand(sql, connection);
                            dataReader = command.ExecuteReader();
                            while (dataReader.Read())
                            {
                                data.Add(dataReader.GetString(0));
                            }
                            break;
                    }
                    break;
                case "InfoGPU":
                    switch (category)
                    {
                        case "Vender":
                            if (param1 != null)
                            {
                                sql = "select IncludingGPU from InfoCPU where Name = '" + param1 + "'";
                                command = new SQLiteCommand(sql, connection);
                                string str = Convert.ToString(command.ExecuteScalar());
                                if (str == "None")
                                {
                                    sql = "select distinct Vender from InfoGPU where NOT Vender = 'Intel'";
                                    command = new SQLiteCommand(sql, connection);
                                    dataReader = command.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        data.Add(dataReader.GetString(0));
                                    }
                                }
                                else
                                {
                                    if (str.Contains("Intel"))
                                    {
                                        sql = "select distinct Vender from InfoGPU";
                                        command = new SQLiteCommand(sql, connection);
                                        dataReader = command.ExecuteReader();
                                        while (dataReader.Read())
                                        {
                                            data.Add(dataReader.GetString(0));
                                        }
                                    }
                                    else
                                    {
                                        sql = "select distinct Vender from InfoGPU where NOT Vender = 'Intel'";
                                        command = new SQLiteCommand(sql, connection);
                                        dataReader = command.ExecuteReader();
                                        while (dataReader.Read())
                                        {
                                            data.Add(dataReader.GetString(0));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                sql = "select distinct Vender from InfoGPU where NOT Vender = 'Intel'";
                                command = new SQLiteCommand(sql, connection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    data.Add(dataReader.GetString(0));
                                }
                            }
                            break;
                        case "Generation":
                            sql = "select distinct IncludingGPU from InfoCPU where Name = '" + param2 + "'";
                            command = new SQLiteCommand(sql, connection);
                            string including = command.ExecuteScalar().ToString();
                            if (including == "None")
                            {
                                sql = "select distinct Generation from InfoGPU where Vender = '" + param1 + "' and IsIntegrated = 'False'";
                                command = new SQLiteCommand(sql, connection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    data.Add(dataReader.GetString(0));
                                }
                            }
                            else
                            {
                                sql = "select distinct Generation from InfoGPU where Vender = '" + param1 + "'";
                                command = new SQLiteCommand(sql, connection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    data.Add(dataReader.GetString(0));
                                }
                            }
                            break;
                        case "Name":
                            sql = "select distinct IncludingGPU from InfoCPU where Name = '" + param2 + "'";
                            command = new SQLiteCommand(sql, connection);
                            including = command.ExecuteScalar().ToString();
                            if (including == "None")
                            {
                                sql = "select distinct Name from InfoGPU where Generation = '" + param1 + "' and IsIntegrated = 'False'";
                                command = new SQLiteCommand(sql, connection);
                                dataReader = command.ExecuteReader();
                                while (dataReader.Read())
                                {
                                    data.Add(dataReader.GetString(0));
                                }
                            }
                            else
                            {
                                sql = "select Generation from InfoGPU where Name = '" + including + "'";
                                command = new SQLiteCommand(sql, connection);
                                string checkGeneration = Convert.ToString(command.ExecuteScalar());
                                if (param1 == checkGeneration)
                                {
                                    data.Add(including);
                                }
                                else
                                {
                                    sql = "select distinct Name from InfoGPU where Generation = '" + param1 + "' and IsIntegrated = 'False'";
                                    command = new SQLiteCommand(sql, connection);
                                    dataReader = command.ExecuteReader();
                                    while (dataReader.Read())
                                    {
                                        data.Add(dataReader.GetString(0));
                                    }
                                }
                            }
                            break;
                    }
                    break;
                case "RAM":
                    switch (category)
                    {
                        case "Type":
                            sql = "select distinct MemoryType from InfoCPU where Name = '" + param1 + "'";
                            command = new SQLiteCommand(sql, connection);
                            data.Add(command.ExecuteScalar().ToString());
                            break;
                        case "Value":
                            sql = "select distinct Value from RAM where Type = '" + param1 + "'";
                            command = new SQLiteCommand(sql, connection);
                            dataReader = command.ExecuteReader();
                            while (dataReader.Read())
                            {
                                data.Add(dataReader.GetInt32(0).ToString());
                            }
                            break;
                        case "Frequency":
                            sql = "select distinct Frequency from RAM where Value = '" + param1 + "' and Type = '" + param2 + "'";
                            command = new SQLiteCommand(sql, connection);
                            dataReader = command.ExecuteReader();
                            while (dataReader.Read())
                            {
                                data.Add(dataReader.GetInt32(0).ToString());
                            }
                            break;
                    }
                    break;
                case "InfoHARD":
                    switch (category)
                    {
                        case "Type":
                            sql = "select distinct Type from InfoHARD";
                            command = new SQLiteCommand(sql, connection);
                            dataReader = command.ExecuteReader();
                            while (dataReader.Read())
                            {
                                data.Add(dataReader.GetString(0));
                            }
                            break;
                        case "Company":
                            sql = "select distinct Company from InfoHARD where Type = '" + param1 + "'";
                            command = new SQLiteCommand(sql, connection);
                            dataReader = command.ExecuteReader();
                            while (dataReader.Read())
                            {
                                data.Add(dataReader.GetString(0));
                            }
                            break;
                        case "Value":
                            sql = "select distinct Value from InfoHARD where Company = '" + param1 + "' and Type = '" + param2 + "'";
                            command = new SQLiteCommand(sql, connection);
                            dataReader = command.ExecuteReader();
                            while (dataReader.Read())
                            {
                                data.Add(dataReader.GetString(0));
                            }
                            break;
                        case "Name":
                            string[] str = param2.Split('&');
                            sql = "select distinct Name from InfoHARD where Value = '" + param1 + "' and Company = '" + str[0] + "' and Type = '" + str[1] + "'";
                            command = new SQLiteCommand(sql, connection);
                            dataReader = command.ExecuteReader();
                            while (dataReader.Read())
                            {
                                data.Add(dataReader.GetString(0));
                            }
                            break;
                    }
                    break;
                case "OS":
                    switch (category)
                    {
                        case "Name":
                            sql = "select distinct Name from OS";
                            command = new SQLiteCommand(sql, connection);
                            dataReader = command.ExecuteReader();
                            while (dataReader.Read())
                            {
                                data.Add(dataReader.GetString(0));
                            }
                            break;
                        case "Version":
                            sql = "select distinct Version from OS where Name = '" + param1 + "'";
                            command = new SQLiteCommand(sql, connection);
                            dataReader = command.ExecuteReader();
                            while (dataReader.Read())
                            {
                                data.Add(dataReader.GetString(0));
                            }
                            break;
                    }
                    break;

            }
            connection.Close();
            return data;
        }

        public string SetDataToTextBox(string Type, string Param, string name = null)
        {
            string result = String.Empty;
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();
            switch (Type)
            {
                case "Name":
                    result = "For " + Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(n => n.Name).FirstOrDefault();
                    break;
                case "CPU":
                    int rank = Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(r => r.recRequirements.ProcessorRank).FirstOrDefault();
                    string sql = "select Name from InfoCPU where Rank >= '" + rank + "' order by Rank ASC Limit 1";
                    SQLiteCommand command = new SQLiteCommand(sql, connection);
                    result = Convert.ToString(command.ExecuteScalar());
                    break;
                case "GPU":
                    rank = Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(r => r.recRequirements.VideocardRank).FirstOrDefault();
                    sql = "select IncludingGPU from InfoCPU where Name = '" + Param + "'";
                    command = new SQLiteCommand(sql, connection);
                    string str = command.ExecuteScalar().ToString();
                    if (str == "None")
                    {
                        sql = "select Name from InfoGPU where Rank >= '" + rank + "' and IsIntegrated = 'False' order by Rank ASC Limit 1";
                        command = new SQLiteCommand(sql, connection);
                        result = Convert.ToString(command.ExecuteScalar());
                    }
                    else
                    {
                        sql = "select Rank from InfoGPU where Name = '" + str + "' and Rank > '" + rank + "'";
                        command = new SQLiteCommand(sql, connection);
                        int checkRank = Convert.ToInt32(command.ExecuteScalar());
                        sql = "select Rank from InfoGPU where Rank >= '" + rank + "' and IsIntegrated = 'False' order by Rank ASC Limit 1";
                        command = new SQLiteCommand(sql, connection);
                        int searchedRank = Convert.ToInt32(command.ExecuteScalar());
                        if (checkRank < searchedRank && checkRank != 0)
                            result = str;
                        else
                        {
                            sql = "select Name from InfoGPU where Rank = '" + searchedRank + "'";
                            command = new SQLiteCommand(sql, connection);
                            result = Convert.ToString(command.ExecuteScalar());
                        }
                    }
                    break;
                case "RAM":
                    int ramSize = Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(r => r.recRequirements.MemoryValue).FirstOrDefault();
                    sql = "select MemoryType from InfoCPU where Name = '" + Param + "'";
                    command = new SQLiteCommand(sql, connection);
                    str = command.ExecuteScalar().ToString();
                    sql = "select Name from RAM where Value = '" + ramSize + "' and Type = '" + str + "' order by Frequency ASC Limit 1 OFFSET 2";
                    command = new SQLiteCommand(sql, connection);
                    result = Convert.ToString(command.ExecuteScalar());
                    break;
                case "HARD":
                    Random rnd = new Random();
                    sql = "select Name from InfoHard where Value like '%93%' or Value like '%46%' or Value like '%44%' order by Rank ASC Limit 1 OFFSET " + rnd.Next(1, 8) + "";
                    command = new SQLiteCommand(sql, connection);
                    result = Convert.ToString(command.ExecuteScalar());
                    break;
                case "OS":
                    string sysName = Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(r => r.recRequirements.SystemName).FirstOrDefault();
                    string sysVersion = Singleton.Games.Where(r => r.Rank == Singleton.GamePosition).Select(r => r.recRequirements.xVersion).FirstOrDefault();
                    sql = "select Name, Version from OS where Name like '" + sysName + "%' and Version = '" + sysVersion + "' order by Name ASC Limit 1";
                    command = new SQLiteCommand(sql, connection);
                    SQLiteDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        result = dataReader.GetString(0) + ", " + dataReader.GetString(1);
                    }
                    break;
            }
            return result;
        }
    }
}
