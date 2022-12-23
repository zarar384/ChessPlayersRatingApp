using ChessPlayersRatingApp.Data;
using ChessPlayersRatingApp.Models;
using System.IO;
using System.Linq;

namespace ChessPlayersRatingApp.Utility
{
    public class PlayersCsv
    {
        private static Player ParseFieldScv(string line)
        {
            string[] parts = line.Split(';');
            return new Player
            {
                Rank = int.Parse(parts[0]),
                Name = parts[1].Replace(",", " ").Trim(),
                Title = parts[2],
                Country = parts[3],
                Rating = int.Parse(parts[4]),
                Games = int.Parse(parts[5]),
                BirthYear = int.Parse(parts[6])
            };
        }

        public void PlayersDataEntry(string file)
        {
            using (AppDbContext db = new AppDbContext())
            {
                if (!db.Players.Any())
                {
                    var players = File.ReadAllLines(file)
                                 .Skip(1)
                                 .Select(ParseFieldScv)
                                 .Take(100);
                    foreach (var item in players)
                    {
                        db.Players.Add(new Player
                        {
                            Rank = item.Rank,
                            Name = item.Name,
                            Title = item.Title,
                            Country = item.Country,
                            Rating = item.Rating,
                            Games = item.Games,
                            BirthYear = item.BirthYear
                        }); ;
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}
