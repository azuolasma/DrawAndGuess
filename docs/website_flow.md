# Website Flow

## 1. User enters root website (`/`)
The home screen displays:
- **List of existing public rooms (with refresh button)**
  - Each room shows: name, current players out of max players, which round it is out of which, game state.
  - A "Join Room" button is available for joinable rooms.
- **"Create Room"** button

**Actions:**
1. Click **Join Room** → proceeds to "Enter Name" screen.
2. Click **Create Room** → proceeds to "Enter Name" and then "Room Settings" screen.


## 2. User enters name
- User is prompted for a display name (required).
- After entering, the app, proceeds to either:
  - Join the selected room (if joining)
  - Show "Create Room" settings (if creating)


## 3. If creating a room
- User can configure:
  - Room name (default: `<username>’s room`)
  - Round count (int 3-10)
  - Player limit (max 16)
- After confirming, the server:
  - Creates a `GameRoom` object
  - Assigns this user as host
  - Redirects to `/room/<roomId>`
  - Broadcasts room creation event (for other users to see new public room)


## 4. Inside the room (`/room/<roomId>`)
The user sees:

### General UI
- **Top left:** Round info ("Round 1 / 5")
- **Top center:** Word hint (drawer sees full word; guessers see `_ _ _ _`)
- **Top right:** Timer countdown for current round
- **Left panel:** Player list with names and scores
- **Center:** Drawing canvas (interactive only for drawer)
- **Right panel:** Chat (for guesses and chatting, also mby system messages)
- **Bottom:** Toolbar (clear, color, brush size, undo — only visible to drawer)

### Real-time behavior
The game state updates in real time via SignalR:
- Canvas data (strokes)
- Chat messages (guesses)
- Player join/leave notifications
- Score updates
- Round transitions


## 5. Gameplay flow (round loop)

1. **Round start:**
   - Server picks the next drawer (rotating)
   - Drawer receives 3 word options and selects one
   - Guessers see "_ _ _ _" hint (same length as word)
   - Timer starts (e.g., 90 seconds)

2. **During round:**
   - Drawer draws → canvas updates broadcast to all clients
   - Guessers send chat messages
     - If message matches the word (case-insensitive):
       - Guesser receives points based on remaining time
       - Drawer receives a smaller fixed number of points
       - Guessers who already guessed correctly can still chat, but their guess no longer counts
   - Timer updates for all players

3. **Round end:**
   - When time runs out or all guessers guessed correctly:
     - Server reveals the word
     - Brief 5-second intermission (“Next round starting…”)
     - Moves to next round or ends the game if limit reached


## 6. Game end
- When all rounds are finished:
  - Leaderboard modal appears showing final scores (sorted)
  - Option buttons:
    - “Play Again” (host restarts)
    - “Leave Room”
  - Results broadcast to all clients


## 7. Leaving / reconnecting
- If user leaves mid-game:
  - They’re removed from the player list
  - If they reconnect within a timeout window, rejoin is possible
  - If the drawer leaves, a new drawer is assigned automatically


## 8. Error / edge cases
- Joining full room → show "Room is full"
- Joining nonexistent room → "Room not found"
- Connection lost → “Reconnecting…” message
- Drawer disconnects → automatically reassign drawer and skip to next round if needed
