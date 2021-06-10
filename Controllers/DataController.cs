using RankingYourComputer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RankingYourComputer.Controllers
{
    class DataController
    {
        public DataSingleton Singleton;
        public DataController(IStrategyData strategy)
        {
            Singleton = DataSingleton.getInstance();
            Strategy = strategy;
        }

        public IStrategyData Strategy { private get; set; }

        public void SetData(string path)
        {
            Strategy.SetDataFromFile(path);
        }

        public void SaveConfiguration(string pathFile, string name)
        {
            Computer computer = Singleton.Computers.Where(pc => pc.Name.Contains(name)).FirstOrDefault();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(pathFile, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Computer");
                writer.WriteElementString("NamePC", computer.Name);
                writer.WriteElementString("ProcessorID", computer.Processor.Id.ToString());
                writer.WriteElementString("VideocardID", computer.Videocard.Id.ToString());
                writer.WriteElementString("MemoryID", computer.Memory.Id.ToString());
                writer.WriteElementString("DriveID", computer.Drive.Id.ToString());
                writer.WriteElementString("OSName", computer.System.Name);
                writer.WriteElementString("OSxVersion", computer.System.xVersion);
                writer.WriteElementString("Rank", computer.RankPC.ToString());
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
