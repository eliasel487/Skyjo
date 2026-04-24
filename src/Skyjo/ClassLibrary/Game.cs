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
            int cpt, temp, nbJoueur = 0;
            string nomJoueur;
            
            Console.WriteLine("Combien de joueur voulez vous ajouter ? : ");
            nbJoueur = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < nbJoueur; i++)
            {
                Console.WriteLine("Taper nom du joueur : ");
                nomJoueur = Console.ReadLine();
                while(nomJoueur == null)
                    nomJoueur = Console.ReadLine();
                Player p = new Player(nomJoueur, _deck, 0, 0);
                _players.Add(p);
            }

            while (!_gameOver)
            {
                for (int i = 0; i < _players.Count(); i++)
                {
                    if (_players[i]._gameScore >= 100) _gameOver = true;
                }

                if (_gameOver) break;
                
                cpt = 0;
                while (!_roundFinished)
                {
                    AfficherGame(nbJoueur);
                    if (cpt == _players.Count()-1) cpt = 0;
                    if (_players[cpt].RoundOver()) _roundFinished = true;
                    _players[cpt].Play(_deck);
                    cpt++;
                }
                temp = cpt;
                cpt++;
                while (cpt != temp)
                {
                    if (cpt == _players.Count()-1) cpt = 0;
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
