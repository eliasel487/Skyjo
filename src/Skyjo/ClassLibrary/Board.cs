using ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Text;

namespace ClassLibrary
{
    [DataContract]
    public class Board
    {
        [DataMember (Name ="Colonnes")]
        private readonly List<Column> _columns = new();

        public Board(Deck deck)
        {
            for (int i = 0; i < 4; i++)
            {
                _columns.Add(new Column(deck.PickACard(0), deck.PickACard(0), deck.PickACard(0)));
            }
        }
        public void ReplaceCard(Deck deck, int y, int x, Card t)
        {
            if (y >= 0 && y < _columns.Count)
            {
                List<Card> cards = _columns[y].GetCards();
                if (x >= 0 && x < cards.Count)
                {
                    deck.DiscardACard(cards[x]);
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
            return _columns;
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


