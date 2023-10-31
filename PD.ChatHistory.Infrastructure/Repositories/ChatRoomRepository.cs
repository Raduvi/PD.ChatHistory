using Microsoft.EntityFrameworkCore;
using PD.ChatHistory.Domain.Contracts.Repositories;
using PD.ChatHistory.Domain.Entities.Chatroom;
using PD.ChatHistory.Infrastructure.Contexts;

namespace PD.ChatHistory.Infrastructure.Repositories
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        private readonly ChatHistoryDbContext _context;

        public ChatRoomRepository(ChatHistoryDbContext context)
        {
            _context = context;
        }

        public async Task<ChatRoom> Get(int roomId, DateTime day, CancellationToken cancellationToken)
        {
            var room = await _context.ChatRooms
                           .Where(r => r.Id == roomId)
                           .Include(r => r.Events)
                           .ThenInclude(r => r.ChatUser)
                           .Include(r => r.Events
                                .Where(e => e.CreatedOnUTC.ToShortDateString() == day.ToShortDateString())
                                .OrderByDescending(e => e.CreatedOnUTC))
                           .ThenInclude(r => r.HighFivedChatUser)
                           .FirstOrDefaultAsync();

            if (room == null) return new ChatRoom() { };

            return room;
        }

        public async Task<List<ChatRoom>> GetAll(CancellationToken cancellationToken)
        {
            var rooms = await _context.ChatRooms
                         .ToListAsync();

            return rooms;
        }
    }
}
