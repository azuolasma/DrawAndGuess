# Draw and Guess (Online)


## Overview
Draw and Guess (Online) is a collaborative, real-time web app where players join a lobby, take turns drawing a secret word, and others guess it through chat.
Points are awarded for correct guesses and drawing accuracy.


## Prerequisites
Before running the project, ensure you have:
- Node.js >= v18
- .NET SDK >= 8.0
- SQL Server (or LocalDB)
- Git


## Installation
Clone repo:
```
git clone https://github.com/yourusername/online-drawing-board.git
cd online-drawing-board
```

Setup backend:
```
cd backend
dotnet restore
dotnet build
```

Setup frontend:
```
cd ../frontend
npm install
```


## Running the App
Start backend:
```
dotnet run
```
(default URL: https://localhost:5001)

Start frontend:
```
npm start
```
(default URL: http://localhost:3000)


## License
This project is open-source under the MIT License.