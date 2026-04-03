using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Game
    {
        List<Player> _players = new();
        Deck _deck = new();
        
        public void AddPlayer(Player player)
        {
            if (_players.Count < 5)
            {
                _players.Add(player);
            }
        }

        public void RemovePlayer(string nom)
        {
            foreach (Player player in _players)
            {

            }
        }
    }
}
