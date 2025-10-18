using Microsoft.AspNetCore.Mvc;
using backend.DTOs;
using backend.Models;
using backend.Services;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly IdGeneratorService _idGenerator;
    private readonly PlayerManagerService _playerManager;

    // Constructor injection
    public PlayersController(IdGeneratorService idGenerator, PlayerManagerService playerManager)
    {
        _idGenerator = idGenerator;
        _playerManager = playerManager;
    }


    [HttpPost] // POST domain.com/api/players
    public ActionResult<PlayersCreateResponse> CreatePlayer([FromBody] PlayersCreateRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Player name is null, empty, or whitespace.");
        }

        var playerId = _idGenerator.GenerateRandomId(IdType.PlayerId);
        var player = new Player(playerId, request.Name);

        _playerManager.AddPlayer(player);

        var response = new PlayersCreateResponse(player.Id, player.Name);
        return Created($"/api/players/{player.Id}", response);
    }

}