using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ChessPlayersRatingApp.Models.ViewModel
{
    public class InformationView
    {
        public Information Information { get; set; }
        public Player Player{ get; set; }
    }
}
