namespace ChessPlayersRatingApp.Models
{
    public class Player
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public int Rating { get; set; }
        public int Games { get; set; }
        public int BirthYear { get; set; }

        public static Player ParseFieldScv(string line)
        {
            string[] parts = line.Split(';');
            return new Player
            {
                Rank = int.Parse(parts[0]),
                Name = parts[1].Replace(","," ").Trim(),
                Title = parts[2],
                Country = parts[3],
                Rating = int.Parse(parts[4]),
                Games = int.Parse(parts[5]),
                BirthYear = int.Parse(parts[6])
            };
        }
    }
}
