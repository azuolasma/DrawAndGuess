using backend.Models;

namespace backend.Services;

public class IdGeneratorService
{
    private readonly HashSet<int> _usedPlayerIds = new();
    private readonly HashSet<int> _usedRoomIds = new();
    private readonly Random _random = new();


    public int GenerateRandomId(IdType type)
    {
        // technically a bit of an unecessary check, but good in case we add more values to the enum IdType
        if (type != IdType.PlayerId && type != IdType.RoomId)
            throw new ArgumentException("Invalid ID type.");

        int newId;

        if (type == IdType.PlayerId)
        {
            do
            {
                newId = _random.Next(10000, 99999); // 5 digit id's
            }
            while (_usedPlayerIds.Contains(newId));
        }
        else
        {
            do
            {
                newId = _random.Next(10000, 99999); //5 digit id's
            }
            while (_usedRoomIds.Contains(newId));
        }


        if (type == IdType.PlayerId)
            _usedPlayerIds.Add(newId);
        else
            _usedRoomIds.Add(newId);

        return newId;
    }
}
