# Data models
enum **GameState** { WaitingForPlayers, InProgress, RoundEnded, Finished }

**GameRoomSettings**:
- Id (int)
- Name (string)
- PlayerLimit (int)
- RoundLimit (int)

**GameRoomState**:
- Players (`List<Player>`)
- Host (Player)
- CurrentRound (GameRound)
- CurrentDrawer (Player)
- State (GameState)
- Chat (GameChat)

**Player**:
- Id (int)
- Name (string)
- RoomId (int)
- Score (int)

**GameChat**:
- Messages (`List<GameMessage>`)

**GameMessage**:
- PlayerId (int)
- PlayerName (string)
- Message (string)
- Timestamp (DateTime)
- IsCorrectGuess (bool)

**GameLeaderboard**:
- PlayersDescendingByScore (`List<Player>`)

**GameRound**:
- RoundNumber (int)
- DrawerId (int)
- Word (GameWord)
- TimeRemaining (TimeSpan)
- IsFinished (bool)

**GameWord**:
- word (string)
- letterCount (int)



notes:
1. (seperated GameRoom into GameRoomSettings and GameRoomState since GameRoomSettings is static and GameRoomState is dynamic)
2.  its good to have GameWord class instead of just storing a string because we might need to add additional properties
like category, difficulty, language, etc. and in general it just adds
semantic clarity.
3. probably would be a good idea to refactor to DTO's later, but cba to add so much boilerplate atm

