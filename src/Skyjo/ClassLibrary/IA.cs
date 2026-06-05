using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ClassLibrary
{
    /**
     * @class IA
     * @brief Classe qui permet d'instacier un joueur qui joue tout seul
     */
    [DataContract]
    public class IA : Player // le but de cette IA, c'est qu'elle ne soit pas trop dur donc elle vas laisser le joueur finir en premier la manche
    {
        /**
         * @brief Déclaration du constructeur de la classe IA
         */
        public IA(int GameScore, Deck deck, int RoundScore)
            : base(GameScore, deck, RoundScore)
        {
        }

        /**
         * @brief Déclaration de la méthode PlayRound qui permet à l'IA de jouer un tour
         * @param d Le deck qui permettera à l'IA de déffausser un carte
         */
        public void PlayRound(Deck d) //fait jouer un tour complet a l'IA
        {
            int choosePick = 0, x = 0, y = 0;
            bool replaced = false;

            Card c = PickACard(d, choosePick);
            c.FlipCard();
            if (c.Number > 7) // si la carte est supérieur à 7 il va chercher à retourne une carte à la place, sinon il pose la carte
            {
                while (y < 4 && replaced == false) // cette boucle vas parcourir tout le tableau pour voir si les cartes sont retournés, sinon il retourne la carte
                {
                    List<Column> columns = Board.GetColumns();
                    List<Card> cards = columns[y].GetCards();
                    if (!cards[x].IsFlipped())
                    {
                        d.DiscardACard(c);
                        Board.ReturnACard(y, x);
                        replaced = true;
                    }
                    x++;
                    if (x >= 3) { y++; x = 0; }
                }
                if (!replaced) PlaceCardAuto(d, c);
            }
            else PlaceCardAuto(d, c);
        }

        /**
         * @brief Déclaration de la méthode PlaceCardAuto qui permet à l'IA de placer un carte automatiquement
         * @param deck Deck dans lequel l'IA pourrat déffausser un carte
         * @param c La carte à placer
         */
        public void PlaceCardAuto(Deck deck, Card c)
        {
            c.FlipCard();
            bool place = false;
            int x = 0, y = 0;
            while (place == false && y < 4) // cette boucle vas parcourir tout le tableau pour voir si il peut poser une carte dont le nombre est inférieur à celle retourner sur le plateau
                                    // sinon il la pose en 0 0, car il faut bien qu'il fasse quelque chose à un moment
            {
                List<Column> columns = Board.GetColumns();
                List<Card> cards = columns[y].GetCards();
                if (cards[x].IsFlipped() && cards[x].Number < c.Number)
                {
                    Board.ReplaceCard(deck, y, x, c);
                    place = true;
                }
                x++;
                if (x >= 3) { y++; x = 0; }
            }
            if (!place) Board.ReplaceCard(deck, 0, 0, c);
        }
    }
}
