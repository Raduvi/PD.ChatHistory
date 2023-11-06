using PD.ChatHistory.Domain.Entities.Chatroom;

namespace PD.ChatHistory.Application.DTOs
{
    public class ChatRoomHourDTO
    {
        public IEnumerable<HourlyView> HourlyView { get; set; } = new List<HourlyView>();
    }
}