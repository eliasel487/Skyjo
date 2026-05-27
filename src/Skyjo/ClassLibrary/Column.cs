using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassLibrary
{
    public class Column
    {
        private readonly List<Card> _cards = new();
        private bool _valued { get; set; } = true;

        public Column(Card card1, Card card2, Card card3)
        {
            _cards.Add(card1);
            _cards.Add(card2);
            _cards.Add(card3);
        }

        public bool GetValued()
        {
            return _valued;
        }

        public void Remove()
        {
            if (IsRemovable()) 
            {
                _valued = false;
            }
        }

        public bool IsRemovable()
        {
            if (_cards[0].IsFlipped() && _cards[1].IsFlipped() && _cards[2].IsFlipped())
                return _cards[0].Number == _cards[1].Number && _cards[1].Number == _cards[2].Number;
            return false;
        }

        public List<Card> GetCards()
        {
            List<Card> column = new List<Card>();
            if (_cards[0].GetNumber() == -3 || _cards[1].GetNumber() == -3 || _cards[2].GetNumber() == -3)
            {
                return column; // renvoie une liste vide
            }
            return _cards;
        }
    }
}
