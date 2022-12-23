using ChessPlayersRatingApp.Data;
using ChessPlayersRatingApp.Models;
using ChessPlayersRatingApp.Utility;
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
            PlayersCsv playersCsv = new PlayersCsv();
            playersCsv.PlayersDataEntry("Top100ChessPlayers.csv"); 
            CreateHostBuilder(args).Build().Run();   
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
