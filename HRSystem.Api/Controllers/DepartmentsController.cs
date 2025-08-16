using HRSystem.Api.Commands;
using HRSystem.Api.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/departments
        [HttpGet("GetAllDepartments")]
        public async Task<ActionResult> GetAllDepartments()
        {
            try
            {
                var departments = await _mediator.Send(new GetAllDepartmentsQuery());
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // POST: api/departments
        [HttpPost("CreateDepartment")]
        public async Task<ActionResult> CreateDepartment([FromBody] CreateDepartmentCommand command)
        {
            try
            {
                var departmentId = await _mediator.Send(command);
                return Ok(new { Id = departmentId, Message = "Department created successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
