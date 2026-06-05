using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ClassLibrary
{
    /**
     * @class Game
     * @brief Class qui permet d'instancier une partie
     */
    [DataContract (Name="Partie")]
    public class Game
    {
        [DataMember (Name ="Joueurs", Order = 0)]//!< Joueurs Liste des joueurs de la partie pour la persistance
        public List<Player> Players { get; set;  } = new();//!< Joueurs Liste des joueurs de la partie
        [DataMember(Name = "IAs", Order = 1)]//!< IAs Liste des IAs de la partie pour la persistance
        public List<IA> IAs { get; set; } = new();//!< IAs Liste des IAs de la partie

        [DataMember(Name = "Deck", Order = 2)]//!< Deck Deck de la partie pour la persistance
        public Deck Deck { get; set; } = new();//!< Deck Deck de la partie
        [DataMember(Name = "Tour_fini", Order = 3)]//!< Tour_fini Permet de savoir si la manche est fini ou non pour la persistance
        public readonly bool RoundFinished = false;//!< Tour_fini Permet de savoir si la manche est fini ou non
        // Pas besoin d'un DataMember ici car si la partie est finie, on ne la garde pas.
        public readonly bool GameOver = false;//!< Tour_fini Permet de savoir si la partie est fini ou non

        /**
         * @brief Déclaration de la méthode NewDeck qui permet de créer un nouveau deck pour la partie
         */
        public void NewDeck()
        {
            Deck = new Deck();
        }

        /**
         * @brief Déclaration de AddPlayer qui permet de créer et ajouter un joueur à la partie
         * @param nomJoueur Le nom du joueur qu'on veut ajouter
         */
        public void AddPlayer(string nomJoueur)
        {
            Player p;
            p = new Player(nomJoueur, Deck, 0, 0);

            if (Players.Count < 4)
            {
                Players.Add(p);
            }
        }

        /**
         * @brief Déclaration de AddIA qui permet de créer et ajouter une IA à la partie
         */
        public void AddIA()
        {
            IA ia;
            ia = new IA(0, Deck, 0);

            if (IAs.Count < 4)
            {
                IAs.Add(ia);
            }
        }

        /**
         * @brief Déclaration de RemovePlayer qui permet de supprimer un joueur de la partie
         * @param nomJoueur Le nom du joueur qu'on veut supprimer
         */
        public void RemovePlayer(string name)
        {
            int n = Players.Count;
            int id = -1;
            for (int i = 0; i < n; i++)
            {
                if (Players[i].Name == name)
                {
                    id = i; break;
                }
            }
            if (id >= 0) { Players.RemoveAt(id); }
        }

        /**
         * @brief Déclaration de RemoveIA qui permet de supprimer une IA à la partie
         */
        public void RemoveAllIA()
        {
            int n = IAs.Count;
            for (int i = 0; i < n; i++)
            {
                IAs.RemoveAt(0);
            }
        }
    }
}
