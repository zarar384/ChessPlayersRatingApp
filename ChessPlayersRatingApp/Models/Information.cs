using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ChessPlayersRatingApp.Models
{
    public class Information
    {
        [Key]
        public int Id { get; set; }
        public string BaseInfoText { get; set; } //representation
        public Task<byte[]> Image { get; set; } //base64 encoded jpg
        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }
    }
}
