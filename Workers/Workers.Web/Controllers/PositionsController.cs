using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using Worker.BusinessLogic.DTO;
using Worker.BusinessLogic.Interfaces;
using Worker.BusinessLogic.Services;

namespace Workers.Web.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class PositionsController : Controller
{
    private readonly ILogger<PositionsController> _logger;
    private readonly IPositionService _positionService;

    public PositionsController(
        ILogger<PositionsController> logger,
        IPositionService positionService
    )
    {
        _logger = logger;
        _positionService = positionService;
    }

    /// <summary>
    /// Get All Positions
    /// </summary>
    /// <returns>
    /// <response code="200">Positions was recieved</response>
    /// <response code="400">Positions has missing/invalid values</response>
    /// <response code="500">Oops! Can't get your Positions right now</response>
    /// </returns>

    [HttpGet("AllPositions")]
    [ProducesResponseType(typeof(List<PositionDTO>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAllPositions()
    {
        var positions = await _positionService.GetAllPositionsAsync();

        return Ok(positions);
    }

    /// <summary>
    ///  Adding new Position
    /// </summary>
    /// <param name="PositionDTO"></param>
    /// <returns>
    /// <response code="200">Position created</response>
    /// <response code="400">Position has missing/invalid values</response>
    /// <response code="500">Oops! Can't create your Position right now</response>
    /// </returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreatePosition(PositionDTO positionDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid model");
        }

        var position = await _positionService.CreatePositionAsync(positionDTO);

        if(position == null)
        {
            string message = "Position wasn't created";
            _logger.LogWarning(message);

            return BadRequest(message);
        }

        return Ok(position.Id);
    }

    /// <summary>
    /// Get Position
    /// </summary>
    /// <param name="positionId"></param>
    /// <returns>
    /// <response code="200">Position was recieved</response>
    /// <response code="400">Position has missing/invalid values</response>
    /// <response code="500">Oops! Can't get your Position right now</response>
    /// </returns>
    [HttpGet("PositionId")]
    [ProducesResponseType(typeof(PositionDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetPosition(Guid positionId)
    {
        var position = await _positionService.GetPositionAsync(positionId);

        if (position == null)
        {
            string message = "Position wasn't recieved";
            _logger.LogWarning(message);

            return BadRequest(message);
        }

        return Ok(position);
    }

    /// <summary>
    /// Get Position By Name
    /// </summary>
    /// <param name="positionName"></param>
    /// <returns>
    /// <response code="200">Position was recieved</response>
    /// <response code="400">Position has missing/invalid values</response>
    /// <response code="500">Oops! Can't get your Position right now</response>
    /// </returns>
    [HttpGet("PositionName")]
    [ProducesResponseType(typeof(PositionDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetPosition(string positionName)
    {
        var position = await _positionService.GetPositionByNameAsync(positionName);

        if (position == null)
        {
            string message = "Position wasn't recieved";
            _logger.LogWarning(message);

            return BadRequest(message);
        }

        return Ok(position);
    }

    /// <summary>
    /// Update Position
    /// </summary>
    /// <param name="PositionDTO"></param>
    /// <returns>
    /// <response code="200">Position updated</response>
    /// <response code="400">Position has missing/invalid values</response>
    /// <response code="500">Oops! Can't update your Position right now</response>
    /// </returns>
    [HttpPut]
    [ProducesResponseType(typeof(PositionDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdatePosition(PositionDTO PositionDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid model");
        }

        var position = await _positionService.UpdatePositionAsync(PositionDTO);

        if (position == null)
        {
            string message = "Position wasn't updated";
            _logger.LogWarning(message);

            return NotFound(message);
        }

        return Ok(position);
    }

    /// <summary>
    /// Delete Position
    /// </summary>
    /// <param name="id"></param>
    /// <returns>
    /// <response code="200">Position was deleted</response>
    /// <response code="400">Position has missing/invalid values</response>
    /// <response code="500">Oops! Can't delete your Position right now</response>
    /// </returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(string),(int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeletePosition(Guid id)
    {
        bool result = await _positionService.DeletePositionAsync(id);

        if (!result)
        {
            string message = "Position wasn't deleted";
            _logger.LogWarning($"{message}");

            return NotFound(message);
        }

        return Ok("Position was deleted.");
    }
}
