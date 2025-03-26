namespace Bmc
{
    public class Define
    {
        public enum PlayState
        {
            None,
            Draw,
            Guess,
            Death
        }

        public enum CardType
        {
            Ace = 0,
            Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten,
            Jack, Queen, King
        }

        public enum Decision
        {
            Up, BlackJack, Down
        }

        public enum GamePhase
        {
            Start, Play, End
        }
    }
}