using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Deck
    {
        readonly List<Card>  _pull = new();
        readonly List<Card> _discard = new();

        public Deck()
        {
            for (int i = 10; i < 161; i ++)
            {
                if (i < 131) _pull.Add(new Card(i / 10));
                else if (i < 141) _pull.Add(new Card(-1));
                else if (i < 146) _pull.Add(new Card(-2));
                else _pull.Add(new Card(0));
            }
        }

        public int NbCardPull() { return _pull.Count; }
        public int NbCardDiscard() { return _discard.Count; }

        public void DiscardACard(Card card)
        {
            _discard.Add(card);
        }

        public Card PickACard(int nb)
        {
            Random rnd = new Random();
            int n;
            Card res;
            if (nb == 0)
            {
                n = rnd.Next(_pull.Count);
                res = _pull[n];
                _pull.RemoveAt(n);
                return res;
            }
            res = _discard[0];
            _discard.RemoveAt(0);
            return res;
        }

        public bool IsEmptyPull()
        {
            return _pull.Count == 0;
        }

        public bool IsEmptyDiscard()
        {
            return _discard.Count == 0;
        }
    }
}
