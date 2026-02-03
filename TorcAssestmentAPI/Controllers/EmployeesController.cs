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

        // Dependency Injection for Employee Service and Validator
        public readonly IEmployeeService _employeeService;
        private readonly IValidator<EmployeeDto> _validator;

        // Constructor
        public EmployeesController(IEmployeeService employeeService, IValidator<EmployeeDto> validator)
        {
            _employeeService = employeeService;
            _validator = validator;
        }

        // Create Employee
        [HttpPost("/create")]
        public async Task<IActionResult> Create([FromBody] EmployeeDto dto)
        {
            // Validate the incoming DTO
            var result = await _validator.ValidateAsync(dto);

            // If validation fails, return BadRequest with errors
            if (!result.IsValid)
            { return BadRequest(new { Message = result.Errors }); }

            // Create the employee
            await _employeeService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpGet("/getall")]
        public async Task<IActionResult> GetAll()
        {
            // Retrieve all employees
            var employees = await _employeeService.GetAll();
            return Ok(employees);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Retrieve employee by ID
            var employee = await _employeeService.GetById(id);

            // If not found, return NotFound
            if (employee == null)
            { return NotFound(); }

            // Return the found employee
            return Ok(employee);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeDto dto)
        {
            // Validate the incoming DTO
            var result = await _validator.ValidateAsync(dto);

            // If validation fails, return BadRequest with errors
            if (!result.IsValid)
            { return BadRequest(new { Message = result.Errors }); }

            // Check if the employee exists
            var existingEmployee = await _employeeService.GetById(id);
            if (existingEmployee == null)
            { return NotFound(); }

            // Update the employee
            dto.Id = id;
            await _employeeService.Update(dto);

            // Return NoContent to indicate successful update
            return NoContent();
        }

        [HttpDelete("/delete")]
        public async Task<IActionResult> Delete(List<int> ids)
        {
            // Delete employees by IDs
            List<int> notFound = await _employeeService.Delete(ids);

            // If any IDs were not found, return them in the response
            if (notFound.Count > 0)
            { return Ok(new { Message = "The following IDs were not found: ", IDs = notFound }); }

            // Return NoContent to indicate successful deletion
            return NoContent();
        }
    }
}
