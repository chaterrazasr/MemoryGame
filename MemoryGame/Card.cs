public class Card
{
    public int Value { get; private set; }
    public bool IsFaceUp { get; private set; }
    public bool IsMatched { get; private set; }

    public Card(int value)
    {
        Value = value;
        IsFaceUp = false;
        IsMatched = false;
    }

    public void Flip()
    {
        IsFaceUp = !IsFaceUp;
    }

    public void SetMatched()
    {
        IsMatched = true;
        IsFaceUp = true;
    }
}
