using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YandexDisk.Client;
using YandexDisk.Client.Http;

namespace RankingYourComputer.Controllers.Resourses
{
    public class Downloader
    {
        public Downloader(string file)
        {
            string str = Assembly.GetExecutingAssembly().Location;
            string filePath = str.Remove(str.Length - 23, 23);
            string Path = filePath + "\\" + file;
            if (File.Exists(Path))
                File.Delete(Path);
            WebClient webClient = new WebClient();
            string sourceFile = GetFileLink(file);
            webClient.DownloadFileAsync(new Uri(sourceFile), Path);
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(AfterDownload);
        }

        private string GetFileLink(string file)
        {
            //You should have oauth token from Yandex Passport.
            //See https://tech.yandex.ru/oauth/
            string oauthToken = "AQAAAABAdszkAAcfhoC3M71Vzkl5pDTQ7p19Nz0";

            // Create a client instance
            IDiskApi diskApi = new DiskHttpApi(oauthToken);

            //Upload file from local
            string URL = diskApi.Files.GetDownloadLinkAsync("Project Folder/" + file, cancellationToken: CancellationToken.None).Result.Href;
            return URL;
        }
        private void AfterDownload(object sender, AsyncCompletedEventArgs e)
        {

        }
    }
}
