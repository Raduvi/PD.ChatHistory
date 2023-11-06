using PD.ChatHistory.Application.DTOs;

namespace PD.ChatHistory.Application.Contracts.Services
{
    public interface IChatRoomHistoryService
    {
        Task<List<ChatRoomDTO>> GetRoomsAsync(CancellationToken cancellationToken);

        Task<ChatRoomHourDTO> GetRoomHourHistoryAsync(int roomId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);

        Task<ChatRoomMinuteDTO> GetRoomMinuteHistoryAsync(int roomId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    }
}
