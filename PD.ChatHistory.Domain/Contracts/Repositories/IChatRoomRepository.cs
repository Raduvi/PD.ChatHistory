using PD.ChatHistory.Domain.Entities.Chatroom;

namespace PD.ChatHistory.Domain.Contracts.Repositories
{
    public interface IChatRoomRepository
    {
        Task<ChatRoom> GetAsync(int roomId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);

        Task<List<ChatRoom>> GetAllAsync(CancellationToken cancellationToken);
    }
}
