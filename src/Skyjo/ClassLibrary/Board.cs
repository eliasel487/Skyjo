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

        public Board()
        {
            _columns = new List<Column>();
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


