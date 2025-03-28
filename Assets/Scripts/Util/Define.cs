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
        Jack, Queen, King, Joker
    }

    public enum Guess
    {
        Up, Spot, Down
    }

    public enum GamePhase
    {
        Opening, Start, Play, Shooting, End
    }
}