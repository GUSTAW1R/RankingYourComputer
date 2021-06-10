using RankingYourComputer.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace RankingYourComputer.Controllers
{
    class ContextComputerXML : IStrategyData
    {
        public DataSingleton Singleton;
        SQLiteConnection connection;
        static string sPath = Environment.CurrentDirectory + "\\ComputerDB.db";
        static string ConnectionString = @"Data Source=" + sPath + ";Version=3;Cache=Shared;Mode=ReadWrite";

        public ContextComputerXML()
        {
            Singleton = DataSingleton.getInstance();
            connection = new SQLiteConnection(ConnectionString);

        }

        public void SetDataFromFile(string pathFile)
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();
            string sql = "";
            SQLiteCommand command;
            SQLiteDataReader dataReader;

            Processor processor = null;
            Videocard videocard = null;
            PhysicalMemory memory = null;
            HardDrive drive = null;

            using (XmlReader reader = XmlReader.Create(pathFile))
            {
                List<string> param = new List<string>();
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Text:
                            param.Add(reader.Value);
                            break;
                    }
                }

                sql = "select * from InfoCPU where Id = '" + param.ElementAt(1) + "'";
                command = new SQLiteCommand(sql, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    processor = new Processor(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3), dataReader.GetString(4), dataReader.GetString(5), dataReader.GetInt32(6));
                }
                sql = "select * from InfoGPU where Id = '" + param.ElementAt(2) + "'";
                command = new SQLiteCommand(sql, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    videocard = new Videocard(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3), dataReader.GetInt32(4), dataReader.GetString(5));
                }
                sql = "select * from RAM where Id = '" + param.ElementAt(3) + "'";
                command = new SQLiteCommand(sql, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    memory = new PhysicalMemory(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetInt32(3), dataReader.GetInt32(4), dataReader.GetInt32(5));
                }
                sql = "select * from InfoHARD where Id = '" + param.ElementAt(4) + "'";
                command = new SQLiteCommand(sql, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    drive = new HardDrive(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3), dataReader.GetString(4), dataReader.GetInt32(5));
                }
                if (Singleton.Computers.Select(n => n.Name).ToList().Contains(param.ElementAt(0)))
                {
                    MessageBoxResult result = MessageBox.Show("Текущая конфигурация уже есть в списке конфигураций" +
                "\nЗагрузите другую или создайте новую", "Внимание",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Warning);
                }
                else
                {
                    Singleton.Computers.Add(new Computer
                    {
                        Name = param.ElementAt(0),
                        Processor = processor,
                        Videocard = videocard,
                        Memory = memory,
                        Drive = drive,
                        System = new OperationSystem(param.ElementAt(5), param.ElementAt(6)),
                        RankPC = Convert.ToInt32(param.ElementAt(7))
                    });
                    MessageBox.Show("Конфигурация успешно импортирована", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                Singleton.ComputerName = param.ElementAt(0);
            }
            connection.Close();
        }
    }
}
