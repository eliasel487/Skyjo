using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassLibrary
{
    public class Column
    {
        private Card _cardTop;
        private Card _cardMiddle;
        private Card _cardBottom;
        private bool _valued = true;

        public Column(Card cardTop, Card cardMiddle, Card cardBottom)
        {
            _cardTop = cardTop;
            _cardMiddle = cardMiddle;
            _cardBottom = cardBottom;
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
            if(_cardTop.IsReturned() == true && _cardMiddle.IsReturned() == true && _cardBottom.IsReturned() == true)
            {
                if(_cardTop.GetNumber() == -3 || _cardMiddle.GetNumber() == -3 || _cardBottom.GetNumber() == -3) 
                    return false;
                return _cardTop.GetNumber() == _cardMiddle.GetNumber() && _cardMiddle.GetNumber() == _cardBottom.GetNumber();
            }
            return false;
        }

        List<Card> column = new List<Card>();
        public List<Card> GetCards()
        {
            if (_cardTop.GetNumber() == -3 || _cardMiddle.GetNumber() == -3 || _cardBottom.GetNumber() == -3)
            {
                return column;
            }
            else
            {
                column.Add(_cardTop);
                column.Add(_cardMiddle);
                column.Add(_cardBottom);
            }
            return column;
        }

        public override string ToString()
        {
            if (_cardTop.GetNumber() == -3 || _cardMiddle.GetNumber() == -3 || _cardBottom.GetNumber() == -3)
            {
                return "";
            }
            else
            {
                return $"{_cardTop.ToString()} \n" +
                       $"{_cardMiddle.ToString()} \n" +
                       $"{_cardBottom.ToString()}";
            }
        }
    }
}
