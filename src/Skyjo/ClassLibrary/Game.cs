using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Game
    {
        public List<Player> Players { get; } = new();
        public List<IA> IAs { get; } = new();
        Deck _deck = new();
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

        public Deck GetDeck()
        {
            return _deck;
        }

        public void NewDeck()
        {
            _deck = new Deck();
        }

        public void AddPlayer(string nomJoueur)
        {
            Player p;
            p = new Player(nomJoueur, _deck, 0, 0);

            if (Players.Count < 4)
            {
                Players.Add(p);
            }
        }

        public void AddIA()
        {
            IA ia;
            ia = new IA(0, _deck, 0);

            if (IAs.Count < 4)
            {
                IAs.Add(ia);
            }
        }

        public void RemovePlayer(string name)
        {
            int n = Players.Count;
            int id = -1;
            for (int i = 0; i < n; i++)
            {
                if (Players[i]._name == name)
                {
                    id = i; break;
                }
            }
            if (id >= 0) { Players.RemoveAt(id); }
        }

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
