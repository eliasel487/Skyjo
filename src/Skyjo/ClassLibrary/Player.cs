using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ClassLibrary
{
    public class Player
    {
        public string _name { get; }
        public int _gameScore { get; }
        public int _roundScore { get ; }
        private bool _IA;
        Board _board;


        public Player(string name, Deck deck, int GameScore, int RoundScore)
        {
            this._name = name;
            this._gameScore = GameScore;
            this._roundScore = RoundScore;
            this._IA = false;
            _board = new Board(deck);
            
        }
        public Player(int GameScore, Deck deck, int RoundScore)
        {
            this._name = "IA";
            this._gameScore = GameScore;
            this._roundScore = RoundScore;
            this._IA = true;
            _board = new Board(deck);
        }

        public bool GetIA()
        {
            return _IA;
        }

        public Board GetBoard()
        {
            return _board;
        }

        public void NewBoard(Deck d)
        {
            _board = new Board(d);
        }

        public Card PickACard(Deck d, int choose)
        {
            return d.PickACard(choose);
        }

        public bool RoundOver()
        {
            return _board.IsAllReturned();
        }

        public void Play(Deck d) //fonction utiliser pour les vues, elle sera peut être inutile
        {
            int choosePick = 0, chooseCard = 0, x = 0, y = 0;
            Card c = PickACard(d, choosePick);
            c.FlipCard();
            ActionCard(c, chooseCard, x, y);
        }

        public void ActionCard(Card c, int choose, int x, int y) // par rapport à ce que le joueur a choisi la carte sera soit placer soit on retourne une carte sur la plateau
        {
            if (choose == 0)
            {
                c.FlipCard();
                _board.ReplaceCard(y, x, c);
            }
            else
            {
                _board.ReturnACard(y, x);
            }
        }
    }
}
