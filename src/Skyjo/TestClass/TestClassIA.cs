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
            //act
            Deck deck = new Deck();
            IA ia = new IA(0, deck, 0);

            //arrange

        }
    }
}
