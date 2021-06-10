using CsvHelper;
using RankingYourComputer.Controllers.Resourses;
using RankingYourComputer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RankingYourComputer.Controllers
{
    class ContextDatаGame : IStrategyData
    {
        public DataSingleton Singleton;

        public ContextDatаGame()
        {
            Downloader downloader = new Downloader("GameData.csv");
            Thread.Sleep(250);
            Singleton = DataSingleton.getInstance();
        }

        public void SetDataFromFile(string pathFile)
        {
            StreamReader streamReader = new StreamReader(pathFile);
            CsvReader csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);

            while (csvReader.Read())
            {
                if (csvReader.GetField(0) != "Rank")
                {
                    string str = csvReader.GetField(0);
                    Singleton.Games.Add(
                    new Game
                    {
                        Rank = Convert.ToInt32(csvReader.GetField(0)),
                        Name = csvReader.GetField(1),
                        Genre = csvReader.GetField(2),
                        minRequirements = new Requierement
                        {
                            Type = "Minimal",
                            ProcessorName = csvReader.GetField(3),
                            ProcessorRank = Convert.ToInt32(csvReader.GetField(4)),
                            VideocardName = csvReader.GetField(5),
                            VideocardRank = Convert.ToInt32(csvReader.GetField(6)),
                            MemoryValue = Convert.ToInt32(csvReader.GetField(7)),
                            HardValue = csvReader.GetField(8),
                            SystemName = csvReader.GetField(9),
                            xVersion = csvReader.GetField(10),
                        },
                        recRequirements = new Requierement
                        {
                            Type = "Recomendent",
                            ProcessorName = csvReader.GetField(11),
                            ProcessorRank = Convert.ToInt32(csvReader.GetField(12)),
                            VideocardName = csvReader.GetField(13),
                            VideocardRank = Convert.ToInt32(csvReader.GetField(14)),
                            MemoryValue = Convert.ToInt32(csvReader.GetField(15)),
                            HardValue = csvReader.GetField(16),
                            SystemName = csvReader.GetField(17),
                            xVersion = csvReader.GetField(18),
                        }
                    });
                }

            }
        }
    }
}
