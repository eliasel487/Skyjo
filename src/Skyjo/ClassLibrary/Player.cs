using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace ClassLibrary
{
    [DataContract]
    public class Player
    {
        [DataMember (Name="Nom_joueur")]
        public string _name { get; set; }

        [DataMember (Name="Score_total_joueur")]
        public int _gameScore { get; set; }

        [DataMember (Name="Score_manche_actuelle")]
        public int _roundScore { get; set; }

        private readonly bool _IA;
        [DataMember (Name="Plateau_carte_joueur")]
        public Board _board;

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

        public static Card PickACard(Deck d, int choose)
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
            ActionCard(d, c, chooseCard, x, y);
        }

        public void ActionCard(Deck deck, Card c, int choose, int x, int y) // par rapport à ce que le joueur a choisi la carte sera soit placer soit on retourne une carte sur la plateau
        {
            if (_board.GetColumns()[y].GetValued())
            {
                if (choose == 0)
                {
                    _board.ReplaceCard(deck, y, x, c);
                }
                else
                {
                    _board.ReturnACard(y, x); //_board.ReturnACard(x, y) == false
                }
            }
        }

        public void RemoveColumn()
        {
            for (int i = 0; i < 4; i++)
            {
                _board.GetColumns()[i].Remove();
            }
        }

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
            _board.GetColumns()[x1].GetCards()[y1].FlipCard();
            _board.GetColumns()[x2].GetCards()[y2].FlipCard();
            res += _board.GetColumns()[x1].GetCards()[y1].Number;
            res += _board.GetColumns()[x2].GetCards()[y2].Number;
            return res;
        }
    }
}
