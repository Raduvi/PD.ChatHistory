using AutoMapper;
using Moq;
using PD.ChatHistory.Application.DTOs;
using PD.ChatHistory.Application.Services;
using PD.ChatHistory.Domain.Contracts.Repositories;
using PD.ChatHistory.Domain.Entities.Chatroom;

namespace PD.ChatHistory.Application.UnitTests
{
    public class ChatRoomHistoryServiceTests
    {
        [Fact]
        public async Task GetRoomMinuteHistoryAsync_ReturnsChatRoomMinuteDTO()
        {
            // Arrange
            var roomId = 1;
            var startDate = DateTime.Now.AddHours(-1);
            var endDate = DateTime.Now;
            var cancellationToken = new CancellationTokenSource().Token;

            var mockChatRoomRepo = new Mock<IChatRoomRepository>();
            var mockMapper = new Mock<IMapper>();

            var expectedChatRoom = new ChatRoom();
            var expectedChatRoomMinuteDTO = new ChatRoomMinuteDTO();

            mockChatRoomRepo.Setup(repo => repo.GetAsync(roomId, startDate, endDate, cancellationToken))
                .ReturnsAsync(expectedChatRoom);

            mockMapper.Setup(mapper => mapper.Map<ChatRoomMinuteDTO>(expectedChatRoom))
                .Returns(expectedChatRoomMinuteDTO);

            var sut = new ChatRoomHistoryService(mockChatRoomRepo.Object, mockMapper.Object);

            // Act
            var result = await sut.GetRoomMinuteHistoryAsync(roomId, startDate, endDate, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ChatRoomMinuteDTO>(result);
            Assert.Same(expectedChatRoomMinuteDTO, result);
        }
    }
}