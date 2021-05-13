using FluentAssertions;
using Game;
using Xunit;

namespace GameTests
{
    public class BoardTests
    {
        [Fact]
        public void An_Empty_Board_Is_Scored_Correctly()
        {
            // Arrange
            var board = new Board();
            
            // Act
            var score = board.CalculateScore();
            
            // Assert
            score.Should().Be(24);
        }
    }
}
