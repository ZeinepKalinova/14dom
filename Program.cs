using System;
using System.Collections.Generic;
using System.Linq;

class Karta
{
    public string Mast { get; set; }
    public string Tip { get; set; }

    public Karta(string mast, string tip)
    {
        Mast = mast;
        Tip = tip;
    }
}

class Player
{
    public List<Karta> Cards { get; set; }

    public Player()
    {
        Cards = new List<Karta>();
    }

    public void DisplayCards()
    {
        foreach (var karta in Cards)
        {
            Console.WriteLine($"{karta.Tip} {karta.Mast}");
        }
    }
}

class Game
{
    private List<Player> players;
    private List<Karta> deck;

    public Game(int playerCount)
    {
        players = new List<Player>();
        deck = GenerateDeck();

        for (int i = 0; i < playerCount; i++)
        {
            players.Add(new Player());
        }

        ShuffleDeck();
        DealCards();
    }

    private List<Karta> GenerateDeck()
    {
        var suits = new string[] { "Черви", "Бубны", "Трефы", "Пики" };
        var tips = new string[] { "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };

        var deck = new List<Karta>();

        foreach (var suit in suits)
        {
            foreach (var tip in tips)
            {
                deck.Add(new Karta(suit, tip));
            }
        }

        return deck;
    }

    private void ShuffleDeck()
    {
        Random random = new Random();
        deck = deck.OrderBy(card => random.Next()).ToList();
    }

    private void DealCards()
    {
        int playerIndex = 0;
        foreach (var card in deck)
        {
            players[playerIndex].Cards.Add(card);
            playerIndex = (playerIndex + 1) % players.Count;
        }
    }

    public void PlayGame()
    {
        while (players.All(player => player.Cards.Count > 0))
        {
            List<Karta> playedCards = players.Select(player => player.Cards[0]).ToList();
            int maxCardIndex = playedCards.IndexOf(playedCards.Max());

            Console.WriteLine($"Игрок {maxCardIndex + 1} забирает карты:");
            foreach (var card in playedCards)
            {
                players[maxCardIndex].Cards.Add(card);
                Console.WriteLine($"{card.Tip} {card.Mast}");
            }

            foreach (var player in players)
            {
                player.Cards.RemoveAt(0);
            }

            Console.WriteLine();
        }

        int winnerIndex = players.FindIndex(player => player.Cards.Count == 36);
        Console.WriteLine($"Игрок {winnerIndex + 1} победил!");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Программа \"Карточная игра!\"\n");

        // Создаем игру с двумя игроками
        Game game = new Game(2);

        // Играем
        game.PlayGame();
    }
}
