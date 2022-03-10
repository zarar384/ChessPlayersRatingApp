using ChessPlayersRatingApp.Data;
using ChessPlayersRatingApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChessPlayersRatingApp
{
    public class Program
    {

        public static void Main(string[] args)
        {
            PlayersDataEntry("Top100ChessPlayers.csv"); 
            CreateHostBuilder(args).Build().Run();   
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        static void PlayersDataEntry(string file)
        {
            using (AppDbContext db = new AppDbContext())
            {

                var players = File.ReadAllLines(file)
                             .Skip(1)
                             .Select(Player.ParseFieldScv)
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
                if (!db.Players.Any())
                {
                    db.SaveChanges();
                }               
            }
        }
    }
}
