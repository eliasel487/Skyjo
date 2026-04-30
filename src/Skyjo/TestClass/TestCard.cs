using System;
using ClassLibrary;
using Xunit;

namespace TestClass
{
    public class TestCard
    {
        [Fact]
        public void VerifCreationCard()
        {
            // Arrange
            Card card1 = new Card(11);

            // Act et Assert
            Assert.Equal(11, card1.Number);
            Assert.Equal(11, card1.GetNumber());
            Assert.False(card1.IsFlipped());
            Assert.Equal(" - 11 (face cachée)", card1.ToString());
        }

        [Fact]
        public void FlipCard()
        {
            // Arrange
            Card card = new Card(3);

            // Act
            card.FlipCard();

            // Assert
            Assert.True(card.IsFlipped());
            Assert.Equal(" - 3 (face visible)", card.ToString());
        }

        [Fact]
        public void VerifIsFlipped()
        {
            // Arrange
            Card card = new Card(0);

            // Assert initial state
            Assert.False(card.IsFlipped());

            // Act
            card.FlipCard();

            // Assert after flip
            Assert.True(card.IsFlipped());
        }

        [Fact]
        public void VerifInvalidNumberSetsMinusThree()
        {
            // Arrange
            Card card = new Card(100);

            // Act & Assert
            Assert.Equal(-3, card.Number);
            Assert.Equal(-3, card.GetNumber());
        }
    }
}
