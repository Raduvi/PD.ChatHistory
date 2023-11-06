using PD.ChatHistory.Application.DTOs;

namespace PD.ChatHistory.Application.Contracts.Services
{
    public interface IChatRoomHistoryService
    {
        Task<List<ChatRoomDTO>> GetRoomsAsync(CancellationToken cancellationToken);

        Task<ChatRoomDTO> GetRoomHistoryAsync(int roomId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    }
}
