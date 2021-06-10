using Microsoft.Win32;
using RankingYourComputer.Controllers.Resourses;
using RankingYourComputer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Management;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace RankingYourComputer.Controllers
{
    class ContextPhysicalPC : IStrategyComputer
    {
        public DataSingleton Singleton;
        SQLiteConnection connection;
        static string sPath = Environment.CurrentDirectory + "\\ComputerDB.db";
        static string ConnectionString = @"Data Source=" + sPath + ";Version=3;New=False;Mode=Read";
        public ManagementObjectSearcher mos;

        public ContextPhysicalPC()
        {
            Singleton = DataSingleton.getInstance();
            connection = new SQLiteConnection(ConnectionString);
        }

        public List<string> GetListComputers()
        {
            List<string> computers = Singleton.Computers.Select(n => n.Name).ToList();
            return computers;
        }

        public Computer GetPC(string name)
        {
            Computer computer = Singleton.Computers.Where(n => n.Name == name).FirstOrDefault();
            return computer;
        }

        public void SetInListPC(string name, string processor, string videocard, string memory, string drive, string systemName, string systemVersion)
        {
            PowerShell powerShell = PowerShell.Create();
            Scripts sc = new Scripts();
            CalculatorRank Rank = new CalculatorRank();
            connection.Open();
            string sql = "";
            SQLiteCommand command;
            SQLiteDataReader dataReader;
            /// <summary>
            /// Получение системного имени компьютера
            /// </summary>
            name = Environment.MachineName;
            /// <summary>
            /// Получение названия процессора из системы и по этому названию остальных параметров
            /// </summary>
            this.mos = new ManagementObjectSearcher("root\\CIMV2", sc.scriptProcessor);
            foreach (ManagementObject mo in mos.Get())
            {
                processor = mo["Name"].ToString();
            }
            Processor Processor = null;
            if (processor.Contains("Intel(R)"))
            {
                sql = "select * from CPU where Name = '" + processor + "'";
                command = new SQLiteCommand(sql, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Processor = new Processor(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3), null, null, dataReader.GetInt32(4));
                }
            }
            else
            {
                string[] str = processor.Split(' ');
                sql = "select Name from CPU where Name like '%" + str[0] + " " + str[1] + "%'";
                command = new SQLiteCommand(sql, connection);
                dataReader = command.ExecuteReader();
                List<string> Names = new List<string>();
                while (dataReader.Read())
                {
                    Names.Add(dataReader.GetString(0));
                }
                foreach (string item in Names)
                {
                    if (processor.Contains(item))
                    {
                        processor = item;
                    }
                }
                sql = "select * from CPU where Name = '" + processor + "'";
                command = new SQLiteCommand(sql, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Processor = new Processor(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3), null, null, dataReader.GetInt32(4));
                }
            }
            /// <summary>
            /// Получение названия видеокарты из системы и остальных параметров из бд (по названию)
            /// </summary>
            this.mos = new ManagementObjectSearcher("root\\CIMV2", sc.scriptVideocard);
            List<string> vs = new List<string>();
            foreach (ManagementObject mo in mos.Get())
            {
                vs.Add(mo["Name"].ToString());
            }
            videocard = vs.FirstOrDefault();
            Videocard Videocard = null;
            sql = "select * from GPU where Name like '%" + videocard + "%'";
            command = new SQLiteCommand(sql, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Videocard = new Videocard(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3), dataReader.GetInt32(4), null);
            }
            /// <summary>
            /// Получение характеристик оперативной памяти из системы и остальных параметров из бд (по названию)
            /// </summary>
            string ddr4 = "DDR4";
            string ddr3 = "DDR3";
            string ddr2 = "DDR2";
            int MaxCapacity = 0;
            int Speed = 0;
            string TypeOfMemory = String.Empty;
            //List<String> DataList = new List<string>();
            powerShell.AddScript(sc.scriptMemory);
            Collection<PSObject> results = powerShell.Invoke();
            results = powerShell.Invoke();
            foreach (var item in results)
            {
                int tempSize = 0;
                if (item.ToString().Contains("Size, GB"))
                    int.TryParse(string.Join("", item.ToString().Where(c => char.IsDigit(c))), out tempSize);
                MaxCapacity += tempSize;

                int tempFreq = 0;
                if (item.ToString().Contains("Speed, MHz"))
                {
                    int.TryParse(string.Join("", item.ToString().Where(c => char.IsDigit(c))), out tempFreq);
                    Speed = tempFreq;
                }

                if (item.ToString().Contains(ddr4))
                    TypeOfMemory = ddr4;
                if (item.ToString().Contains(ddr3))
                    TypeOfMemory = ddr3;
                if (item.ToString().Contains(ddr2))
                    TypeOfMemory = ddr2;
            }
            memory = TypeOfMemory + ", " + MaxCapacity + " GB, " + Speed + " MHz";
            PhysicalMemory Memory = null;
            sql = "select * from RAM where Name like '" + memory + "'";
            command = new SQLiteCommand(sql, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Memory = new PhysicalMemory(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetInt32(3), dataReader.GetInt32(4), dataReader.GetInt32(5));
            }
            /// <summary>
            /// Получение характеристик жёсткого диска из системы и остальных параметров
            /// </summary>
            mos = new ManagementObjectSearcher("root\\CIMV2", sc.scriptHardDrive);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (ManagementObject mo in mos.Get())
            {
                string size = String.Empty;
                drive = mo["Model"].ToString();
                if (Math.Round(Convert.ToDouble(mo["Size"]) / 1073741824, 1) > 1000)
                {
                    size = Math.Round((Convert.ToDouble(mo["Size"]) / 1073741824) / 1024, 1) + " TB";
                }
                else
                {
                    size = Math.Round(Convert.ToDouble(mo["Size"]) / 1073741824, 1) + " GB";
                }
                dict.Add(drive, size);
            }
            HardDrive Drive = null;
            int maxRank = 0;
            foreach (var item in dict)
            {
                string[] str = item.Key.Split(' ');
                if (str.Length < 2)
                {
                    sql = "select * from HARD where Name like '%" + str[0] + "%' and Value = '" + item.Value + "'";
                    command = new SQLiteCommand(sql, connection);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        if (dataReader.GetInt32(5) > maxRank)
                        {
                            Drive = new HardDrive(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3), dataReader.GetString(4), dataReader.GetInt32(5));
                            maxRank = dataReader.GetInt32(5);
                        }
                    }
                }
                else
                {
                    sql = "select * from HARD where Name like '%" + str[0] + " " + str[1] + "%' and Value = '" + item.Value + "'";
                    command = new SQLiteCommand(sql, connection);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        if (dataReader.GetInt32(5) > maxRank)
                        {
                            Drive = new HardDrive(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3), dataReader.GetString(4), dataReader.GetInt32(5));
                            maxRank = dataReader.GetInt32(5);
                        }
                    }
                }
            }
            /// <summary>
            /// Получение параметров операционной системы
            /// </summary>
            String subKey = @"SOFTWARE\Wow6432Node\Microsoft\Windows NT\CurrentVersion";
            RegistryKey key = Registry.LocalMachine;
            RegistryKey skey = key.OpenSubKey(subKey);
            OperationSystem System = null;
            System = new OperationSystem(skey.GetValue("ProductName").ToString(), (Environment.Is64BitOperatingSystem ? 64 : 32) + " Bit");
            /// <summary>
            /// Расчёт рейтинга системы
            /// </summary>
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
            Singleton.ComputerName = name;
            connection.Close();
        }
    }
}
