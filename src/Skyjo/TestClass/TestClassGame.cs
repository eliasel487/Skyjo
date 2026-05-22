using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestClass
{
    public class TestClassGame
    {
        [Fact]
        public void Game_GetRoundFinishedAndGetGameOver()
        {
            //act
            Game game = new Game();

            //arrange
            bool round = game.GetRoundFinished();
            bool g = game.GetGameOver();

            //assert
            Assert.False(round);
            Assert.False(g);
        }

        [Fact]
        public void Game_AddPlayerAndAddIA()
        {
            //act
            Game game = new Game();
            game.AddPlayer("yipee");
            game.AddIA();

            //arrange
            int nbPlayer = game.Players.Count;
            int nbIA = game.IAs.Count;

            //assert
            Assert.Equal(1, nbPlayer);
            Assert.Equal(1, nbIA);
        }

        [Fact]
        public void Game_AddPlayerAndAddIAOverClock()
        {
            //act
            Game game1 = new Game();
            Game game2 = new Game();
            game1.AddPlayer("yipee1");
            game1.AddPlayer("yipee2");
            game1.AddPlayer("yipee3");
            game1.AddPlayer("yipee4");
            game1.AddPlayer("yipee5");

            game2.AddIA();
            game2.AddIA();
            game2.AddIA();
            game2.AddIA();
            game2.AddIA();

            //arrange
            int nbPlayer = game1.Players.Count;
            int nbIA = game2.IAs.Count;

            //assert
            Assert.Equal(4, nbPlayer);
            Assert.Equal(4, nbIA);
        }

        [Fact]
        public void Game_RemovePlayerAndRemoveIA()
        {
            //act
            Game game = new Game();
            game.AddPlayer("yipee1");
            game.AddPlayer("yipee2");

            game.AddIA();
            game.AddIA();

            game.RemoveAllIA();
            game.RemovePlayer("yipee1");

            //arrange
            int nbPlayer = game.Players.Count;
            int nbIA = game.IAs.Count;

            //assert
            Assert.Equal(1, nbPlayer);
            Assert.Equal(0, nbIA);
        }

    }
}
