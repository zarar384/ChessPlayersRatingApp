namespace ChessPlayersRatingApp.Models.DTO
{
    public class InformationDto
    {
        public int Id { get; set; }
        public string BaseInfoText { get; set; } //representation
        public byte[] Image { get; set; } //base64 encoded jpg
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
    }
}
