using ClassLibrary;
using System.Net.NetworkInformation;

namespace TestDeck
{
    public class UnitTest1
    {
        [Fact]
        public void VerifCreationDeck()
        {
            // arange
            var deck1 = new Deck();

            int nbM2 = 0, nbM1 = 0, nb0 =0, nb1=0, nb2 = 0, nb3 = 0, nb4 = 0, nb5 = 0, nb6 = 0, nb7 = 0, nb8 = 0, nb9 = 0, nb10 = 0, nb11 = 0, nb12 = 0;

            for (int i = 0; i <= 150; i++)
            {
                Card card1 = deck1.PickACard(0);
                if (card1.GetNumber() == -2)
                    nbM2++;
                if (card1.GetNumber() == -1)
                    nbM1++;
                if (card1.GetNumber() == 0)
                    nb0++;
                if (card1.GetNumber() == 1)
                    nb1++;
                if (card1.GetNumber() == 2)
                    nb2++;
                if (card1.GetNumber() == 3)
                    nb3++;
                if (card1.GetNumber() == 4)
                    nb4++;
                if (card1.GetNumber() == 5)
                    nb5++;
                if (card1.GetNumber() == 6)
                    nb6++;
                if (card1.GetNumber() == 7)
                    nb7++;
                if (card1.GetNumber() == 8)
                    nb8++;
                if (card1.GetNumber() == 9)
                    nb9++;
                if (card1.GetNumber() == 10)
                    nb10++;
                if (card1.GetNumber() == 11)
                    nb11++;
                if (card1.GetNumber() == 12)
                    nb12++;

            }
            Assert.Equal(5, nbM2);
            Assert.Equal(10, nbM1);
            Assert.Equal(15, nb0);
            Assert.Equal(10, nb1);
            Assert.Equal(10, nb2);
            Assert.Equal(10, nb3);
            Assert.Equal(10, nb4);
            Assert.Equal(10, nb5);
            Assert.Equal(10, nb6);
            Assert.Equal(10, nb7);
            Assert.Equal(10, nb8);
            Assert.Equal(10, nb9);
            Assert.Equal(10, nb10);
            Assert.Equal(10, nb11);
            Assert.Equal(10, nb12);
        }
    }
}
