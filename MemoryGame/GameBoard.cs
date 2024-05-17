using System;
using System.Collections.Generic;

public class GameBoard
{
    private Card[,] grid;
    private List<Card> flippedCards;

    public int Rows { get; private set; }
    public int Columns { get; private set; }

    public GameBoard(int initialCards)
    {
        Rows = 2;
        Columns = initialCards / 2;
        grid = new Card[Rows, Columns];
        flippedCards = new List<Card>();
    }

    public void Initialize(int level)
    {
        int totalCards = level == 1 ? 8 : 12;
        Rows = level == 1 ? 2 : 3;
        Columns = totalCards / Rows;
        grid = new Card[Rows, Columns];
        flippedCards = new List<Card>();

        List<int> values = GenerateCardValues();
        Shuffle(values);
        Deal(values);
    }

    public void Display()
    {
        Console.Write("  ");
        for (int i = 0; i < Columns; i++)
        {
            Console.Write($" {i + 1}");
        }
        Console.WriteLine();

        for (int i = 0; i < Rows; i++)
        {
            Console.Write((char)('A' + i) + " ");

            for (int j = 0; j < Columns; j++)
            {
                if (grid[i, j].IsFaceUp)
                {
                    if (grid[i, j].IsMatched)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write($"{grid[i, j].Value,2} ");
                    Console.ResetColor();
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("  ");
                }
            }
            Console.WriteLine();
        }
    }

    public void FlipCard(int row, int column)
    {
        if (row < 0 || row >= Rows || column < 0 || column >= Columns)
        {
            Console.WriteLine("Invalid coordinates.");
            return;
        }

        grid[row, column].Flip();
        flippedCards.Add(grid[row, column]);
    }

    public bool CheckMatch()
    {
        if (flippedCards.Count != 2)
        {
            return false;
        }

        if (flippedCards[0].Value == flippedCards[1].Value)
        {
            flippedCards[0].SetMatched();
            flippedCards[1].SetMatched();
            flippedCards.Clear();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetFlippedCards()
    {
        foreach (var card in flippedCards)
        {
            card.Flip();
        }
        flippedCards.Clear();
    }

    public bool AllCardsMatched()
    {
        foreach (var card in grid)
        {
            if (!card.IsFaceUp || !card.IsMatched)
            {
                return false;
            }
        }
        return true;
    }

    public void Shuffle(List<int> values)
    {
        Random rand = new Random();
        int n = values.Count;

        while (n > 1)
        {
            n--;
            int k = rand.Next(n + 1);
            int temp = values[k];
            values[k] = values[n];
            values[n] = temp;
        }
    }

    private List<int> GenerateCardValues()
    {
        int totalCards = Rows * Columns;
        List<int> values = new List<int>();

        for (int i = 1; i <= totalCards / 2; i++)
        {
            values.Add(i);
            values.Add(i);
        }

        return values;
    }

    private void Deal(List<int> values)
    {
        int index = 0;

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                grid[i, j] = new Card(values[index++]);
            }
        }
    }
}
