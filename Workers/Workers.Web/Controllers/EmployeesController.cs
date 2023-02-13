using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using Worker.BusinessLogic.DTO;
using Worker.BusinessLogic.Interfaces;

namespace Workers.Web.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class EmployeesController : Controller
{
    private readonly ILogger<EmployeesController> _logger;
    private readonly IEmployeeService _employeeService;

    public EmployeesController(
        ILogger<EmployeesController> logger,
        IEmployeeService employeeService
    )
    {
        _logger = logger;
        _employeeService = employeeService;
    }

    /// <summary>
    /// Get All Employees
    /// </summary>
    /// <returns>
    /// <response code="200">Employee was recieved</response>
    /// <response code="400">Employee has missing/invalid values</response>
    /// <response code="500">Oops! Can't get your employee right now</response>
    /// </returns>

    [HttpGet("AllEmployees")]
    [ProducesResponseType(typeof(List<EmployeeDTO>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAllEmployees()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();

        return Ok(employees);
    }

    /// <summary>
    ///  Adding new Employee
    /// </summary>
    /// <param name="employeeDTO"></param>
    /// <returns>
    /// <response code="200">Employee created</response>
    /// <response code="400">Employee has missing/invalid values</response>
    /// <response code="500">Oops! Can't create your employee right now</response>
    /// </returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateEmployee(EmployeeDTO employeeDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var employee = await _employeeService.CreateEmployeeAsync(employeeDTO);

        return Ok(employee.Id);
    }

    /// <summary>
    /// GetEmployee
    /// </summary>
    /// <param name="employeeId"></param>
    /// <returns>
    /// <response code="200">Employee was recieved</response>
    /// <response code="400">Employee has missing/invalid values</response>
    /// <response code="500">Oops! Can't get your employee right now</response>
    /// </returns>
    [HttpGet("EmployeeId")]
    [ProducesResponseType(typeof(EmployeeDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetEmployee(Guid employeeId)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);

        if (employee == null)
        {
            return BadRequest();
        }


        return Ok(employee);
    }

    /// <summary>
    /// UpdateEmployee
    /// </summary>
    /// <param name="employeeDTO"></param>
    /// <returns>
    /// <response code="200">Employee updated</response>
    /// <response code="400">Employee has missing/invalid values</response>
    /// <response code="500">Oops! Can't update your employee right now</response>
    /// </returns>
    [HttpPut]
    [ProducesResponseType(typeof(EmployeeDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdateEmployee(EmployeeDTO employeeDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var employee = await _employeeService.UpdateEmployeeAsync(employeeDTO);

        if(employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    /// <summary>
    /// Delete employee
    /// </summary>
    /// <param name="id"></param>
    /// <returns>
    /// <response code="200">Employee was deleted</response>
    /// <response code="400">Employee has missing/invalid values</response>
    /// <response code="500">Oops! Can't delete your employee right now</response>
    /// </returns>
    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string),(int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        bool result = await _employeeService.DeleteEmployeeAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return Ok();
    }
}
