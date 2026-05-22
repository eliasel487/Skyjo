using System;
using ClassLibrary;
using Xunit;

namespace TestClass
{
    public class TestClassBoard
    {
        [Fact]
        public void Board_Initial_IsAllReturned_False()
        {
            // Arrange
            var deck = new Deck();
            var board = new Board(deck);

            // Act & Assert
            Assert.False(board.IsAllReturned());
        }

        [Fact]
        public void Board_ReturnAllCards_IsAllReturned_True()
        {
            // Arrange
            var deck = new Deck();
            var board = new Board(deck);

            // Act
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    var ok = board.ReturnACard(y, x);
                }
            }

            // Assert
            Assert.True(board.IsAllReturned());
        }

        [Fact]
        public void Board_ReturnACard_InvalidIndex_ReturnsFalse()
        {
            // Arrange
            var deck = new Deck();
            var board = new Board(deck);

            // Act
            var resultNegative = board.ReturnACard(0, -1);
            var resultTooLarge = board.ReturnACard(0, 13);
            var resultTooLarge1 = board.ReturnACard(0, 12);

            // Assert
            Assert.False(resultNegative);
            Assert.False(resultTooLarge);
            Assert.False(resultTooLarge1);
        }

        [Fact]
        public void Board_ReplaceCard_ReplacesCard()
        {
            // Arrange
            var deck = new Deck();
            var board = new Board(deck);
            var column = board.GetColumns();

            var cardsColumnBefore = column[0].GetCards();
            Assert.True(cardsColumnBefore.Count >= 3);
            var card1 = cardsColumnBefore[0];
            var card2 = cardsColumnBefore[1];
            var card3 = cardsColumnBefore[2];

            // Act
            var newCard = new Card(7);
            board.ReplaceCard(0, 1, newCard);

            var columnAfter = column[0].GetCards();
            var card1After = columnAfter[0];
            var card2After = columnAfter[1];
            var card3After = columnAfter[2];

            // Assert
            Assert.Equal(card1, card1After);
            Assert.NotEqual(card2, card2After);
            Assert.Equal(card3, card3After);
        }

        [Fact]
        public void Board_ReplaceCardIncorrectCoordonnees_DontReplacesCard()
        {
            // Arrange
            var deck = new Deck();
            var board = new Board(deck);
            var column = board.GetColumns();

            var cardsColumnBefore = column[0].GetCards();
            Assert.True(cardsColumnBefore.Count >= 3);
            var card1 = cardsColumnBefore[0];
            var card2 = cardsColumnBefore[1];
            var card3 = cardsColumnBefore[2];

            // Act
            var newCard = new Card(7);
            board.ReplaceCard(0, 8, newCard);
            board.ReplaceCard(0, -1, newCard);
            board.ReplaceCard(-1, 1, newCard);
            board.ReplaceCard(8, 1, newCard);

            var columnAfter = column[0].GetCards();
            var card1After = columnAfter[0];
            var card2After = columnAfter[1];
            var card3After = columnAfter[2];

            // Assert
            Assert.Equal(card1, card1After);
            Assert.Equal(card2, card2After);
            Assert.Equal(card3, card3After);
        }
    }
}
