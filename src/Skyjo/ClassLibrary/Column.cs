using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ClassLibrary
{
    /**
     * @class Column
     * @brief Déclaration de la classe Column
     */
    [DataContract]
    public class Column
    {
        [DataMember(Name = "Cartes")]//!< Cartes La liste des cartes pour la persistance
        private readonly List<Card> _cards = new();//!< _cards La liste des cartes
        [DataMember(Name ="Colonne_valuee")]//!< Colonne_valuee Etat de la colonne (toujours en jeu ou non) pour la persistance
        bool _valued = true;//!< _valued Etat de la colonne (toujours en jeu ou non)

        /**
         * @brief Déclaration du Constructeur de la class Column
         * @param card1 La première carte de la colonne
         * @param card2 La deuxième carte de la colonne
         * @param card3 La troisièe et dernière ccarte de la colonne
         */
        public Column(Card card1, Card card2, Card card3)
        {
            _cards.Add(card1);
            _cards.Add(card2);
            _cards.Add(card3);
        }

        /**
         * @brief Accesseur de l'attribut _valued
         * @return _valued L'état de la colonne
         */
        public bool GetValued()
        {
            return _valued;
        }

        /**
         * @brief Accesseur de l'attribut _cards
         * @return _cards La liste de carte
         */
        public List<Card> GetCards()
        {
            List<Card> column = new List<Card>();
            if (_cards[0].Number == -3 || _cards[1].Number == -3 || _cards[2].Number == -3)
            {
                return column; // renvoie une liste vide
            }
            return _cards;
        }

        /**
         * @brief Déclaration de la méthode Remove qui permet d'enlever une colonne du jeu
         */
        public void Remove()
        {
            if (IsRemovable())
            {
                _valued = false;
            }
        }

        /**
         * @brief Déclaration de la méthode IsRemovable
         * @return true/false La valeur qui permet de déteriner si une colonne peut être enlevée
         */
        public bool IsRemovable()
        {
            if (_cards[0].IsFlipped() && _cards[1].IsFlipped() && _cards[2].IsFlipped())
                return _cards[0].Number == _cards[1].Number && _cards[1].Number == _cards[2].Number;
            return false;
        }
    }
}
