using PD.ChatHistory.Domain.Entities.Chatroom;
using System.ComponentModel.DataAnnotations;

namespace PD.ChatHistory.Application.DTOs
{
    public class ChatRoomDTO
    {
        public int Id { get; init; }

        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(450)]
        public string Description { get; set; } = string.Empty;

        public IEnumerable<MinutelyView> MinutelyView { get; set; } = new List<MinutelyView>();

        public IEnumerable<HourlyView> HourlyView { get; set; } = new List<HourlyView>();
    }
}