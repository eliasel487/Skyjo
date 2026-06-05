using ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Text;

namespace ClassLibrary
{
    /**
     * @class Board
     * @brief Déclaration de la classe Board qui permet d'implementer un plateau de joueur de notre jeu
     */
    [DataContract]
    public class Board
    {
        [DataMember (Name ="Colonnes")]//!< Colonnes est la liste des colonnes de notre plateau pour la persistance
        private readonly List<Column> _columns = new(); //!< _columns est la liste des colonnes de notre plateau

        /**
         * @brief Déclaration du constructeur de la classe Board
         * @param deck La source de carte pour créer le plateau
         */
        public Board(Deck deck)
        {
            for (int i = 0; i < 4; i++)
            {
                _columns.Add(new Column(deck.PickACard(0), deck.PickACard(0), deck.PickACard(0)));
            }
        }

        /**
         * @brief Déclaration de la méthode pour remplacer une carte sur le plateau
         * @param deck La source de carte
         * @param y La coordonnée y de la carte qu'on veut remplacer
         * @param x La coordonnée x de la carte qu'on veut remplacer
         * @param t La carte qu'on met à la place de celle qu'on remplace
         */
        public void ReplaceCard(Deck deck, int y, int x, Card t)
        {
            if (y >= 0 && y < _columns.Count)
            {
                List<Card> cards = _columns[y].GetCards();
                if (x >= 0 && x < cards.Count)
                {
                    deck.DiscardACard(cards[x]);
                    cards[x] = t;
                }
            }
        }

        /**
         * @brief Déclaration de la méthode qui permet de verifier si toutes les cartes sont retournées ou pas
         * @return true/fasle La valeur binaire qui determine si toutes les cartes sont retournées ou non
         */
        public bool IsAllReturned()
        {
            foreach (Column c in _columns)
            {
                foreach (Card card in c.GetCards())
                {
                    if (!card.IsFlipped())
                        return false;
                }
            }
            return true;
        }

        /**
         * @brief Accesseur de l'attribut _columns
         * @return _columns La liste de colonnes du plateau
         */
        public List<Column> GetColumns()
        {
            return _columns;
        }

        /**
         * @brief Declaration de la méthode qui permet de retourner une carte sur le plateau
         * @return true/false La valeur binaire qui determine si l'opération c'est bien effectué
         */
        public bool ReturnACard(int y, int x)
        {
            List<Card> cards = _columns[y].GetCards();
            if (x >= 0 && x < cards.Count)
            {
                cards[x].FlipCard();
                return true;
            }
            return false;
        }
    }
}


