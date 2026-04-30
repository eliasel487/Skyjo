using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Game
    {
        public List<Player> Players { get; } = new();
        public Deck Deck { get; } = new();
        bool RoundFinished = false;
        bool GameOver = false;

        public bool GetRoundFinished()
        {
            return RoundFinished;
        }

        public bool GetGameOver()
        {
            return GameOver;
        }

        public void AddPlayer(string nomJoueur)
        {
            Player p;
            p = new Player(nomJoueur, Deck, 0, 0);

            if (Players.Count <= 5)
            {
                Players.Add(p);
            }
        }

        public void RemovePlayer(string name)
        {
            foreach (Player player in Players)
            {
                if (player._name == name)
                {
                    Players.Remove(player);
                }
            }
        }
        public void PlayGame()
        {
            int cpt, temp, nbJoueur = Players.Count;

            

            while (!GameOver)
            {

                for(int i = 0; i < nbJoueur; i++) // créer un nouveau plateau au début de chaque tour
                {
                    Players[i].NewBoard(Deck);
                }

                RoundFinished = false;
                for (int i = 0; i < Players.Count(); i++)
                {
                    if (Players[i]._gameScore >= 100) GameOver = true;
                }

                if (GameOver) break;
                
                cpt = 0;
                while (!RoundFinished)
                {
                    if (cpt == Players.Count()) cpt = 0;
                    if (Players[cpt].RoundOver()) 
                    {
                        RoundFinished = true; 
                        break; 
                    }
                    Players[cpt].Play(Deck);
                    cpt++;
                }
                temp = cpt;
                cpt++;
                while (cpt != temp)
                {
                    if (cpt == Players.Count()) cpt = 0;
                    Players[cpt].Play(Deck);
                    cpt++;
                }
            }
        }

    }
}
