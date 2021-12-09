namespace Engine.Models
{
    public class GameState
    {
        public Player Player { get; init; }
        public int XCoordinate { get; init; }
        public int YCoordinate { get; init; }

        public GameState(Player player, int xCoordinate, int yCoordinate)
        {
            Player = player;
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
        }
    }
}