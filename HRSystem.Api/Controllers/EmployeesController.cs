using HRSystem.Api.Commands;
using HRSystem.Api.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;


        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateEmployee")]
        public async Task<ActionResult<int>> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            try
            {
                var employeeId = await _mediator.Send(command);
                return Ok(new { Id = employeeId, Message = "Employee created successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // GET: api/employees
        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await _mediator.Send(new GetAllEmployeesQuery());
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

        [HttpGet("GetEmployeeByid/{id}")]
        public async Task<ActionResult> GetEmployee(int id)
        {
            try
            {
                var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));
                if (employee == null)
                {
                    return NotFound(new { Message = "Employee not found" });
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("UpdateEmployee/{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeCommand command)
        {
            try
            {
                command.Id = id;
                var result = await _mediator.Send(command);

                if (!result)
                    return NotFound(new { Message = "Employee not found" });

                return Ok(new { Message = "Employee updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteEmployeeCommand(id));

                if (!result)
                    return NotFound(new { Message = "Employee not found" });

                return Ok(new { Message = "Employee deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
