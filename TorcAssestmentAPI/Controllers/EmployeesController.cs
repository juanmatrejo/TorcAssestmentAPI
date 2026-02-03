using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TorcAssestmentAPI.Models;
using TorcAssestmentAPI.Services;

namespace TorcAssestmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        public readonly IEmployeeService _employeeService;
        private readonly IValidator<EmployeeDto> _validator;

        public EmployeesController(IEmployeeService employeeService, IValidator<EmployeeDto> validator)
        {
            _employeeService = employeeService;
            _validator = validator;
        }

        [HttpPost("/create")]
        public async Task<IActionResult> Create([FromBody] EmployeeDto dto)
        {
            var result = await _validator.ValidateAsync(dto);

            if (!result.IsValid)
            { return BadRequest(new { Message = result.Errors }); }

            await _employeeService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpGet("/getall")]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAll();
            return Ok(employees);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetById(id);
            if (employee == null)
            { return NotFound(); }

            return Ok(employee);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeDto dto)
        {
            var result = await _validator.ValidateAsync(dto);

            if (!result.IsValid)
            { return BadRequest(new { Message = result.Errors }); }

            var existingEmployee = await _employeeService.GetById(id);
            if (existingEmployee == null)
            { return NotFound(); }

            dto.Id = id;
            await _employeeService.Update(dto);

            return NoContent();
        }

        [HttpDelete("/delete")]
        public async Task<IActionResult> Delete(List<int> ids)
        {
            List<int> notFound = await _employeeService.Delete(ids);

            if (notFound.Count > 0)
            { return Ok(new { Message = "The following IDs were not found: ", IDs = notFound }); }

            return NoContent();
        }
    }
}
