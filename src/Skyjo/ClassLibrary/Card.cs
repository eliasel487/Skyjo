using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;

namespace ClassLibrary
{

    /**
     * @class Card
     * @brief Classe qui permet d'implementer des cartes avec des numéros allant de -2 à 12
     */
    [DataContract]
    public class Card
    {
        [DataMember (Name ="Numero_carte")]//!< Numero_carte est le numero de la carte pour la persistance
        public int Number { get; set; }//!< Number est le numero de la carte
        [DataMember (Name ="Carte_retournee")] //!< Carte_retournee est l'état de la carte (face cachée ou face visible) pour la persistance
        private bool _face = false;//!< _face est l'état de la carte (face cachée ou face visible)

        /**
         * @brief Déclaration du constructeur de la classe Card
         * @param nb Numero qui permet de créer la carte
         */
        public Card(int nb)
        {
            if (nb >= -2 && nb <=12) Number = nb;
            else Number = -3;
        }

        /**
         * @brief Accesseur de l'attribut _face
         * @return _face Booléen qui definit l'état de la carte
         */
        public bool IsFlipped()
        {
            return _face;
        }

        /**
         * @brief Déclaration de la méthode FlipCard qui permet de retourner un carte
         */
        public void FlipCard()
        {
            _face = true;
        }
    }
}
