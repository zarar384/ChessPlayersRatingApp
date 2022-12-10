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

        public static byte[] GetPhotoAsync(int PlayerId)
        {
            var db = new AppDbContext();
            var player = db.Players.Find(PlayerId);

            //var deserializeStr = Newtonsoft.Json.JsonConvert.DeserializeObject(strFromPage);
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
            var strFromPage = wiki.DownloadString("https://en.wikipedia.org/api/rest_v1/page/summary/" + GetCorrectNameFormat(name));
            wiki.Headers.Add("Api-User-Agent", "Example/1.0");
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
