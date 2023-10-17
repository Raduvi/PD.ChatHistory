using PD.ChatHistory.Domain.Entities.Chatroom;
using PD.ChatHistory.Domain.Entities.Common;
using PD.ChatHistory.Domain.Entities.User;
using PD.ChatHistory.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PD.ChatHistory.Domain.Entities.ChatroomEvent
{
    public class ChatRoomEvent : Entity
    {
        public ChatRoom? Chatroom { get; private set; }

        [MaxLength(450)]
        public string Comment { get; private set; } = string.Empty;

        public ChatUser? ChatUser { get; private set; }

        public ChatUser? HighFivedChatUser { get; private set; }

        public EventTypes EventType { get; private set; } = EventTypes.Enter;

        public ChatRoomEvent Create(
            ChatRoom chatroom,
            string comment,
            ChatUser chatUser,
            EventTypes eventType,
            DateTime createdOnUTC,
            DateTime updatedOnUTC,
            ChatUser? highFivedUser = null)
        {
            return new ChatRoomEvent()
            {
                Chatroom = chatroom,
                Comment = comment,
                ChatUser = chatUser,
                HighFivedChatUser = highFivedUser,
                EventType = eventType,
                CreatedBy = 1,
                CreatedOnUTC = createdOnUTC,
                UpdatedBy = 1,
                UpdatedOnUTC = updatedOnUTC
            };
        }
    }
}
