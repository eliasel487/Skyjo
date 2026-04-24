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
                    Assert.True(ok);
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
            var resultTooLarge = board.ReturnACard(0, 5);

            // Assert
            Assert.False(resultNegative);
            Assert.False(resultTooLarge);
        }

        [Fact]
        public void Board_GetColumns_InvalidIndex_Throws()
        {
            var deck = new Deck();
            var board = new Board(deck);

            Assert.Throws<ArgumentOutOfRangeException>(() => board.GetColumns(0));
        }

        [Fact]
        public void Board_ReplaceCard_ReplacesCard()
        {
            // Arrange
            var deck = new Deck();
            var board = new Board(deck);
            var column = board.GetColumns(1);
            var cardsBefore = column.GetCards();
            Assert.True(cardsBefore.Count >= 3);
            int oldNumber = cardsBefore[1].GetNumber();

            // Act
            var newCard = new Card(7);
            board.ReplaceCard(1, 1, newCard);

            // Assert
            var cardsAfter = board.GetColumns(1).GetCards();
            Assert.Equal(7, cardsAfter[1].GetNumber());
            Assert.NotEqual(oldNumber, cardsAfter[1].GetNumber());
        }
    }
}
