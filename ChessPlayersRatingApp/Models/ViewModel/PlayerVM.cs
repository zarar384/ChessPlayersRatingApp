using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ChessPlayersRatingApp.Models.ViewModel
{
    public class PlayerVM
    {
        public Player Player { get; set; }
        public IEnumerable<SelectListItem> Information { get; set; }
    }
}
