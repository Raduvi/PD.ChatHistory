using PD.ChatHistory.Domain.Entities.Common;
using PD.ChatHistory.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PD.ChatHistory.Domain.Entities.User
{
    public class ChatUser : Entity
    {
        [MaxLength(150)]
        public string UserName { get; private set; } = string.Empty;

        [EmailAddress]
        public string Email { get; private set; } = string.Empty;

        public UserTypes UserType { get; private set; } = UserTypes.User;

        public ChatUser Create(
            string username,
            string email,
            UserTypes userType,
            int createdBy,
            DateTime createdOnUTC,
            int updatedBy,
            DateTime updatedOnUTC)
        {
            return new ChatUser()
            {
                UserType = userType,
                Email = email,
                UserName = username,
                CreatedBy = createdBy,
                CreatedOnUTC = createdOnUTC,
                UpdatedBy = updatedBy,
                UpdatedOnUTC = updatedOnUTC
            };
        }

    }
}
