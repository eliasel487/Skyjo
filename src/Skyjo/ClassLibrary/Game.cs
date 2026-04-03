using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Game
    {
        List<Player> _players = new();
        Deck _deck = new();
        bool _roundFinished;

        public void AddPlayer(Player player)
        {
            if (_players.Count < 5)
            {
                _players.Add(player);
            }
        }

        public void RemovePlayer(string name)
        {
            foreach (Player player in _players)
            {
                if (player.Name == name)
                {
                    _players.Remove(player);
                }
            }
        }
        public void PlayGame()
        {
            int cpt = 1;
            while (!_roundFinished)
            {
                if (cpt == 5) cpt = 1;
                _players[cpt].Play(_deck);
            }
            cpt++;
        }
    }
}
