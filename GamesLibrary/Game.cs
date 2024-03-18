
namespace GamesLibrary
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Studio { get; set; }
        public string Style { get; set; }
        public DateTime ReleaseDate { get; set; }

        public int? GameModeId { get; set; }
        public GameMode GameMode { get; set; }

        public int SoldCopies { get; set; }
    }
    public class GameMode
    {
        public int Id { get; set; }
        public string Mode { get; set; } 
    }

}
