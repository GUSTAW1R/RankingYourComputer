using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RankingYourComputer.Controllers.Resourses
{
    public class Searcher
    {
        public Searcher(string SearchField)
        {
            string[] array = SearchField.Split(' ');
            string ForBrowser = String.Empty;
            for (int i = 0; i < array.Length; i++)
            {
                ForBrowser += array[i] + "%20";
            }
            string Url = "https://www.google.com/search?q=" + ForBrowser;
            OpenBrowser(Url);
        }

        public void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
