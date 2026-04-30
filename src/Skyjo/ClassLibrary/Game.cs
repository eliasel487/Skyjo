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

        public void AddPlayer(string nomJoueur)
        {
            Player p;
            p = new Player(nomJoueur, _deck, 0, 0);

            if (_players.Count <= 5)
            {
                _players.Add(p);
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
            int cpt, temp, nbJoueur = _players.Count;

            

            while (!_gameOver)
            {

                for(int i = 0; i < nbJoueur; i++) // créer un nouveau plateau au début de chaque tour
                {
                    _players[i].NewBoard(_deck);
                }

                _roundFinished = false;
                for (int i = 0; i < _players.Count(); i++)
                {
                    if (_players[i]._gameScore >= 100) _gameOver = true;
                }

                if (_gameOver) break;
                
                cpt = 0;
                while (!_roundFinished)
                {
                    AfficherGame(nbJoueur);
                    if (cpt == _players.Count()) cpt = 0;
                    if (_players[cpt].RoundOver()) 
                    { 
                        _roundFinished = true; 
                        break; 
                    }
                    _players[cpt].Play(_deck);
                    cpt++;
                }
                temp = cpt;
                cpt++;
                while (cpt != temp)
                {
                    if (cpt == _players.Count()) cpt = 0;
                    _players[cpt].Play(_deck);
                    cpt++;
                }
            }
        }


        public void AfficherGame(int nbJoueur)
        {
            int i;
            for (i = 0; i < nbJoueur; i++)
            {
                _players[i].AfficherBoardPlayer();
                Console.WriteLine();
            }
        }
    }
}
