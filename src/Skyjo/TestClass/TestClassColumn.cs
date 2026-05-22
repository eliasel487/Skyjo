using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestClass
{
    public class TestClassColumn
    {
        [Fact]
        public void isRemovable_ReturnsTrue()
        {
            // Arrange
            var _card1 = new Card(6);
            var _card2 = new Card(6);
            var _card3 = new Card(6);
            var _colonne = new Column(_card1, _card2, _card3);
            _card1.FlipCard();
            _card2.FlipCard();
            _card3.FlipCard();

            // Act
            bool res = _colonne.IsRemovable();

            // Assert
            Assert.True(res);
        }

        [Fact]
        public void isRemovable_ReturnsFalse_CaseNotFullyFliped()
        {
            // Arrange
            var _card1 = new Card(7);
            var _card2 = new Card(7);
            var _card3 = new Card(7);
            var _colonne = new Column(_card1, _card2, _card3);
            _card1.FlipCard();
            _card3.FlipCard();

            // Act
            bool res = _colonne.IsRemovable();

            // Assert
            Assert.False(res);
        }

        [Fact]
        public void isRemovable_ReturnsFalse_CaseNotSameCards()
        {
            // Arrange
            var _card1 = new Card(7);
            var _card2 = new Card(4);
            var _card3 = new Card(10);
            var _colonne = new Column(_card1, _card2, _card3);
            _card1.FlipCard();
            _card2.FlipCard();
            _card3.FlipCard();

            // Act
            bool res = _colonne.IsRemovable();

            // Assert
            Assert.False(res);
        }

        [Fact]
        public void Remove_SuccessCase()
        {
            // Arrange
            var _card1 = new Card(6);
            var _card2 = new Card(6);
            var _card3 = new Card(6);
            var _colonne = new Column(_card1, _card2, _card3);
            _card1.FlipCard();
            _card2.FlipCard();
            _card3.FlipCard();

            // Act
            _colonne.Remove();

            // Assert
            Assert.False(_colonne.GetValued());
        }

        [Fact]
        public void Remove_FailCase()
        {
            // Arrange
            var _card1 = new Card(1);
            var _card2 = new Card(-2);
            var _card3 = new Card(12);
            var _colonne = new Column(_card1, _card2, _card3);
            _card1.FlipCard();
            _card2.FlipCard();
            _card3.FlipCard();

            // Act
            _colonne.Remove();

            // Assert
            Assert.True(_colonne.GetValued());
        }

        [Fact]
        public void GetCards_ReturnsCorrectCards()
        {
            // Arrange
            var _card1 = new Card(1);
            var _card2 = new Card(6);
            var _card3 = new Card(9);
            var _colonne = new Column(_card1, _card2, _card3);
            _card1.FlipCard();
            _card2.FlipCard();
            _card3.FlipCard();

            List<Card> expected = new List<Card>();
            expected.Add(_card1);
            expected.Add(_card2);
            expected.Add(_card3);

            // Act
            var actual = _colonne.GetCards();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetCards_FailsToReturn()
        {
            // Arrange
            var _card1 = new Card(1);
            var _card2 = new Card(6);
            var _card3 = new Card(12);
            var _colonne = new Column(_card1, _card2, _card3);
            _card1.FlipCard();
            _card2.FlipCard();

            List<Card> expected = new List<Card>();
            expected.Add(_card1);
            expected.Add(_card2);
            expected.Add(_card3);

            // Act
            var actual = _colonne.GetCards();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetCards_ReturnsNone()
        {
            // Arrange
            var _card1 = new Card(91);
            var _card2 = new Card(91);
            var _card3 = new Card(9);
            var _colonne = new Column(_card1, _card2, _card3);
            _card1.FlipCard();
            _card2.FlipCard();
            _card3.FlipCard();

            List<Card> expected = new List<Card>();
            expected.Add(_card1);
            expected.Add(_card2);
            expected.Add(_card3);

            // Act
            var actual = _colonne.GetCards();

            // Assert
            Assert.Empty(actual);
        }
    }
}
