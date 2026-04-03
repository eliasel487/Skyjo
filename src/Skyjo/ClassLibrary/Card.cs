using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;

namespace ClassLibrary
{
    public class Card
    {
        public int Number {  get; }
        private bool _face = false;

        public Card(int nb)
        {
            if (nb >= -2 && nb <=12) Number = nb;
            else Number = -3;
        }

        public int GetNumber()
        {
            return Number;
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
            if (_face) return $" - {Number} (face visible)";
            return $" - {Number} (face cachée)";
        }
    }
}
