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

        public enum Face
        {
            Number, Jack, Queen, King
        }

        public enum Decision
        {
            Up, BlackJack, Down
        }
    }
}