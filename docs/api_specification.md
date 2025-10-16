# API contract
Currently defined endpoints:
- `GET /api/rooms`
- `POST /api/rooms`
- `POST /api/rooms/{id}/join`
- `GET /api/rooms/{id}`
- `POST /api/players`

NOTE: I tried my best to make these as future proof as possible, so that we wouldn't need to redesign logic mid development, but it's hard and 99% sure we will need to redesign certain stuff as we develop it. Also this is only for 1st step in website_flow.md


## `GET /api/rooms`

**Description:**
Returns a list of all game rooms (used for displaying the list of rooms in root screen).

**Parameters:**
No parameters.

**Request body example:**
No request body.

**Response example:**
```json
[
  {
    "id": 1,
    "name": "Alex's Room",
    "currentPlayers": 3,
    "playerLimit": 8,
    "currentRound": 2,
    "roundLimit": 5,
    "gameState": "WaitingForPlayers"
  },
  {
    "id": 2,
    "name": "Public Room 2",
    "currentPlayers": 5,
    "playerLimit": 10,
    "currentRound": 1,
    "roundLimit": 5,
    "gameState": "InProgress"
  }
]
```

**Status codes:**
- 200 OK — Success


## `POST /api/rooms`

**Description:**
Creates a new game room (request data is used to create new GameRoomSettings and GameRoomState objects, response data should be used to display the newly created room alongside already existing rooms).

**Parameters:**
No parameters.

**Request body example:**
```json
{
  "hostId": "21312",
  "name": "Alex's Room",
  "playerLimit": 8,
  "roundLimit": 5
}
```

**Response example:**
```json
{
  "id": 1,
  "name": "Alex's Room",
  "currentPlayers": 0,
  "playerLimit": 8,
  "currentRound": 2,
  "roundLimit": 5,
  "gameState": "WaitingForPlayers"
}
```

**Status codes:**
- 201 Created — Room successfully created
- 400 Bad Request — Invalid input


## `POST /api/rooms/{id}/join`

**Description:**
Joins an existing game room by ID.

**Path parameters:**
- `id` (int) — ID of the room to join.

**Request body example:**
```json
{
  "playerId": 31321
}
```

**Response example:**
```json
{
    "id": 1,
    "playerId": 31321
}
```
or no response, both can work, but echoing doesn't do any harm imo.

**Status codes:**
- 200 OK — Joined successfully
- 404 Not Found — Room does not exist
- 409 Conflict — Room is full


## `GET /api/rooms/{id}`

**Description:**
Retrieves current room information (used for refreshing room data).

**Path parameters:**
- `id` (int) — ID of the room.

**Request body example:**
No request body.

**Response example:**
```json
{
  "id": 1,
  "name": "Alex's Room",
  "currentPlayers": 4,
  "playerLimit": 8,
  "currentRound": 1,
  "roundLimit": 5,
  "gameState": "WaitingForPlayers"
}
```

## `POST /api/players`

**Description:**
Creates a new player object and returns its generated ID. This endpoint is used when a user enters their display name before joining or creating a room.

**Parameters:**
No parameters.

**Request body example:**
```json
{
  "name": "Alex"
}
```

**Response example:**
```json
{
  "id": 31321,
  "name": "Alex"
}
```

**Status codes:**
- 201 Created — Player successfully created
- 400 Bad Request — Invalid input


# Realtime events contract

TODO
