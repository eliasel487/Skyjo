using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;


namespace Stub;


public class Stub
{
    public Stub(string name = "stub")
        : base(/*name*/)
    {

        Game game1 = new();
        Deck deck = game1.GetDeck();

        game1.AddPlayer("Joueur 1");
        game1.AddPlayer("Joueur 2");

        Player p1 = game1.Players[0];
        Player p2 = game1.Players[1];

        // Player 1
        // Column 1
        p1.GetBoard().GetColumns()[0].GetCards()[0] = new Card(0);
        p1.GetBoard().GetColumns()[0].GetCards()[1] = new Card(1);
        p1.GetBoard().GetColumns()[0].GetCards()[2] = new Card(2);
        //Column 2
        p1.GetBoard().GetColumns()[1].GetCards()[0] = new Card(8);
        p1.GetBoard().GetColumns()[1].GetCards()[1] = new Card(2);
        p1.GetBoard().GetColumns()[1].GetCards()[2] = new Card(3);
        //Column 3
        p1.GetBoard().GetColumns()[2].GetCards()[0] = new Card(2);
        p1.GetBoard().GetColumns()[2].GetCards()[1] = new Card(3);
        p1.GetBoard().GetColumns()[2].GetCards()[2] = new Card(5);
        //Column 4
        p1.GetBoard().GetColumns()[3].GetCards()[0] = new Card(3);
        p1.GetBoard().GetColumns()[3].GetCards()[1] = new Card(4);
        p1.GetBoard().GetColumns()[3].GetCards()[2] = new Card(4);

        //Player 2
        //Column 1
        p2.GetBoard().GetColumns()[0].GetCards()[0] = new Card(12);
        p2.GetBoard().GetColumns()[0].GetCards()[1] = new Card(11);
        p2.GetBoard().GetColumns()[0].GetCards()[2] = new Card(10);
        //Column 2
        p2.GetBoard().GetColumns()[1].GetCards()[0] = new Card(11);
        p2.GetBoard().GetColumns()[1].GetCards()[1] = new Card(10);
        p2.GetBoard().GetColumns()[1].GetCards()[2] = new Card(9);
        //Column 3
        p2.GetBoard().GetColumns()[2].GetCards()[0] = new Card(10);
        p2.GetBoard().GetColumns()[2].GetCards()[1] = new Card(9);
        p2.GetBoard().GetColumns()[2].GetCards()[2] = new Card(8);
        //Column 4
        p2.GetBoard().GetColumns()[3].GetCards()[0] = new Card(9);
        p2.GetBoard().GetColumns()[3].GetCards()[1] = new Card(8);
        p2.GetBoard().GetColumns()[3].GetCards()[2] = new Card(7);

        //Mettre une carte au début de la partie
        game1.Deck.DiscardACard(new Card(5));

        //Retourner les deux cartes de départ
        p1.GetBoard().GetColumns()[0].GetCards()[0].FlipCard();
        p1.GetBoard().GetColumns()[0].GetCards()[2].FlipCard();

        p2.GetBoard().GetColumns()[0].GetCards()[2].FlipCard();
        p2.GetBoard().GetColumns()[2].GetCards()[2].FlipCard();

        AfficherLesPlateaux(game1.Players, game1.Deck);

        // Tour 1
        Console.WriteLine("Tour 1 : ");

        p1.ActionCard(game1.GetDeck(), new Card(0), 0, 1, 0);

        p2.ActionCard(game1.GetDeck(), new Card(10), 0, 0, 0);

        p1.CountRoundScore();
        p2.CountRoundScore();

        AfficherLesPlateaux(game1.Players, game1.Deck);

        //Tour 2
        Console.WriteLine("Tour 2 : ");

        p1.ActionCard(game1.GetDeck(), new Card(12), 1, 0, 1);

        p2.ActionCard(game1.GetDeck(), new Card(-2), 0, 0, 1);

        p1.CountRoundScore();
        p2.CountRoundScore();

        AfficherLesPlateaux(game1.Players, game1.Deck);

        //Tour 3
        Console.WriteLine("Tour 3 : ");

        p1.ActionCard(game1.GetDeck(), new Card(10), 1, 2, 2);

        p2.ActionCard(game1.GetDeck(), new Card(0), 0, 2, 3);

        p1.CountRoundScore();
        p2.CountRoundScore();

        AfficherLesPlateaux(game1.Players, game1.Deck);

        //Tour 4
        Console.WriteLine("Tour 4 : ");

        p1.ActionCard(game1.GetDeck(), new Card(10), 0, 0, 2);

        p2.ActionCard(game1.GetDeck(), new Card(11), 0, 1, 1);

        p1.CountRoundScore();
        p2.CountRoundScore();

        AfficherLesPlateaux(game1.Players, game1.Deck);

        //Tour 5
        Console.WriteLine("Tour 5 : ");

        p1.ActionCard(game1.GetDeck(), new Card(12), 0, 1, 1);

        p2.ActionCard(game1.GetDeck(), new Card(11), 0, 1, 0);

        p1.CountRoundScore();
        p2.CountRoundScore();

        AfficherLesPlateaux(game1.Players, game1.Deck);

        //Tour 6
        Console.WriteLine("Tour 6 : ");

        p1.ActionCard(game1.GetDeck(), new Card(12), 0, 0, 1);

        p2.ActionCard(game1.GetDeck(), new Card(7), 0, 2, 1);

        p1.CountRoundScore();
        p2.CountRoundScore();

        AfficherLesPlateaux(game1.Players, game1.Deck);

        //Tour 7
        Console.WriteLine("Tour 7 : ");

        p1.ActionCard(game1.GetDeck(), new Card(12), 0, 2, 1);

        p2.ActionCard(game1.GetDeck(), new Card(5), 0, 1, 2);

        p1.CountRoundScore();
        p2.CountRoundScore();

        AfficherLesPlateaux(game1.Players, game1.Deck);

        //Tour 8
        Console.WriteLine("Tour 8 : ");

        p1.ActionCard(game1.GetDeck(), new Card(-2), 0, 1, 2);

        p2.ActionCard(game1.GetDeck(), new Card(12), 0, 0, 3);

        p1.RemoveColumn();
        p1.CountRoundScore();
        p2.CountRoundScore();

        AfficherLesPlateaux(game1.Players, game1.Deck);

        //Tour 9
        Console.WriteLine("Tour 9 : ");

        p1.ActionCard(game1.GetDeck(), new Card(12), 0, 2, 3);

        p2.ActionCard(game1.GetDeck(), new Card(11), 0, 0, 1);

        p1.CountRoundScore();
        p2.CountRoundScore();

        AfficherLesPlateaux(game1.Players, game1.Deck);

        //Tour 10
        Console.WriteLine("Tour 10 : ");

        p1.ActionCard(game1.GetDeck(), new Card(0), 0, 2, 0);

        p2.ActionCard(game1.GetDeck(), new Card(12), 0, 1, 3);

        p1.CountRoundScore();
        p2.CountRoundScore();

        AfficherLesPlateaux(game1.Players, game1.Deck);

        //Tour 11
        Console.WriteLine("Tour 11 : ");

        p1.ActionCard(game1.GetDeck(), new Card(-2), 0, 2, 3);

        p2.ActionCard(game1.GetDeck(), new Card(3), 0, 0, 2);

        p1.RemoveColumn();
        p1.CountRoundScore();
        p2.CountRoundScore();

        AfficherLesPlateaux(game1.Players, game1.Deck);

        //Tour 11
        Console.WriteLine("Tour 11 : ");

        p1.ActionCard(game1.GetDeck(), new Card(-2), 0, 0, 2);

        p1.CountRoundScore();
        p2.CountRoundScore();

        AfficherLesPlateaux(game1.Players, game1.Deck);

        //Fin de la mache et de lapartie vu qu'il y a un joueur qui a plus de 100 points

        Console.WriteLine("-----------------------\nFin de la manche : \n");
        p1.CountGameScore();
        p2.CountGameScore();
        Console.WriteLine("Joueur 1 : " + p1._gameScore + "\nJoueur 2 : " + p2._gameScore);

        Console.WriteLine("Joueur 1 a gagné");
    }

















    internal void AfficherLesPlateaux(List<Player> p, Deck deck) // affiche le plateau de chaque joueur
    {
        int j, k;
        Board b;
        List<Column> columns;
        List<Card> cards;
        for (int i = 0; i < p.Count; i++)
        {
            b = p[i].GetBoard();
            columns = b.GetColumns();
            Console.WriteLine(p[i]._name + " (Game Score = " + p[i]._gameScore + ", Round Score = " + p[i]._roundScore + ") :");
            for (k = 0; k < 3; k++)
            {
                Console.WriteLine("---------------------");
                Console.Write("|");
                for (j = 0; j < 4; j++)
                {
                    cards = columns[j].GetCards();
                    if (!columns[j].GetValued()) Console.Write(" // |");
                    else if (!cards[k].IsFlipped()) Console.Write(" XX |");
                    else
                        if (cards[k].GetNumber() < 0 || cards[k].GetNumber() >= 10)
                        {
                            Console.Write(" " + cards[k].GetNumber() + " |");
                        }
                        else Console.Write(" 0" + cards[k].GetNumber() + " |");
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------------\n");
        }
        Console.WriteLine(deck.GetFirstCardDiscard() + "\n");
    }

}
