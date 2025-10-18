/*
all getters and setters are automatically provided by c#, id and name
initialized upon creation of the object, roomid should be managed by something
external i.e. when a player joins a room this gets set to the room id and when
the player leaves it gets set back to null, same for score
*/
namespace backend.Models;

public class Player
{
    public int Id { get; }
    public string Name { get; }

    public int? RoomId { get; set; }
    public int? Score { get; set; } // set to 0 when joins a room, until then it should be null


    public Player(int id, string name)
    {
        // enforce invariants
        Id = id;
        Name = name;
    }

    public void AddScore(int points)
    {
        if (!Score.HasValue)
        {
            throw new InvalidOperationException("Error: Score wasn't initialized before calling AddScore()");
        }
        Score += points;
    }
}

