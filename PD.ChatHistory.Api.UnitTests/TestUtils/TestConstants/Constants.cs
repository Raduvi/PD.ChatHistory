using PD.ChatHistory.Application.DTOs;
using PD.ChatHistory.Domain.Entities.Chatroom;

namespace PD.ChatHistory.Api.UnitTests.TestUtils.Constants
{
    public static class Constants
    {
        public const int NonExistingRoomId = 0;
        public const int ValidRoomId = 1;
        public static DateTime DefaultDatetime = DateTime.UtcNow;
        public const string RoomName = "Test Room 1";
        public const string Description = "Test Room 1 Description";
        public const string TestExceptionMessage = "An error occurred: Testing Exception";

        public static ChatRoomDTO chatRoomDTO = new ChatRoomDTO()
        {
            Name = Constants.RoomName,
            Description = Constants.Description,
        };

        public static ChatRoomHourDTO chatRoomHourDTO = new ChatRoomHourDTO()
        {
            HourlyView = new List<HourlyView>()
            {
                new HourlyView(12, "comments: 12"),
            },
        };

        public static ChatRoomMinuteDTO chatRoomMinuteDTO = new ChatRoomMinuteDTO()
        {
            MinutelyView = new List<MinutelyView>()
            {
                new MinutelyView(DateTime.Now, "comment: test"),
            }
        };
    }
}
