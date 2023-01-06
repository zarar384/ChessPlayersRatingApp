namespace ChessPlayersRatingApp.Models.DTO
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public int Rating { get; set; }
        public int Games { get; set; }
        public int BirthYear { get; set; }
        public int InformationId { get; set; }

    }
}
