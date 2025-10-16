# General info about the project

## Frontend stack
- React (Vite or CRA)
- HTML5 Canvas API
- Tailwind CSS
- @microsoft/signalr (client library)

## Backend stack
- ASP.NET Core 8 Web API
- SignalR for real-time communication
- Entity Framework Core (for database)
- SQL Server


## Project structure:
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
