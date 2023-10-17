using PD.ChatHistory.Domain.Entities.Chatroom;

namespace PD.ChatHistory.Domain.Contracts.Repositories
{
    public interface IChatRoomRepository
    {
        Task<ChatRoom> Get(int roomId, DateTime day, CancellationToken cancellationToken);

        Task<List<ChatRoom>> GetAll(CancellationToken cancellationToken);
    }
}
