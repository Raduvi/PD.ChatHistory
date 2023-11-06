using Microsoft.Extensions.DependencyInjection;

using PD.ChatHistory.Domain.Entities.Chatroom;
using PD.ChatHistory.Domain.Entities.ChatroomEvent;
using PD.ChatHistory.Domain.Entities.User;
using PD.ChatHistory.Domain.Enums;
using PD.ChatHistory.Infrastructure.Contexts;

namespace PD.ChatHistory.Infrastructure.Seed
{
    public static class DbSeeder
    {
        public static void Seed(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ChatHistoryDbContext>();

                var users = new List<ChatUser>()
                {
                    new ChatUser().Create(
                        "Alex",
                        "alex@mail.com",
                        UserTypes.Admin,
                        1,
                        DateTime.UtcNow,
                        1,
                        DateTime.UtcNow
                    ),
                    new ChatUser().Create(
                        "Bob",
                        "bob@mail.com",
                        UserTypes.User,
                        1,
                        DateTime.UtcNow,
                        1,
                        DateTime.UtcNow
                    ),
                    new ChatUser().Create(
                        "Kate",
                        "kate@mail.com",
                        UserTypes.User,
                        1,
                        DateTime.UtcNow,
                        1,
                        DateTime.UtcNow
                    )
                };

                context.AddRange(users);
                context.SaveChanges();

                var chatrooms = new List<ChatRoom>()
                {
                    new ChatRoom().Create(
                        1,
                        "Test Room 1",
                        "A chatroom for testing.",
                        1,
                        1,
                        DateTime.UtcNow,
                        DateTime.UtcNow
                    ),
                        new ChatRoom().Create(
                        2,
                        "Test Room 2",
                        "A chatroom for testing more stuff.",
                        1,
                        1,
                        DateTime.UtcNow,
                        DateTime.UtcNow
                    )
                };

                context.AddRange(chatrooms);
                context.SaveChanges();

                var defaultChatRoom = context.ChatRooms.Where(r => r.Id == 1).First();
                var chatUserAlex = context.ChatUsers.First(u => u.UserName == "Alex");
                var chatUserBob = context.ChatUsers.First(u => u.UserName == "Bob");
                var chatUserKate = context.ChatUsers.First(u => u.UserName == "Kate");
                var defaultDateTime = DateTime.UtcNow;

                var chatroomEvents = new List<ChatRoomEvent>()
                {
                    new ChatRoomEvent().Create(
                       chatroom: defaultChatRoom,
                       chatUser: chatUserBob,
                       comment: $"{chatUserBob.UserName} enters the room",
                       eventType: EventTypes.Enter,
                       createdOnUTC: defaultDateTime.AddMinutes(0).AddDays(-1),
                       updatedOnUTC: defaultDateTime.AddMinutes(0).AddDays(-1)
                    ),
                     new ChatRoomEvent().Create(
                       chatroom: defaultChatRoom,
                       chatUser: chatUserKate,
                       comment: $"{chatUserKate.UserName} enters the room",
                       eventType: EventTypes.Enter,
                       createdOnUTC: defaultDateTime.AddMinutes(5).AddDays(-1),
                       updatedOnUTC: defaultDateTime.AddMinutes(5).AddDays(-1)
                    ),
                    new ChatRoomEvent().Create(
                       chatroom: defaultChatRoom,
                       chatUser: chatUserBob,
                       comment: $"{chatUserBob.UserName} enters the room",
                       eventType: EventTypes.Enter,
                       createdOnUTC: defaultDateTime.AddMinutes(0),
                       updatedOnUTC: defaultDateTime.AddMinutes(0)
                    ),
                     new ChatRoomEvent().Create(
                       chatroom: defaultChatRoom,
                       chatUser: chatUserKate,
                       comment: $"{chatUserKate.UserName} enters the room",
                       eventType: EventTypes.Enter,
                       createdOnUTC: defaultDateTime.AddMinutes(5),
                       updatedOnUTC: defaultDateTime.AddMinutes(5)
                    ),
                     new ChatRoomEvent().Create(
                       chatroom: defaultChatRoom,
                       chatUser: chatUserBob,
                       comment: $"{chatUserBob.UserName} comments: Hey, Kate - high five?",
                       eventType: EventTypes.Comment,
                       createdOnUTC: defaultDateTime.AddMinutes(15),
                       updatedOnUTC: defaultDateTime.AddMinutes(15)
                    ),
                     new ChatRoomEvent().Create(
                       chatroom: defaultChatRoom,
                       chatUser: chatUserKate,
                       highFivedUser: chatUserBob,
                       comment: $"{chatUserKate.UserName} high-fives {chatUserBob.UserName}",
                       eventType: EventTypes.HighFive,
                       createdOnUTC: defaultDateTime.AddMinutes(17),
                       updatedOnUTC: defaultDateTime.AddMinutes(17)
                    ),
                     new ChatRoomEvent().Create(
                       chatroom: defaultChatRoom,
                       chatUser: chatUserBob,
                       comment: $"{chatUserBob.UserName}  leaves",
                       eventType: EventTypes.Leave,
                       createdOnUTC: defaultDateTime.AddMinutes(18),
                       updatedOnUTC: defaultDateTime.AddMinutes(18)
                    ),
                    new ChatRoomEvent().Create(
                       chatroom: defaultChatRoom,
                       chatUser: chatUserKate,
                       comment: $"{chatUserKate.UserName} comments: Oh, typical",
                       eventType: EventTypes.Comment,
                       createdOnUTC: defaultDateTime.AddMinutes(20),
                       updatedOnUTC: defaultDateTime.AddMinutes(20)
                    ),
                    new ChatRoomEvent().Create(
                       chatroom: defaultChatRoom,
                       chatUser: chatUserKate,
                       comment: $"{chatUserKate.UserName} leaves",
                       eventType: EventTypes.Leave,
                       createdOnUTC: defaultDateTime.AddMinutes(21),
                       updatedOnUTC: defaultDateTime.AddMinutes(21)
                    ),
                    new ChatRoomEvent().Create(
                       chatroom: defaultChatRoom,
                       chatUser: chatUserBob,
                       comment: $"{chatUserBob.UserName} enters the room",
                       eventType: EventTypes.Enter,
                       createdOnUTC: defaultDateTime.AddHours(1).AddMinutes(21),
                       updatedOnUTC: defaultDateTime.AddHours(1).AddMinutes(21)
                    ),
                    new ChatRoomEvent().Create(
                       chatroom: defaultChatRoom,
                       chatUser: chatUserBob,
                       comment: $"{chatUserBob.UserName} comments: Anyone here?",
                       eventType: EventTypes.Comment,
                       createdOnUTC: defaultDateTime.AddHours(1).AddMinutes(25),
                       updatedOnUTC: defaultDateTime.AddHours(1).AddMinutes(25)
                    ),
                    new ChatRoomEvent().Create(
                       chatroom: defaultChatRoom,
                       chatUser: chatUserAlex,
                       comment: $"{chatUserAlex.UserName} enters the room",
                       eventType: EventTypes.Enter,
                       createdOnUTC: defaultDateTime.AddHours(1).AddMinutes(26),
                       updatedOnUTC: defaultDateTime.AddHours(1).AddMinutes(26)
                    ),
                    new ChatRoomEvent().Create(
                       chatroom: defaultChatRoom,
                       chatUser: chatUserKate,
                       comment: $"{chatUserKate.UserName} enters the room",
                       eventType: EventTypes.Enter,
                       createdOnUTC: defaultDateTime.AddHours(1).AddMinutes(27),
                       updatedOnUTC: defaultDateTime.AddHours(1).AddMinutes(27)
                    ),
                     new ChatRoomEvent().Create(
                       chatroom: defaultChatRoom,
                       chatUser: chatUserAlex,
                       highFivedUser: chatUserBob,
                       comment: $"{chatUserAlex.UserName} high-fives {chatUserBob.UserName}",
                       eventType: EventTypes.HighFive,
                       createdOnUTC: defaultDateTime.AddHours(1).AddMinutes(28),
                       updatedOnUTC: defaultDateTime.AddHours(1).AddMinutes(28)
                    ),
                     new ChatRoomEvent().Create(
                       chatroom: defaultChatRoom,
                       chatUser: chatUserAlex,
                       highFivedUser: chatUserKate,
                       comment: $"{chatUserAlex.UserName} high-fives {chatUserKate.UserName}",
                       eventType: EventTypes.HighFive,
                       createdOnUTC: defaultDateTime.AddHours(1).AddMinutes(29),
                       updatedOnUTC: defaultDateTime.AddHours(1).AddMinutes(29)
                    ),
                     new ChatRoomEvent().Create(
                       chatroom: defaultChatRoom,
                       chatUser: chatUserAlex,
                       highFivedUser: chatUserAlex,
                       comment: $"{chatUserAlex.UserName} high-fives {chatUserAlex.UserName}",
                       eventType: EventTypes.HighFive,
                       createdOnUTC: defaultDateTime.AddHours(1).AddMinutes(29),
                       updatedOnUTC: defaultDateTime.AddHours(1).AddMinutes(29)
                    )
                };

                context.AddRange(chatroomEvents);
                context.SaveChanges();
            }
        }
    }
}
