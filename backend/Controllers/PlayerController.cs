using Microsoft.AspNetCore.Mvc;
using DrawandGuess.Models;
using System.Collections.Generic;
using System.Linq;
using DrawandGuess.Requests;
using System;

namespace DrawAndGuess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private static List<Player> _players = new List<Player>();

        [HttpGet("list")]
        public ActionResult<List<Player>> GetPlayers()
        {
            if (!_players.Any())
            {
                return NotFound("No players yet. Add one using /api/player/add?name=John");
            }
            return Ok(_players);
        }

        [HttpPost("add")]
        public ActionResult<Player> AddPlayer([FromQuery] string name = "Anonymous")
        {
            if (_players.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                return Conflict("Player with this name already exists.");
            }

            var player = new Player(name);
            _players.Add(player);
            return Ok(player);
        }

        [HttpPost("setDrawer")]
        public ActionResult SetDrawer([FromBody] DrawerRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return BadRequest("Name is required.");

            var player = _players.FirstOrDefault(p =>
                p.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));
            if (player == null)
                return NotFound("Player not found.");

            foreach (var p in _players)
                p.Role = PlayerRole.Guesser;

            player.Role = PlayerRole.Drawer;

            return Ok($"{player.Name} is now the drawer!");
        }

        [HttpGet("sorted")]
        public ActionResult<List<Player>> GetSortedScores()
        {
            if (!_players.Any())
            {
                return NotFound("No players yet. Add one using /api/player/add?name=John");
            }

            var sortedPlayers = _players.OrderByDescending(p => p.Score).ToList();
            return Ok(sortedPlayers);
        }

        [HttpPost("addScore")]
        public ActionResult AddScore([FromQuery] string name, [FromQuery] int points = 10)
        {
            var player = _players.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (player == null)
                return NotFound("Player not found.");

            player.Score += points;
            return Ok($"{player.Name} now has {player.Score} points.");
        }
    }
}
