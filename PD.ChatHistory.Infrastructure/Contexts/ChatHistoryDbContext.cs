using Microsoft.EntityFrameworkCore;
using PD.ChatHistory.Domain.Entities.Chatroom;
using PD.ChatHistory.Domain.Entities.ChatroomEvent;
using PD.ChatHistory.Domain.Entities.User;

namespace PD.ChatHistory.Infrastructure.Contexts
{
    public class ChatHistoryDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ChatRoomDb");
        }

        public DbSet<ChatRoom> ChatRooms { get; set; }

        public DbSet<ChatRoomEvent> ChatRoomEvents { get; set; }

        public DbSet<ChatUser> ChatUsers { get; set; }
    }
}
