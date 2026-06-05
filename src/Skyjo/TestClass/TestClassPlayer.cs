using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestClass
{
    public class TestClassPlayer
    {
        [Fact]
        public void Player_Creation()
        {
            //arrange
            Deck deck = new Deck();
            Player p = new Player("Jean", deck, 0, 0);

            //act
            string name = p.Name;
            int game = p.GameScore;
            int round = p.RoundScore;

            //assert
            Assert.Equal("Jean", name);
            Assert.Equal(0, game);
            Assert.Equal(0, round);
        }

        [Fact]
        public void Player_newBoard()
        {
            //arrange 
            Deck deck = new Deck();
            Player p = new Player("Jean", deck, 0, 0);
            Card before = p.Board.GetColumns()[0].GetCards()[0];

            //act
            p.NewBoard(deck);
            Card after = p.Board.GetColumns()[0].GetCards()[0];

            //assert
            Assert.NotEqual(before, after);
        }

        [Fact]
        public void Player_GetIa()
        {
            //arrange 
            Deck deck = new Deck();
            Player p = new Player("Jean", deck, 0, 0);

            //act
            bool res = p.GetIA();

            //assert
            Assert.False(res);
        }

        [Fact]
        public void Player_PickSomeCards()
        {
            //arrange 
            Deck deck = new Deck();
            Player p = new Player("Jean", deck, 0, 0);

            //act
            Card c1 = Player.PickACard(deck, 0);
            Card c2 = Player.PickACard(deck, 0);
            Card c3 = Player.PickACard(deck, 0);
            deck.DiscardACard(c1);
            deck.DiscardACard(c2);
            Card c4 = Player.PickACard(deck, 1);

            //assert
            Assert.Equal(136, deck.NbCardPull());
            Assert.Equal(1, deck.NbCardDiscard());
        }

        [Fact]
        public void Player_RoundOver()
        {
            //arrange 
            Deck deck = new Deck();
            Player p = new Player("Jean", deck, 0, 0);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    p.Board.GetColumns()[i].GetCards()[j].FlipCard();
                }
            }

            //act
            bool res = p.RoundOver();

            //assert
            Assert.True(res);
        }

        [Fact]
        public void Player_Play()
        {
            //arrange 
            Deck deck = new Deck();
            Player p = new Player("Jean", deck, 0, 0);
            p.Play(deck);

            //act
            Card c = p.Board.GetColumns()[0].GetCards()[0];

            //assert
            Assert.True(c.IsFlipped());
        }

        [Fact]
        public void Player_ActionCard()
        {
            //arrange 
            Deck deck = new Deck();
            Player p = new Player("Jean", deck, 0, 0);
            p.ActionCard(deck, deck.PickACard(0), 0, 1, 2);
            p.ActionCard(deck, deck.PickACard(0), 1, 2, 2);

            //act
            Card c1 = p.Board.GetColumns()[2].GetCards()[1];
            Card c2 = p.Board.GetColumns()[2].GetCards()[2];

            //assert
            Assert.True(c1.IsFlipped());
            Assert.True(c2.IsFlipped());
        }


    }
}
