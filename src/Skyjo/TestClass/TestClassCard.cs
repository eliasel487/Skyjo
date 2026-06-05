using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestClass
{
    public class TestClassCard
    {
        [Fact]
        public void Card_CreateCard_GetNumber()
        {
            //arrange
            Card c = new Card(3);

            //act
            int res = c.Number;
            bool flip = c.IsFlipped();

            //assert
            Assert.Equal(3, res);
            Assert.False(flip);
        }

        [Fact]
        public void Card_CreateCardWrongNumber_GetNumber()
        {
            //arrange
            Card c1 = new Card(91);
            Card c2 = new Card(-5);

            //act
            int res1 = c1.Number;
            bool flip1 = c1.IsFlipped();
            int res2 = c2.Number;
            bool flip2 = c2.IsFlipped();

            //assert
            Assert.Equal(-3, res1);
            Assert.False(flip1);
            Assert.Equal(-3, res2);
            Assert.False(flip2);
        }

        [Fact]
        public void Card_FlipACard_CardFlipped()
        {
            //arrange
            Card c = new Card(1);
            c.FlipCard();

            //act
            bool flip = c.IsFlipped();

            //assert
            Assert.True(flip);
        }
    }
}
