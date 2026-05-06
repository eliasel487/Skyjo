using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class IA : Player // le but de cette IA, c'est qu'elle ne soit pas trop dur donc elle vas laisser le joueur finir en premier la manche
    {
        Board _board;
        public IA(int GameScore, Deck deck, int RoundScore) 
            : base(GameScore, deck, RoundScore)
        {
            _board = new Board(deck);
        }

        public void PlayRound(Deck d) //fait jouer un tour complet a l'IA
        {
            int choosePick = 0, x = 0, y = 0;

            Card c = PickACard(d, choosePick);
            c.FlipCard();
            if (c.GetNumber() > 7) // si la carte est supérieur à 7 il vas chercher à retourne une carte à la place, sinon il pose la carte
            {
                for (y = 0; y < 4; y++) // cette boucle vas parcourir tout le tableau pour voir si les cartes sont retournés, sinon il retourne la carte
                {
                    List<Column> columns = _board.GetColumns();
                    for (x = 0; x < 3; x++)
                    {
                        List<Card> cards = columns[y].GetCards();
                        if (cards[x].IsFlipped())
                        {
                            x++;
                        }
                        else
                            d.DiscardACard(c);
                            _board.ReturnACard(y, x);
                    }
                    y++;
                }
                PlaceCardAuto(c);
            }
            PlaceCardAuto(c);



        }

        public void PlaceCardAuto(Card c)
        {
            int x = 0, y = 0;
            for (y = 0; y < 4; y++) // cette boucle vas parcourir tout le tableau pour voir si il peut poser une carte dont le nombre est inférieur à celle retourner sur le plateau
                                    // sinon il la pose en 0 0, car il faut bien qu'il fasse quelque chose à un moment
            {
                List<Column> columns = _board.GetColumns();
                for (x = 0; x < 3; x++)
                {
                    List<Card> cards = columns[y].GetCards();
                    if (cards[x].IsFlipped() && cards[x].GetNumber() > c.GetNumber())
                    {
                        x++;
                    }
                    else
                        _board.ReplaceCard(y, x, c);
                }
                y++;
            }
            _board.ReplaceCard(0, 0, c);
        }
    }
}
