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

        public async Task<ChatRoom> GetAsync(int roomId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            try
            {
                var room = await _context.ChatRooms
                           .Where(r => r.Id == roomId)
                           .Include(r => r.Events
                                .Where(e => e.CreatedOnUTC.Date >= startDate.Date && e.CreatedOnUTC.Date <= endDate.Date)
                                .OrderByDescending(e => e.CreatedOnUTC))
                           .ThenInclude(r => r.ChatUser)
                           .Include(r => r.Events)
                           .ThenInclude(r => r.HighFivedChatUser)
                           .FirstOrDefaultAsync(cancellationToken);

                if (room == null) return new ChatRoom() { };

                return room;
            }
            catch (OperationCanceledException ex)
            {
                throw;
            }
        }

        public async Task<List<ChatRoom>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var rooms = await _context.ChatRooms
                         .ToListAsync();

                return rooms;
            }
            catch (OperationCanceledException ex)
            {
                throw;
            }
        }
    }
}
