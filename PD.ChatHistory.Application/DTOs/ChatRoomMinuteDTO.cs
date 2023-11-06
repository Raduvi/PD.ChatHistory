using PD.ChatHistory.Domain.Entities.Chatroom;

namespace PD.ChatHistory.Application.DTOs
{
    public class ChatRoomMinuteDTO
    {
        public IEnumerable<MinutelyView> MinutelyView { get; set; } = new List<MinutelyView>();
    }
}