using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Game
    {
        List<Player> _players = new();
        Deck _deck = new();
        bool _roundFinished = false;
        bool _gameOver = false;

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
                if (player._name == name)
                {
                    _players.Remove(player);
                }
            }
        }
        public void PlayGame()
        {
            int cpt, temp;
            while (!_gameOver)
            {
                for (int i = 1; i <= _players.Count(); i++)
                {
                    if (_players[i]._gameScore <= 100) _gameOver = true;
                }

                if (_gameOver) break;
                
                cpt = 1;
                while (!_roundFinished)
                {
                    if (cpt == _players.Count()) cpt = 1;
                    if (_players[cpt].RoundOver()) _roundFinished = true;
                    _players[cpt].Play(_deck);
                    cpt++;
                }
                temp = cpt;
                cpt++;
                while (cpt != temp)
                {
                    if (cpt == _players.Count()) cpt = 1;
                    _players[cpt].Play(_deck);
                    cpt++;
                }
            }
        }

        public void AfficherGame()
        {
            int i;
            for (i = 0; i < 5; i++)
            {
                _players[i].AfficherBoardPlayer();
                Console.WriteLine();
            }
        }
    }
}
