using ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Text;

namespace ClassLibrary
{
    public class Board
    {
        private List<Column> _columns = new();

        public Board(Deck deck)
        {
            for (int i = 0; i < 4; i++)
            {
                _columns.Add(new Column(deck.PickACard(0), deck.PickACard(0), deck.PickACard(0)));
            }
        }
        public void ReplaceCard(int y, int x, Card t)
        {
            if (y >= 0 && y < _columns.Count)
            {
                List<Card> cards = _columns[y].GetCards();
                if (x >= 0 && x < cards.Count)
                {
                    cards[x] = t;
                }
            }
        }
        public bool IsAllReturned()
        {
            foreach (Column c in _columns)
            {
                foreach (Card card in c.GetCards())
                {
                    if (!card.IsFlipped())
                        return false;
                }
            }
            return true;
        }
        public List<Column> GetColumns()
        {
            //if (i > 0 && i < 5)
            //    if (!_columns[i].IsRemovable())
            return _columns;
            //throw new ArgumentOutOfRangeException();
        }

        public bool ReturnACard(int y, int x)
        {
            List<Card> cards = _columns[y].GetCards();
            if (x >= 0 && x < cards.Count)
            {
                cards[x].FlipCard();
                return true;
            }
            return false;
        }
    }
}


