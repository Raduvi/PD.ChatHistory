using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PD.ChatHistory.Api.Controllers;
using PD.ChatHistory.Api.UnitTests.TestUtils.Constants;
using PD.ChatHistory.Application.Contracts.Services;
using PD.ChatHistory.Application.DTOs;

namespace PD.ChatHistory.Api.UnitTests
{
    public class ChatroomHistoryControllerTests
    {
        Mock<IChatRoomHistoryService> _mockService = new Mock<IChatRoomHistoryService>();
        Mock<ILogger<ChatroomHistoryController>> _mockLogger = new Mock<ILogger<ChatroomHistoryController>>();

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_WhenIsValid_ShouldReturnOkEmptyListOfRooms()
        {
            // Arrange
            var _chatRoomsFromService = new List<ChatRoomDTO>() { };

            _mockService
                .Setup(r => r.GetRoomsAsync(CancellationToken.None))
                .ReturnsAsync(_chatRoomsFromService);

            var controller = new ChatroomHistoryController(_mockLogger.Object, _mockService.Object);

            // Act
            var taskResult = await controller.GetAllAsync(CancellationToken.None);

            // Assert            
            Assert.IsType<OkObjectResult>(taskResult.Result);
            var okResult = Assert.IsType<OkObjectResult>(taskResult.Result);
            var resultList = Assert.IsAssignableFrom<List<ChatRoomDTO>>(okResult.Value);
            Assert.Empty(resultList);
        }

        [Fact]
        public async Task GetAll_WhenIsValid_ShouldReturnOkListOfRooms()
        {
            // Arrange
            var _chatRoomsFromService = new List<ChatRoomDTO>() { Constants.chatRoomDTO };

            _mockService
                .Setup(r => r.GetRoomsAsync(CancellationToken.None))
                .ReturnsAsync(_chatRoomsFromService);

            var controller = new ChatroomHistoryController(_mockLogger.Object, _mockService.Object);

            // Act
            var taskResult = await controller.GetAllAsync(CancellationToken.None);

            // Assert            
            Assert.IsType<OkObjectResult>(taskResult.Result);
            var okResult = Assert.IsType<OkObjectResult>(taskResult.Result);
            var resultList = Assert.IsAssignableFrom<List<ChatRoomDTO>>(okResult.Value);
            Assert.NotEmpty(resultList);
            Assert.Equal(_chatRoomsFromService, resultList);
        }

        #endregion

        #region GetMinutesAsync

        [Fact]
        public async Task Get_WhenValid_ShouldReturnChatRoomDTO()
        {
            // Arrange
            var _chatRoomFromService = Constants.chatRoomMinuteDTO;

            _mockService
                .Setup(r => r.GetRoomMinuteHistoryAsync(Constants.ValidRoomId, Constants.DefaultDatetime, Constants.DefaultDatetime, CancellationToken.None))
                .ReturnsAsync(_chatRoomFromService);

            var controller = new ChatroomHistoryController(_mockLogger.Object, _mockService.Object);

            // Act
            var taskResult = await controller.GetByMinutesAsync(Constants.ValidRoomId, Constants.DefaultDatetime, Constants.DefaultDatetime, CancellationToken.None);

            // Assert            
            Assert.IsType<OkObjectResult>(taskResult.Result);
            var okResult = Assert.IsType<OkObjectResult>(taskResult.Result);
            var resultRoom = Assert.IsAssignableFrom<ChatRoomMinuteDTO>(okResult.Value);
            Assert.NotEmpty(resultRoom.MinutelyView);
        }

        [Fact]
        public void Get_WhenError_ShouldReturnException()
        {
            // Arrange
            var _chatRoomFromService = Constants.chatRoomDTO;

            _mockService
                .Setup(r => r.GetRoomMinuteHistoryAsync(Constants.ValidRoomId, Constants.DefaultDatetime, Constants.DefaultDatetime, CancellationToken.None))
                .Throws(new Exception(Constants.TestExceptionMessage));

            var controller = new ChatroomHistoryController(_mockLogger.Object, _mockService.Object);

            // Act
            var taskResult = controller.GetByMinutesAsync(Constants.ValidRoomId, Constants.DefaultDatetime, Constants.DefaultDatetime, CancellationToken.None);

            // Assert            
            Assert.NotNull(taskResult.Exception);
            Assert.Equal($"One or more errors occurred. ({Constants.TestExceptionMessage})", taskResult.Exception.Message);
        }

        #endregion

    }
}