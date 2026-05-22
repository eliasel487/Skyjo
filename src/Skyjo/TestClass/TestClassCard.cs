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
            //act
            Card c = new Card(3);

            //arrange
            int res = c.GetNumber();
            bool flip = c.IsFlipped();

            //assert
            Assert.Equal(3, res);
            Assert.False(flip);
        }

        [Fact]
        public void Card_CreateCardWrongNumber_GetNumber()
        {
            //act
            Card c1 = new Card(91);
            Card c2 = new Card(-5);

            //arrange
            int res1 = c1.GetNumber();
            bool flip1 = c1.IsFlipped();
            int res2 = c2.GetNumber();
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
            //act
            Card c = new Card(1);
            c.FlipCard();

            //arrange
            bool flip = c.IsFlipped();

            //assert
            Assert.True(flip);
        }
    }
}
