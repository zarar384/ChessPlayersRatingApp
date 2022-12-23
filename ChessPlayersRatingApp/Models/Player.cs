using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessPlayersRatingApp.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public int Rank { get; set; }
        [Required]
        public string Name { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public int Rating { get; set; }
        public int Games { get; set; }
        public int BirthYear { get; set; }
        public int InformationId { get; set; }
        
    }
}
