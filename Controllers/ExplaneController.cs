using RankingYourComputer.Controllers.Resourses;
using RankingYourComputer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingYourComputer.Controllers
{
    class ExplaneController
    {
        private Computer Computer;
        Dictionary dictionary = new Dictionary();
        public ExplaneController(Computer computer)
        {
            this.Computer = computer;
        }

        public List<string> GetDescribeCPU()
        {
            List<string> result = new List<string>();

            if (Computer.Processor.Name.Contains("Xeon") || Computer.Processor.Name.Contains("Pro"))
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "CPU" && r.Contition == "Professional").Select(v => v.Result).FirstOrDefault());
            }
            if (Computer.Processor.Rank <= 2100)
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "CPU" && r.Contition == "No Requier").Select(v => v.Result).FirstOrDefault());
            }
            if (Computer.Processor.Rank > 2100 && Computer.Processor.Rank <= 6368)
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "CPU" && r.Contition == "Min Requier").Select(v => v.Result).FirstOrDefault());
            }
            if (Computer.Processor.Rank > 6368 && Computer.Processor.Rank < 8062)
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "CPU" && r.Contition == "Rec Requier").Select(v => v.Result).FirstOrDefault());
            }
            if (Computer.Processor.Rank >= 8062)
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "CPU" && r.Contition == "Full Requier").Select(v => v.Result).FirstOrDefault());
            }

            return result;
        }

        public List<string> GetDescribeGPU(List<string> cpu)
        {
            List<string> result = new List<string>();
            if (Computer.Videocard.Name.Contains("Quadro") || Computer.Videocard.Name.Contains("FirePro") || Computer.Videocard.Name.Contains("Tesla") || Computer.Videocard.Name.Contains("GRID"))
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "GPU" && r.Contition == "Professional").Select(v => v.Result).FirstOrDefault());
            }
            if (Computer.Videocard.Rank <= 1700)
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "GPU" && r.Contition == "No Requier").Select(v => v.Result).FirstOrDefault());
            }
            if (Computer.Videocard.Rank > 1700 && Computer.Videocard.Rank <= 5344)
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "GPU" && r.Contition == "Min Requier").Select(v => v.Result).FirstOrDefault());
            }
            if (Computer.Videocard.Rank > 5344 && Computer.Videocard.Rank < 9691)
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "GPU" && r.Contition == "Rec Requier").Select(v => v.Result).FirstOrDefault());
            }
            if (Computer.Videocard.Rank >= 9691)
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "GPU" && r.Contition == "Full Requier").Select(v => v.Result).FirstOrDefault());
            }
            result.AddRange(cpu);
            return result;
        }

        List<string> GetDescribeRAM(List<string> gpu)
        {
            List<string> result = new List<string>();
            if (Computer.Memory.Value < 4)
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "RAM" && r.Contition == "No Requier").Select(v => v.Result).FirstOrDefault());
            }
            if (Computer.Memory.Value >= 4 && Computer.Memory.Value < 8)
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "RAM" && r.Contition == "Min Requier").Select(v => v.Result).FirstOrDefault());
            }
            if (Computer.Memory.Value >= 8 && Computer.Memory.Value < 16)
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "RAM" && r.Contition == "Rec Requier").Select(v => v.Result).FirstOrDefault());
            }
            if (Computer.Memory.Value >= 16)
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "RAM" && r.Contition == "Full Requier").Select(v => v.Result).FirstOrDefault());
            }
            if (Computer.Memory.Type == "DDR3")
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "RAM" && r.Contition == "Type Memory").Select(v => v.Result).FirstOrDefault());
            }
            else
            {
                result.Add("Вы используете современный тип памяти DDR4. Дальнейшие рекомендации будут только в плане объёма");
            }

            result.AddRange(gpu);
            return result;
        }

        List<string> GetDescribeHARD(List<string> ram)
        {
            List<string> result = new List<string>();
            double Size = 0;
            double.TryParse(string.Join("", Computer.Drive.Value.ToString().Where(c => char.IsDigit(c))), out Size);
            if (Size <= 128 && !Computer.Drive.Value.Contains("TB"))
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "HARD" && r.Contition == "Min Value").Select(v => v.Result).FirstOrDefault());
            }
            if (Size > 128 && Size <= 400)
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "HARD" && r.Contition == "Norm Value").Select(v => v.Result).FirstOrDefault());
            }
            if (Size > 400 || Computer.Drive.Value.Contains("TB"))
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "HARD" && r.Contition == "Max Value").Select(v => v.Result).FirstOrDefault());
            }
            if (Computer.Drive.TypeConnection.Contains("HDD"))
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "HARD" && r.Contition == "Type Connection").Select(v => v.Result).FirstOrDefault());
            }
            else
            {
                result.Add("Используется современный тип дискового накопилеля, который обеспечивает быструю работу");
            }
            result.AddRange(ram);
            return result;
        }

        List<string> GetDescribeOS(List<string> hard)
        {
            List<string> result = new List<string>();
            if (Computer.System.xVersion.Contains("32"))
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "OS" && r.Contition == "32 Bit").Select(v => v.Result).FirstOrDefault());
            }
            if (Computer.System.xVersion.Contains("64"))
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "OS" && r.Contition == "64 Bit").Select(v => v.Result).FirstOrDefault());
            }
            if (Computer.System.Name.Contains("Windows 7"))
            {
                result.Add(dictionary.DictionarySituation.Where(r => r.Target == "OS" && r.Contition == "Name OS").Select(v => v.Result).FirstOrDefault());
            }
            else
            {
                result.Add("Используется современная операционная система (Windows 8/8.1 или Windows 10)");
            }

            result.AddRange(hard);
            return result;
        }

        public List<string> Resunt()
        {
            List<string> result = new List<string>();
            result.AddRange(GetDescribeOS(GetDescribeHARD(GetDescribeRAM(GetDescribeGPU(GetDescribeCPU())))));
            result.Reverse();
            return result;
        }
    }
}
