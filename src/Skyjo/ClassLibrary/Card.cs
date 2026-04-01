using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;

namespace ClassLibrary
{
    public class Card
    {
        private int number;
        private bool face = false;

        public Card(int nb)
        {
            if (nb >= -2 && nb <=12) number = nb;
            else number = -3;
        }

        public int GetNumber()
        {
            return number;
        }

        public bool IsReturned()
        {
            return face;
        }

        public void ReturnCard()
        {
            face = true;
        }

        public override string ToString()
        {
            return $" - {number}";
        }
    }
}
