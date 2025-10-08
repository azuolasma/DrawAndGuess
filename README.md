# Online Drawing and Guessing game

## Overview

Online Drawing and Guessing game is a collaborative, real-time web app where players join a lobby, take turns drawing a secret word, and others guess it through chat.
Points are awarded for correct guesses and drawing accuracy.

### Frontend stack
- React (Vite or CRA)
- HTML5 Canvas API
- Tailwind CSS
- @microsoft/signalr (client library)

### Backend stack
- ASP.NET Core 8 Web API
- SignalR for real-time communication
- Entity Framework Core (for database)
- SQL Server

## Project structure

### 1. Front end components
- Components:
    - CanvasBoard - drawing and rendering strokes.
    - ChatBox - guessing word in real time.
    - Leaderboard - shows connected players and their scores.
    - GameControls - start game, word selection, timers.
    - lobby - join/create a room.

### 2. Backend


### Example of project structure:
```
/DrawAndGuess
├── /backend
│   ├── Controllers/
│   │   ├── GameController.cs
│   │   ├── UserController.cs
│   ├── Hubs/
│   │   ├── GameHub.cs
│   ├── Models/
│   │   ├── User.cs
│   │   ├── Room.cs
│   │   ├── GameRound.cs
│   │   ├── Message.cs
│   ├── Services/
│   │   ├── GameService.cs
│   ├── appsettings.json
│   └── Program.cs
│
├── /frontend
│   ├── /src
│   │   ├── components/
│   │   │   ├── CanvasBoard.jsx
│   │   │   ├── ChatBox.jsx
│   │   │   ├── PlayerList.jsx
│   │   │   ├── Scoreboard.jsx
│   │   ├── pages/
│   │   │   ├── Lobby.jsx
│   │   │   ├── GameRoom.jsx
│   │   ├── utils/
│   │   │   ├── signalRConnection.js
│   │   └── App.jsx
│   └── package.json
│
├── .gitignore
├── README.md
└── LICENSE

```
## Getting started

### 📋 Prerequisites

Before running the project, ensure you have:

- Node.js >= v18
- .NET SDK >= 8.0
- SQL Server (or LocalDB)
- Git
### ⚙️ Installation
#### Clone the repo:
```
git clone https://github.com/yourusername/online-drawing-board.git
cd online-drawing-board
```
#### Setup backend:
```
cd backend
dotnet restore
dotnet build
```
#### Setup frontend:
```
cd ../frontend
npm install
```
### Running the App
#### Start backend:
```
dotnet run
```
(default URL: https://localhost:5001)

#### Start frontend:
```
npm start
```
(default URL: http://localhost:3000)

## Database Design for Online Drawing Board

Main entities :

1. Users → Players who join games.

2. Rooms → Game lobbies where players connect.

3. Games → Each round/session of the drawing game.

4. GameRounds → Individual rounds within a game.

5. Messages → Chat messages for guesses and talk.

6. Scores → Points earned per player per game.

### Tables and Columns

### 1. Users
Stores player information.
```sql
Users
- UserId (PK, int, identity)
- Username (nvarchar(50), unique)
- CreatedAt (datetime)
```
### 2. Rooms
Represents a game room (like a lobby).
```sql
Rooms
- RoomId (PK, int, identity)
- RoomCode (nvarchar(10), unique)   -- e.g., "ABCD123"
- CreatedByUserId (FK → Users.UserId)
- CreatedAt (datetime)
- IsActive (bit)                     -- active or closed

```
### 3. RoomPlayers (many-to-many between Rooms & Users)
Tracks which players are in which rooms.
```sql
RoomPlayers
- RoomPlayerId (PK, int, identity)
- RoomId (FK → Rooms.RoomId)
- UserId (FK → Users.UserId)
- JoinedAt (datetime)
- IsHost (bit)                       -- true if host
```
### 4. Games
Each room can have multiple game sessions (best-of-5 rounds, etc.).
```sql
Games
- GameId (PK, int, identity)
- RoomId (FK → Rooms.RoomId)
- StartedAt (datetime)
- EndedAt (datetime, nullable)
```
### 5. GameRounds
Each game has multiple rounds (one per drawing turn).
```sql
GameRounds
- RoundId (PK, int, identity)
- GameId (FK → Games.GameId)
- DrawerUserId (FK → Users.UserId)   -- who is drawing
- Word (nvarchar(100))               -- secret word
- StartedAt (datetime)
- EndedAt (datetime, nullable)
```
### 6. Messages
Stores chat/guess messages in a round.
```sql
Messages
- MessageId (PK, int, identity)
- RoundId (FK → GameRounds.RoundId)
- UserId (FK → Users.UserId)
- MessageText (nvarchar(255))
- SentAt (datetime)
- IsCorrectGuess (bit)               -- true if matched the word
```
### 7. Scores
Tracks points earned by players per game.
```sql
Scores
- ScoreId (PK, int, identity)
- GameId (FK → Games.GameId)
- UserId (FK → Users.UserId)
- Points (int)
```

## Entity relationships

- User ↔ Room = Many-to-Many (via RoomPlayers).

- Room ↔ Game = One-to-Many (a room can host many games).

- Game ↔ Round = One-to-Many (each game has multiple rounds).

- Round ↔ Messages = One-to-Many (chat log of guesses).

- Game ↔ Scores = One-to-Many (each player gets a score).

### How This Works in Gameplay

1. User joins a room → added to RoomPlayers.

2. Game starts in room → new Games row created.

3. Each round starts → GameRounds entry (assign drawer + word).

4. Players chat guesses → stored in Messages, check if IsCorrectGuess=true.

5. Scores updated → drawer + guesser get points (stored in Scores).

6. Game ends → leaderboard from Scores.

## 🔌 Real-Time Communication

The app uses **SignalR** (a real-time communication library built on WebSockets) to keep all players’ screens synchronized instantly.  
Whenever a player draws, chats, or makes a guess, the data is sent to the server, which then broadcasts it to all connected clients.

---

#### 🧭 Client → Server Events

| Event | Payload | Description |
|--------|----------|-------------|
| `draw` | (x, y, color, strokeWidth) | Sent when a player draws on the canvas |
| `chatMessage` | (username, message) | Sent when a player sends a chat message |
| `guess` | (word) | Sent when a player submits a guess |

---

#### ⚡ Server → Client Events

| Event | Payload | Description |
|--------|----------|-------------|
| `updateCanvas` | stroke data | Broadcasts new strokes to all connected players |
| `chatUpdate` | message data | Sends chat updates to all clients |
| `correctGuess` | (username, score) | Notifies all players who guessed correctly and updates scores |
| `gameStateUpdate` | (currentDrawer, timeRemaining, wordLength) | Updates all clients with the current game state |

---

## Branching stategy for this project

### Think of your repo as a tree:

- **main** = the trunk (always stable, production-ready code).

- **dev** = a working branch (integration branch).

- **feature branches** = small branches where each teammate works.

### Branches :

1. **main**

 - Always stable.

- Contains only tested, working code.

- Nobody pushes directly (changes go through PRs).

2. **dev**

- Integration branch.

- Everyone merges their work here first.

- Regularly tested before promoting to main.

3. **feature branches** (one per task)

    - Named after the feature or fix.

    - Examples:

        - feature/drawing-canvas

        - feature/chat-box

        - feature/signalr-setup

        - bugfix/score-calculation

### Your typical workflow

### 1. Update dev:
```
git checkout dev
git pull origin dev
```
(Make sure you’re starting from the latest code.)

### 2. Create feature branch:
```
git checkout -b feature/chat-box
```
### 3. Do your work locally(add commits locally).

### 4. push feature branch:
```
git push origin feature/chat-box
```
### 5. Open a Pull Request (PR):
- Base branch = dev
- Compare branch = feature/chat-box
- Add title + description of what you did.

### 6. Team reviews the PR:
- Teammates read your code.
- Leave comments("Can you rename this?"/ "Bug here").
- Approve when ready.

### 7. Merge PR into dev:
- After approval, merge into dev.
- Delete feature branch when merged.

### 8. Promote to main:
- When dev is stable, merge dev -> main.

## Git Setup

### .gitignore key items:
```bash
# React
frontend/node_modules/
frontend/build/
# .NET
backend/bin/
backend/obj/
backend/.vs/
# Config
.env
```

## License
This project is open-source under the MIT License.