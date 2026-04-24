using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassLibrary
{
    public class Column
    {
        private List<Card> _cards = new();
        private bool _valued = true;

        public Column(Card card1, Card card2, Card card3)
        {
            _cards.Add(card1);
            _cards.Add(card2);
            _cards.Add(card3);
        }

        public void Remove()
        {
            if (isRemovable()) 
            {
                _valued = false;
            }
        }

        public bool isRemovable()
        {
            if (_cards[0].IsFliped() == true && _cards[1].IsFliped() == true && _cards[2].IsFliped() == true)
                return _cards[0].Number == _cards[1].Number && _cards[1].Number == _cards[2].Number;
            return false;
        }

        List<Card> column = new List<Card>();
        public List<Card> GetCards()
        {
            if (_cards[0].GetNumber() == -3 || _cards[1].GetNumber() == -3 || _cards[2].GetNumber() == -3)
            {
                return column;
            }
            else
            {
                column.Add(_cards[0]);
                column.Add(_cards[1]);
                column.Add(_cards[2]);
            }
            return column;
        }

        public override string ToString()
        {
            if (_cards[0].GetNumber() == -3 || _cards[1].GetNumber() == -3 || _cards[2].GetNumber() == -3)
            {
                return "";
            }
            else
            {
                return $"{_cards[0].ToString()} \n" +
                       $"{_cards[1].ToString()} \n" +
                       $"{_cards[2].ToString()}";
            }
        }

        public void AfficherColumn()
        {
            int i;
            for (i = 0; i < 3; i++)
            {
                if (_cards[i].IsFliped() == false)
                    Console.Write(" X ");
                else
                    Console.Write(" " + _cards[i].GetNumber() + " ");
            }
        }
    }
}
