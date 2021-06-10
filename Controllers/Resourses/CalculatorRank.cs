using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingYourComputer.Controllers.Resourses
{
    public class CalculatorRank
    {
        SQLiteConnection connection;
        static string sPath = Environment.CurrentDirectory + "\\ComputerDB.db";
        static string ConnectionString = @"Data Source=" + sPath + ";Version=3;New=False;Cache=Shared;Mode=ReadWrite";

        public CalculatorRank()
        {
            connection = new SQLiteConnection(ConnectionString);
        }

        public int GetComputerRank(int cpuRank, int gpuRank, int ramRank, int hardRank)
        {
            connection.Open();
            string sql;
            SQLiteCommand command;
            int result;

            sql = "select max(Rank) from CPU";
            command = new SQLiteCommand(sql, connection);
            double cpuMax = Convert.ToDouble(command.ExecuteScalar());
            sql = "select max(Rank) from GPU";
            command = new SQLiteCommand(sql, connection);
            double gpuMax = Convert.ToDouble(command.ExecuteScalar());
            sql = "select max(Rank) from RAM";
            command = new SQLiteCommand(sql, connection);
            double ramMax = Convert.ToDouble(command.ExecuteScalar());
            sql = "select max(Rank) from HARD";
            command = new SQLiteCommand(sql, connection);
            double hardMax = Convert.ToDouble(command.ExecuteScalar());
            double[] numsArray = { Convert.ToDouble((cpuRank / cpuMax) * 0.3), Convert.ToDouble((gpuRank / gpuMax) * 0.3), Convert.ToDouble((ramRank / ramMax) * 0.25), Convert.ToDouble((hardRank / hardMax) * 0.15) };
            result = Convert.ToInt32(numsArray.Sum() * ((cpuMax + gpuMax + ramMax + hardMax) / 4));
            connection.Close();
            return result;
        }
        public int GetMaxRank()
        {
            connection.Open();
            string sql;
            SQLiteCommand command;
            int result;

            sql = "select max(Rank) from CPU";
            command = new SQLiteCommand(sql, connection);
            double cpuMax = Convert.ToDouble(command.ExecuteScalar());
            sql = "select max(Rank) from GPU";
            command = new SQLiteCommand(sql, connection);
            double gpuMax = Convert.ToDouble(command.ExecuteScalar());
            sql = "select max(Rank) from RAM";
            command = new SQLiteCommand(sql, connection);
            double ramMax = Convert.ToDouble(command.ExecuteScalar());
            sql = "select max(Rank) from HARD";
            command = new SQLiteCommand(sql, connection);
            double hardMax = Convert.ToDouble(command.ExecuteScalar());

            result = Convert.ToInt32((cpuMax + gpuMax + ramMax + hardMax) / 4);
            connection.Close();
            return result;
        }
    }
}
