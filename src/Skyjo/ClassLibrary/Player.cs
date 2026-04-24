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

        public Card PickACard(Deck d)
        {
            int choose;
            Console.WriteLine("Voulez vous tirer dans le deck ou la défausse (0 : tirer, 1 : défausse) : ");
            choose = Console.Read();
            return d.PickACard(choose);
        }

        public bool RoundOver()
        {
            return _board.IsAllReturned();
        }

        public void Play(Deck d)
        {
            int choose, x, y;
            Card c = PickACard(d);

            Console.WriteLine("Voulez vous placer votre carte ou la défausser (0 : placer, 1 : défausser) : ");
            choose = Console.Read();

            if (choose == 0)
            {
                Console.WriteLine("Entrer les coordoner X et Y une par une pour placer votre carte : ");
                x = Console.Read();
                y = Console.Read();
                _board.ReplaceCard(y, x, c);
            }
            else
            {
                d.DiscardACard(c);
                Console.WriteLine("Entrer les coordoner X et Y une par une pour retourner une carte face caché : ");
                x = Console.Read();
                y = Console.Read();
                while(_board.ReturnACard(x, y) == false)
                {
                    Console.WriteLine("Coordoner incorect ! Entrer les coordoner X et Y une par une pour retourner une carte face caché : ");
                    x = Console.Read();
                    y = Console.Read();
                }

            }


        }
    }
}
