using ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Text;

namespace ClassLibrary
{
    internal class Board
    {
        private List<Column> _columns;

        public Board(Column 1, Column 2, Column 3, Column 4)
        {
            _columns.Add(1);
            _columns.Add(2);
            _columns.Add(3);
            _columns.Add(4);

        }
        public void ReplaceCard(int y, int x, Card t)
        {
            
            List<Card> cards = _columns[y].GetCards();
            if (x >= 0 && x < cards.Count)
            {
                cards[x] = t;
            }
        }
        public bool IsAllReturned()
        {
            foreach (Column c in _columns)
            {
                foreach (Card card in c.GetCards())
                {
                    if (!card.IsReturned())
                        return false;
                }
            }
            return true;
        }
        public Column GetColumns() 
        {
            return _columns;
        }
    }


