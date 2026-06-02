using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;

namespace ClassLibrary
{
    [DataContract]
    public class Card
    {
        [DataMember (Name ="Numero_carte")]
        public int Number { get; set; }
        [DataMember (Name ="Carte_retournee")]
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

        public bool IsFlipped()
        {
            return _face;
        }

        public void FlipCard()
        {
            _face = true;
        }
    }
}
