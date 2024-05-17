using System;

public class MemoryGame
{
    private GameBoard gameBoard;
    private int lives;
    private int score;
    private int level;

    public MemoryGame(int initialCards)
    {
        gameBoard = new GameBoard(initialCards);
        lives = 5;
        score = 0;
        level = 1;
    }

    public void StartGame()
    {
        Console.Title = "MemoryGame";
        Console.Clear();
        DisplayHeader();

        while (!IsGameOver())
        {
            gameBoard.Initialize(level);
            DisplayHeader();
            gameBoard.Display();

            while (!gameBoard.AllCardsMatched() && lives > 0)
            {
                TakeTurn();
                DisplayHeader();
                gameBoard.Display();
            }

            if (lives > 0)
            {
                level++;
                if (level > 2)
                {
                    Console.WriteLine("You completed all levels!");
                    break;
                }
                else
                {
                    Console.WriteLine("You passed to the next level!");
                }
            }
        }

        Console.WriteLine("Game over!");
    }

    private void DisplayHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("********************");
        Console.WriteLine("*     MemoryGame    *");
        Console.WriteLine("********************");
        Console.WriteLine($"Lives: {lives} | Score: {score} | Level: {level}");
        Console.WriteLine();
        Console.ResetColor();
    }

    private void TakeTurn()
    {
        Console.WriteLine("Select two cards to flip (e.g., A1 B2):");
        string input = Console.ReadLine().ToUpper();
        string[] inputs = input.Split(' ');

        if (inputs.Length != 2)
        {
            Console.WriteLine("Invalid input. You must select exactly two cards.");
            return;
        }

        char[] coordinates1 = inputs[0].ToCharArray();
        char[] coordinates2 = inputs[1].ToCharArray();

        int row1 = coordinates1[0] - 'A';
        int column1 = coordinates1[1] - '1';
        int row2 = coordinates2[0] - 'A';
        int column2 = coordinates2[1] - '1';

        if (row1 < 0 || row1 >= gameBoard.Rows || column1 < 0 || column1 >= gameBoard.Columns ||
            row2 < 0 || row2 >= gameBoard.Rows || column2 < 0 || column2 >= gameBoard.Columns)
        {
            Console.WriteLine("Invalid coordinates. Try again.");
            return;
        }

        gameBoard.FlipCard(row1, column1);
        gameBoard.Display();
        gameBoard.FlipCard(row2, column2);
        gameBoard.Display();

        if (gameBoard.CheckMatch())
        {
            Console.WriteLine("Match found!");
            score += 10;
        }
        else
        {
            Console.WriteLine("No match. You lose a life.");
            lives--;
            score -= 5;
            gameBoard.ResetFlippedCards();
        }

        DisplayHeader();
    }

    private bool IsGameOver()
    {
        return lives <= 0 || level > 2;
    }
}
