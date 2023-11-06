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
        public async Task<ActionResult<List<ChatRoomDTO>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var history = await _chatRoomHistoryService.GetRoomsAsync(cancellationToken);
            return Ok(history);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChatRoomDTO>> GetAsync(int id, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, CancellationToken cancellationToken)
        {
            if (startDate > endDate)
            {
                return BadRequest("startDate cannot be greater than startDate");
            }

            var history = await _chatRoomHistoryService.GetRoomHistoryAsync(id, startDate, endDate, cancellationToken);

            if (string.IsNullOrEmpty(history.Name))
            {
                return NotFound();
            }

            return Ok(history);
        }
    }
}