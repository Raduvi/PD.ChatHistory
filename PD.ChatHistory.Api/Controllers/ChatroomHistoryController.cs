using Microsoft.AspNetCore.Mvc;
using PD.ChatHistory.Application.Contracts.Services;
using PD.ChatHistory.Application.DTOs;

namespace PD.ChatHistory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatroomHistoryController : ControllerBase
    {
        private readonly ILogger<ChatroomHistoryController> _logger;
        private readonly IChatRoomHistoryService _chatRoomHistoryService;

        public ChatroomHistoryController(ILogger<ChatroomHistoryController> logger, IChatRoomHistoryService chatRoomHistoryService)
        {
            _logger = logger;
            _chatRoomHistoryService = chatRoomHistoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ChatRoomDTO>>> GetAll(CancellationToken cancellationToken)
        {
            var history = await _chatRoomHistoryService.GetRoomsAsync(cancellationToken);
            return Ok(history);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChatRoomDTO>> Get(int id, [FromQuery] DateTime day, CancellationToken cancellationToken)
        {
            var history = await _chatRoomHistoryService.GetRoomHistoryAsync(id, day, cancellationToken);

            if (string.IsNullOrEmpty(history.Name))
            {
                return NotFound();
            }

            return Ok(history);
        }
    }
}