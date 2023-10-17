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

        #region GetAll

        [Fact]
        public void GetAll_WhenIsValid_ShouldReturnOkEmptyListOfRooms()
        {
            // Arrange
            var _chatRoomsFromService = new List<ChatRoomDTO>() { };

            _mockService
                .Setup(r => r.GetRoomsAsync(CancellationToken.None))
                .ReturnsAsync(_chatRoomsFromService);

            var controller = new ChatroomHistoryController(_mockLogger.Object, _mockService.Object);

            // Act
            var taskResult = controller.GetAll(CancellationToken.None);

            // Assert            
            Assert.IsType<OkObjectResult>(taskResult.Result.Result);
            var okResult = Assert.IsType<OkObjectResult>(taskResult.Result.Result);
            var resultList = Assert.IsAssignableFrom<List<ChatRoomDTO>>(okResult.Value);
            Assert.Empty(resultList);
        }

        [Fact]
        public void GetAll_WhenIsValid_ShouldReturnOkListOfRooms()
        {
            // Arrange
            var _chatRoomsFromService = new List<ChatRoomDTO>() { Constants.chatRoomDTO };

            _mockService
                .Setup(r => r.GetRoomsAsync(CancellationToken.None))
                .ReturnsAsync(_chatRoomsFromService);

            var controller = new ChatroomHistoryController(_mockLogger.Object, _mockService.Object);

            // Act
            var taskResult = controller.GetAll(CancellationToken.None);

            // Assert            
            Assert.IsType<OkObjectResult>(taskResult.Result.Result);
            var okResult = Assert.IsType<OkObjectResult>(taskResult.Result.Result);
            var resultList = Assert.IsAssignableFrom<List<ChatRoomDTO>>(okResult.Value);
            Assert.NotEmpty(resultList);
            Assert.Equal(_chatRoomsFromService, resultList);
        }

        #endregion

        #region Get

        [Fact]
        public void Get_WhenIdDoesNotExist_ShouldReturnNotFound()
        {
            // Arrange
            var _chatRoomFromService = new ChatRoomDTO();

            _mockService
                .Setup(r => r.GetRoomHistoryAsync(Constants.NonExistingRoomId, Constants.DefaultDatetime, CancellationToken.None))
                .ReturnsAsync(_chatRoomFromService);

            var controller = new ChatroomHistoryController(_mockLogger.Object, _mockService.Object);

            // Act
            var taskResult = controller.Get(Constants.NonExistingRoomId, Constants.DefaultDatetime, CancellationToken.None);

            // Assert            
            Assert.IsType<NotFoundResult>(taskResult.Result.Result);
        }

        [Fact]
        public void Get_WhenValid_ShouldReturnChatRoomDTO()
        {
            // Arrange
            var _chatRoomFromService = Constants.chatRoomDTO;

            _mockService
                .Setup(r => r.GetRoomHistoryAsync(Constants.ValidRoomId, Constants.DefaultDatetime, CancellationToken.None))
                .ReturnsAsync(_chatRoomFromService);

            var controller = new ChatroomHistoryController(_mockLogger.Object, _mockService.Object);

            // Act
            var taskResult = controller.Get(Constants.ValidRoomId, Constants.DefaultDatetime, CancellationToken.None);

            // Assert            
            Assert.IsType<OkObjectResult>(taskResult.Result.Result);
            var okResult = Assert.IsType<OkObjectResult>(taskResult.Result.Result);
            var resultRoom = Assert.IsAssignableFrom<ChatRoomDTO>(okResult.Value);
            Assert.Equal(Constants.RoomName, resultRoom.Name);
        }

        [Fact]
        public void Get_WhenError_ShouldReturnException()
        {
            // Arrange
            var _chatRoomFromService = Constants.chatRoomDTO;

            _mockService
                .Setup(r => r.GetRoomHistoryAsync(Constants.ValidRoomId, Constants.DefaultDatetime, CancellationToken.None))
                .Throws(new Exception(Constants.TestExceptionMessage));

            var controller = new ChatroomHistoryController(_mockLogger.Object, _mockService.Object);

            // Act
            var taskResult = controller.Get(Constants.ValidRoomId, Constants.DefaultDatetime, CancellationToken.None);

            // Assert            
            Assert.NotNull(taskResult.Exception);
            Assert.Equal($"One or more errors occurred. ({Constants.TestExceptionMessage})", taskResult.Exception.Message);
        }

        #endregion

    }
}