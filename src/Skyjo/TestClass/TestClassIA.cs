using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestClass
{
    public class TestClassIA
    {
        [Fact]
        public void IA_Creation()
        {
            //arrange
            Deck deck = new Deck();
            IA ia = new IA(0, deck, 0);

            //act
            string name = ia._name;
            int game = ia._gameScore;
            int round = ia._roundScore;
            bool IsIA = ia.GetIA();

            //assert
            Assert.Equal("IA", name);
            Assert.Equal(0, game);
            Assert.Equal(0, round);
            Assert.True(IsIA);
        }

        [Fact]
        public void IA_PlaceCardAuto()
        {
            //arrange
            Deck deck = new Deck();
            IA ia = new IA(0, deck, 0);
            ia.PlaceCardAuto(deck.PickACard(0));

            //act
            Card c = ia.GetBoard().GetColumns()[0].GetCards()[0];

            //assert
            Assert.True(c.IsFlipped());
        }

        [Fact]
        public void IA_PlayRound()
        {
            //arrange
            Deck deck = new Deck();
            IA ia = new IA(0, deck, 0);
            ia.PlayRound(deck);

            //act
            Card c = ia.GetBoard().GetColumns()[0].GetCards()[0];

            //assert
            Assert.True(c.IsFlipped());

        }
    }
}
