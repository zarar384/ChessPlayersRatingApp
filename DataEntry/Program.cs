using ChessPlayersRatingApp.Data;
using ChessPlayersRatingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace DataEntry
{
    class Program
    {

        static void Main(string[] args)
        {
            //DisplayChessPlayers("top100playerChess.csv");
            Console.ReadLine();
        }

        //static void DisplayChessPlayers(string file)
        //{
        //    var players = File.ReadAllLines(file)
        //                         .Skip(1)
        //                         .Select(Player.ParseFieldScv)
        //                         .Take(10);
        //    foreach (var player in players)
        //    {
        //        .Players.Add(new Player
        //        {
        //            Rank = player.Rank,
        //            Name = player.Name,
        //            Title = player.Title,
        //            Country = player.Country,
        //            Rating = player.Rating,
        //            Games = player.Games,
        //            BirthYear = player.BirthYear
        //        }); ;
        //    }
        //}
    }
}
