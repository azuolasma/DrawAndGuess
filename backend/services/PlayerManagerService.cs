using backend.Models;

namespace backend.Services;


public class PlayerManagerService
{
    private readonly List<Player> _players = new();

    public Player GetPlayerById(int id)
    {
        foreach (var player in _players)
        {
            if (player.Id == id)
            {
                return player;
            }
        }

        throw new KeyNotFoundException($"Player with id {id} was not found in the player list _players."); // is there a point to check this?
    }

    public void AddPlayer(Player player)
    {
        if (player is null) // is there even a point in this check?
        {
            throw new ArgumentNullException("Cannot add a null player to the player list _players.");
        }

        _players.Add(player);
    }
}