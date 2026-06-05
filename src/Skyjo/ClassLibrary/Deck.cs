using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ClassLibrary
{
    /**
     * @class Deck
     * @brief Classe qui permet de stocker les cartes du jeu dans deux piles : la pioche qui permet de fournir des cartes et la déffausse où on met les cartes dont on a plus besoin
     */
    [DataContract]
    public class Deck
    {
        [DataMember (Name ="Pioche")]//!< Pioche La liste qui fournit les cartes pour la ersistance
        readonly List<Card>  _pull = new();//!< _pull La liste qui fournit les cartes
        [DataMember (Name ="Defausse")]//!< Deffausse La liste qui permet de stocker les cartes dont on a plus besoin pour la persistance
        readonly List<Card> _discard = new();//!< _discard L aliste qui permet de stocker les cartes dont on a plus besoin

        /**
         * @brief Déclaration du constructeur de la class Deck
         */
        public Deck()
        {
            for (int i = 10; i < 161; i ++)
            {
                if (i < 131) _pull.Add(new Card(i / 10));
                else if (i < 141) _pull.Add(new Card(-1));
                else if (i < 146) _pull.Add(new Card(-2));
                else _pull.Add(new Card(0));
            }
        }

        /**
         * @brief Accesseur de la taille de l'attribut _pull
         * @return _pull.Count La taille de la pioche
         */
        public int NbCardPull() { return _pull.Count; }

        /**
         * @brief Accesseur de la taille de l'attribut _discard
         * @return _pull.Count La taille de la déffausse
         */
        public int NbCardDiscard() { return _discard.Count; }

        /**
         * @brief Accesseur de la première de la pioche
         * @return _discard.Last().Number.ToString() La première carte de la déffausse
         */
        public string GetFirstCardDiscard()
        {
            if (!IsEmptyDiscard())
                return "La défausse contient : " + _discard.Last().Number.ToString();
            else return "La défausse est vide";
        }

        /**
         * @brief Déclaration de la méthode IsEmptyPull qui permet de savoir si la pioche est vide ou non
         * @return _pull.Count == 0 Permet de savoir si la pioche est vide ou non
         */
        public bool IsEmptyPull()
        {
            return _pull.Count == 0;
        }

        /**
         * @brief Déclaration de la méthode IsEmptyDiscard qui permet de savoir si la défausse est vide ou non
         * @return _pull.Count == 0 Permet de savoir si la défausse est vide ou non
         */
        public bool IsEmptyDiscard()
        {
            return _discard.Count == 0;
        }

        /**
         * @brief Déclaration de la méthode DiscardACard qui permet de mettre une cartes dans la déffausse
         */
        public void DiscardACard(Card card)
        {
            _discard.Add(card);
        }

        /**
         * @brief Déclaration de la méthode PickACard qui permet de tirer une carte
         * @param nb Détermine si on tire la carte dans la pioche (0) ou dans la déffausse (1)
         * @retrun res La carte tirée
         */
        public Card PickACard(int nb)
        {
            Random rnd = new Random();
            int n;
            Card res;
            if (nb == 0)
            {
                if (_pull.Count == 0)
                {
                    // Règle Skyjo : on retourne la défausse (sauf la carte du dessus) pour reformer la pioche
                    Card topDiscard = _discard.Last();
                    _discard.RemoveAt(_discard.Count - 1);
                    _pull.AddRange(_discard);
                    _discard.Clear();
                    _discard.Add(topDiscard);
                }
                n = rnd.Next(_pull.Count);
                res = _pull[n];
                _pull.RemoveAt(n);
                return res;
            }
            res = _discard.Last();
            _discard.RemoveAt(_discard.Count - 1);
            return res;
        }
    }
}
