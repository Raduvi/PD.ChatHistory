using PD.ChatHistory.Domain.Entities.Chatroom;

namespace PD.ChatHistory.Domain.UnitTests
{
    public class ChatRoomTests
    {
        [Fact]
        public void GetHourly_ReturnsHourlyViewList()
        {
            // Arrange
            var chatRoom = new ChatRoom();

            // Act
            var result = chatRoom.GetHourly();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<HourlyView>>(result);
        }

        [Fact]
        public void GetHourly_ReturnsMinutelyViewList()
        {
            // Arrange
            var chatRoom = new ChatRoom();

            // Act
            var result = chatRoom.GetMinutely();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<MinutelyView>>(result);
        }
    }
}
