using System;

class Program
{
    static void Main(string[] args)
    {
        // Initialize and start the game
        MemoryGame game = new MemoryGame(8); // Start with 8 cards (4 pairs) for level 1
        game.StartGame();
    }
}
