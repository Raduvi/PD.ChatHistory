using AutoMapper;
using PD.ChatHistory.Application.Contracts.Services;
using PD.ChatHistory.Application.DTOs;
using PD.ChatHistory.Domain.Contracts.Repositories;
using PD.ChatHistory.Domain.Entities.Chatroom;

namespace PD.ChatHistory.Application.Services
{
    public class ChatRoomHistoryService : IChatRoomHistoryService
    {
        private readonly IChatRoomRepository _chatRoomRepo;
        private readonly IMapper _mapper;

        public ChatRoomHistoryService(IChatRoomRepository chatRoomRepo, IMapper mapper)
        {
            _chatRoomRepo = chatRoomRepo;
            _mapper = mapper;
        }

        public async Task<ChatRoomHourDTO> GetRoomHourHistoryAsync(int roomId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            ChatRoom room = await _chatRoomRepo.GetAsync(roomId, startDate, endDate, cancellationToken);

            return _mapper.Map<ChatRoomHourDTO>(room);
        }

        public async Task<ChatRoomMinuteDTO> GetRoomMinuteHistoryAsync(int roomId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            ChatRoom room = await _chatRoomRepo.GetAsync(roomId, startDate, endDate, cancellationToken);

            return _mapper.Map<ChatRoomMinuteDTO>(room);
        }

        public async Task<List<ChatRoomDTO>> GetRoomsAsync(CancellationToken cancellationToken = default)
        {
            var rooms = await _chatRoomRepo.GetAllAsync(cancellationToken);

            return _mapper.Map<List<ChatRoomDTO>>(rooms);
        }
    }
}
