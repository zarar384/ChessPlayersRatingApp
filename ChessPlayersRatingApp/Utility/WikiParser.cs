using ChessPlayersRatingApp.Data;
using ChessPlayersRatingApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChessPlayersRatingApp.Utility
{
    public class WikiParser
    {
        private static readonly WebClient wiki = new();

        public static string GetExtract(int PlayerId)
        {
            var _db = new AppDbContext();
            var player = _db.Players.Find(PlayerId);

            var data = (JObject)JsonConvert.DeserializeObject(GetDataFromWikiAPI(player.Name));
            var extractStr = data["extract"].Value<string>();//get value from key "extract"
            return extractStr;
        }

        public static byte[] GetPhoto(int PlayerId)
        {
            var db = new AppDbContext();
            var player = db.Players.Find(PlayerId);
            wiki.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; " +
                                  "Windows NT 5.2; .NET CLR 1.0.3705;)");
            wiki.Headers["UserAgent"] = "appname";

            var data = (JObject)JsonConvert.DeserializeObject(GetDataFromWikiAPI(player.Name));
            try
            {
                var urlPhoto = data["thumbnail"]["source"];//TODO
                if (urlPhoto != null)
                {
                    return wiki.DownloadData(urlPhoto.ToString());
                }
            }
            catch
            {
                return System.Text.Encoding.ASCII.GetBytes("Not Image");
            }
            return null;
        }

        private static string GetDataFromWikiAPI(string name)
        {
            wiki.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; " +
                                  "Windows NT 5.2; .NET CLR 1.0.3705;)");
            wiki.Headers["UserAgent"] = "appname";
            string strFromPage =" ";
            try
            {
                 strFromPage = wiki.DownloadString("https://en.wikipedia.org/api/rest_v1/page/summary/" + name);
            }
            catch(WebException ex)
            {
                var resp = (HttpWebResponse)ex.Response;
                if (resp.StatusCode == HttpStatusCode.NotFound) // HTTP 404
                {
                    strFromPage = wiki.DownloadString("https://en.wikipedia.org/api/rest_v1/page/summary/" + GetCorrectNameFormat(name));
                }
            }
            return strFromPage;
        }
        private static string GetCorrectNameFormat(string name)
        {
            string[] strArr = name.Split(' ');
            string correctFormatName = strArr[2] + "_" + strArr[0];//correct format for getting json

            return correctFormatName;
        }
    }
}
