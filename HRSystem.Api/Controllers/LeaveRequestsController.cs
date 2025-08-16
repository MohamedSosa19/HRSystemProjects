using HRSystem.Api.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/leaverequests
        [HttpPost("CreateLeaveRequest")]
        public async Task<ActionResult> CreateLeaveRequest([FromBody] CreateLeaveRequestCommand command)
        {
            try
            {
                var leaveRequestId = await _mediator.Send(command);
                return Ok(new { Id = leaveRequestId, Message = "Leave request submitted successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // PUT: api/leaverequests/approve
        [HttpPut("approve")]
        public async Task<ActionResult> ApproveLeaveRequest([FromBody] ApproveLeaveRequestCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                if (!result)
                    return NotFound(new { Message = "Leave request not found or already processed" });

                var message = command.IsApproved ? "Leave request approved successfully" : "Leave request rejected";
                return Ok(new { Message = message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
