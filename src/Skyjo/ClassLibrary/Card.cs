using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;

namespace ClassLibrary
{
    public class Card
    {
        private int _number;
        private bool _face = false;

        public Card(int nb)
        {
            if (nb >= -2 && nb <=12) _number = nb;
            else _number = -3;
        }

        public int GetNumber()
        {
            return _number;
        }

        public bool IsReturned()
        {
            return _face;
        }

        public void ReturnCard()
        {
            _face = true;
        }

        public override string ToString()
        {
            if (_face) return $" - {_number} (face visible)";
            return $" - {_number} (face cachée)";
        }
    }
}
