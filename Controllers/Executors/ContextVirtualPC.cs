using RankingYourComputer.Controllers.Resourses;
using RankingYourComputer.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingYourComputer.Controllers
{
    class ContextVirtualPC : IStrategyComputer
    {
        public DataSingleton Singleton;
        SQLiteConnection connection;
        static string sPath = Environment.CurrentDirectory + "\\ComputerDB.db";
        static string ConnectionString = @"Data Source=" + sPath + ";Version=3;Cache=Shared;Mode=Read";

        public ContextVirtualPC()
        {
            Singleton = DataSingleton.getInstance();
            connection = new SQLiteConnection(ConnectionString);

        }

        public Computer GetPC(string name)
        {
            Computer computer = Singleton.Computers.FirstOrDefault(n => n.Name == name);
            return computer;
        }

        public void SetInListPC(string name, string processor, string videocard, string memory, string drive, string systemName, string systemVersion)
        {
            CalculatorRank Rank = new CalculatorRank();
            connection.Open();
            string sql = "";
            SQLiteCommand command;
            SQLiteDataReader dataReader;

            name = name + "  #" + DateTimeOffset.Now.ToUnixTimeSeconds() + "";

            sql = "select * from InfoCPU where Name = '" + processor + "'";
            command = new SQLiteCommand(sql, connection);
            dataReader = command.ExecuteReader();
            Processor Processor = null;
            while (dataReader.Read())
            {
                Processor = new Processor(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3), dataReader.GetString(4), dataReader.GetString(5), dataReader.GetInt32(6));
            }

            sql = "select * from InfoGPU where Name = '" + videocard + "'";
            command = new SQLiteCommand(sql, connection);
            dataReader = command.ExecuteReader();
            Videocard Videocard = null;
            while (dataReader.Read())
            {
                Videocard = new Videocard(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3), dataReader.GetInt32(4), dataReader.GetString(5));
            }

            sql = "select * from RAM where Name = '" + memory + "'";
            command = new SQLiteCommand(sql, connection);
            dataReader = command.ExecuteReader();
            PhysicalMemory Memory = null;
            while (dataReader.Read())
            {
                Memory = new PhysicalMemory(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetInt32(3), dataReader.GetInt32(4), dataReader.GetInt32(5));
            }

            sql = "select * from InfoHARD where Name = '" + drive + "'";
            command = new SQLiteCommand(sql, connection);
            dataReader = command.ExecuteReader();
            HardDrive Drive = null;
            while (dataReader.Read())
            {
                Drive = new HardDrive(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3), dataReader.GetString(4), dataReader.GetInt32(5));
            }

            OperationSystem System = new OperationSystem(systemName, systemVersion);

            int RankPC = Rank.GetComputerRank(Processor.Rank, Videocard.Rank, Memory.Rank, Drive.Rank);
            Singleton.Computers.Add(
                new Computer
                {
                    Name = name,
                    Processor = Processor,
                    Videocard = Videocard,
                    Memory = Memory,
                    Drive = Drive,
                    System = System,
                    RankPC = RankPC
                });
            connection.Close();
            Singleton.ComputerName = name;
        }

        public List<string> GetListComputers()
        {
            List<string> computers = Singleton.Computers.Select(n => n.Name).Skip(1).ToList();
            return computers;
        }
    }
}
