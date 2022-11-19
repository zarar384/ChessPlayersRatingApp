using ChessPlayersRatingApp.Data;
using ChessPlayersRatingApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            var db = new AppDbContext();
            var player = db.Players.Find(PlayerId);

            var strFromPage = wiki.DownloadString("https://en.wikipedia.org/api/rest_v1/page/summary/" + GetCorrectNameFormat(player.Name));
            var data = (JObject)JsonConvert.DeserializeObject(strFromPage);
            var extractStr = data["extract"].Value<string>();//get value from key "extract"
            return extractStr;
        }

        public static async Task<byte[]> GetPhotoAsync(int PlayerId)
        {
            var db = new AppDbContext();
            var player = db.Players.Find(PlayerId);

            var strFromPage = wiki.DownloadString("https://en.wikipedia.org/api/rest_v1/page/summary/" + GetCorrectNameFormat(player.Name));
            //var deserializeStr = Newtonsoft.Json.JsonConvert.DeserializeObject(strFromPage);
            var data = (JObject)JsonConvert.DeserializeObject(strFromPage);
            var c = data["thumbnail"]["source"];
            if (c != null)
            {
                var client = new HttpClient();
                var bytes = await client.GetByteArrayAsync(c.ToString());

                return bytes;
            }

            return null;
        }

        private static string GetCorrectNameFormat(string name)
        {
            string[] strArr = name.Split(' ');
            string correctFormatName = strArr[2] + "_" + strArr[0];//correct format for getting json

            return correctFormatName;
        }
    }
}
