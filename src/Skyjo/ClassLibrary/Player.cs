using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace ClassLibrary
{
    /**
     * @class Player
     * @brief Classe qui permet d'instancier un joueur
     */
    [DataContract]
    public class Player
    {
        [DataMember (Name="Nom_joueur")]//!< Nom_joueur Le nom du joueur pour la persictance
        public string Name { get; set; }//!< Name Le nom du joueur

        [DataMember (Name="Score_total_joueur")]//!< Score_total_joueur Le score total du joueur pour la persictance
        public int GameScore { get; set; }//!< GameScore Le score total du joueur

        [DataMember (Name="Score_manche_actuelle")]//!< Score_manch_actuelle Le score de la manche pour le joueur pour la persictance
        public int RoundScore { get; set; }//!< RoundScore Le score de la manche pour le joueur

        private readonly bool _IA;//!< _IA L'etat du joueur (IA ou non)
        [DataMember (Name="Plateau_carte_joueur")]//!< Plateau_carte_joueur Le plateau du joueur pour la persictance
        public Board Board { get; set; }//!< Board Le plateau du joueur

        /**
         * @brief Déclaration du constructeur de la classe Player
         * @param name Le nom du joueur
         * @param deck Le deck qui permet de fournir les cartes 
         * @param GameScore Le score total du joueur
         * @param RoundScore le score de la manche du joueur
         */
        public Player(string name, Deck deck, int GameScore, int RoundScore)
        {
            this.Name = name;
            this.GameScore = GameScore;
            this.RoundScore = RoundScore;
            this._IA = false;
            Board = new Board(deck);

        }

        /**
         * @brief Déclaration du constructeur de la classe Player
         * @param deck Le deck qui permet de fournir les cartes 
         * @param GameScore Le score total du joueur
         * @param RoundScore le score de la manche du joueur
         */
        public Player(int GameScore, Deck deck, int RoundScore)
        {
            this.Name = "IA";
            this.GameScore = GameScore;
            this.RoundScore = RoundScore;
            this._IA = true;
            Board = new Board(deck);
        }

        /**
         * @brief Accesseur de l'attribut IA
         * @return _IA L'etat du joueur (IA ou non)
         */
        public bool GetIA()
        {
            return _IA;
        }

        /**
         * @brief Déclaration de la méthode NewBoard qui permet de recréer un plateau
         * @param d DEck qui fournit les cartes
         */
        public void NewBoard(Deck d)
        {
            Board = new Board(d);
            this.CountGameScore();
        }

        /**
         * @brief Déclaration de la méthode PickACard qui permet de tirer une carte
         * @param d Le deck dans lequel on tire la carte
         * @param choose Le choix de la pioche (0) et de l adéffausse (1)
         * @return d.PickACard(choose) La carte tirée dasn le deck
         */
        public static Card PickACard(Deck d, int choose)
        {
            return d.PickACard(choose);
        }

        /**
         * @brief Déclaration de la méthode RoundOver qui permet de détérminer si la manche est terminée
         * @return Board.IsAllReturned() Le booléen qui permet de détérminer si la manche est terminée
         */
        public bool RoundOver()
        {
            return Board.IsAllReturned();
        }

        /**
         * @brief Déclaration de la méthode Play qui permet de jouer un tour en tant que joueur
         * @param d Le deck qui fournit les cartes
         */
        public void Play(Deck d) //fonction utiliser pour les vues, elle sera peut être inutile
        {
            int choosePick = 0, chooseCard = 0, x = 0, y = 0;
            Card c = PickACard(d, choosePick);
            c.FlipCard();
            ActionCard(d, c, chooseCard, x, y);
        }

        /**
         * @brief Déclaration de la méthode ActionCard qui êrmet d'interagir avec une carte de son plateau
         * @param deck Le deck qui permet de déffausser la carte
         * @param c La carte qu'on va utiliser pour en remplacer une autre sur le plateau
         * @param choose Le choix de remplacer un carte (0) ou de retourner une carte (1)
         * @param x La coordonnée x de la carte sur le plateau avec laquelle on veut interagir
         * @param y La coordonnée y de la carte sur le plateau avec laquelle on veut interagir
         */
        public void ActionCard(Deck deck, Card c, int choose, int x, int y) // par rapport à ce que le joueur a choisi la carte sera soit placer soit on retourne une carte sur la plateau
        {
            if (Board.GetColumns()[y].GetValued())
            {
                c.FlipCard();
                if (choose == 0)
                {
                    Board.ReplaceCard(deck, y, x, c);
                }
                else
                {
                    Board.ReturnACard(y, x); //Board.ReturnACard(x, y) == false
                }
            }
        }

        /**
         * @brief Déclaration de la Méthode RemoveColiumn qui permet d'enlever les colonnes qui sont enlevable
         */
        public void RemoveColumn()
        {
            for (int i = 0; i < 4; i++)
            {
                Board.GetColumns()[i].Remove();
            }
        }

        /**
         * @brief Déclaration de la méthode CountGameScore qui permet de mettre à jour le score total
         */
        public void CountRoundScore()
        {
            Card card;
            Column col;
            RoundScore = 0;
            for (int i = 0; i < 4; i++)
            {
                col = Board.GetColumns()[i];
                for (int j = 0; j < 3; j++)
                {
                    card = col.GetCards()[j];
                    if (card.IsFlipped() && col.GetValued())
                    {
                        RoundScore += card.Number;
                    }
                }
            }
        }

        /**
         * @brief Déclaration de la méthode CountRoundScore qui permet de mettre à jour le score de la manche
         */
        public void CountGameScore()
        {
            GameScore += RoundScore;
            if (GameScore < 0) GameScore = 0;
        }

        /**
         * @brief Déclaration de FlipToCard qui permet de retourner deux cartes aléatoire sur le plateau pour le début de partie
         * @return res La somme de ces deux carets pour pouvoir détérminer le premier joueur
         */
        public int FlipTwoCard()
        {
            Random rnd = new Random();
            int x1, y1, x2, y2, res = 0;
            x1 = rnd.Next(0, 3);
            x2 = rnd.Next(0, 3);
            y1 = rnd.Next(0, 2);
            y2 = rnd.Next(0, 2);
            while (x1 == x2 && y1 == y2)
            {
                y2 = rnd.Next(0, 2);
                x2 = rnd.Next(0, 3);
            }
            Board.GetColumns()[x1].GetCards()[y1].FlipCard();
            Board.GetColumns()[x2].GetCards()[y2].FlipCard();
            res += Board.GetColumns()[x1].GetCards()[y1].Number;
            res += Board.GetColumns()[x2].GetCards()[y2].Number;
            return res;
        }
    }
}
