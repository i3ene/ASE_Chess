# Advanced Software Engineering - Chess
Chess client/server console application with C# and .NET

# Features
This console application implements a client and a server with shared logic.

## Logic
Game:
- Has a player repository to handle both players.
- Has a board that is a chess piece repository to hold chess pieces.
- Has a service that checks interactions between board and chess pieces.

Communication:
- Contains a basic WebSocket implementation.
- Contains various actions that can be exchanged via a socket.

## Server
Server instance:
- Uses HTTP and WebSocket functionallity.
- Can be startet via an URI to listen to.
- Can register channels to propagate received actions to.

Game channel:
- Allows to participate either by joining or by viewing.
- If a player joins and the game is already full, the player will join as viewer.
- If a player suddenly disconnects, he will automatically be removed from the game.
- Handles movement from players. If the action comes from a viewer or a participant that is not on turn, the sender will receive an appropiate message.
- Broadcasts synchronisation actions to keep all listeners up to date.

Chat channel:
- Receives messages from sockets and broadcasts them to other sockets.
- Broadcasts joining or leaving sockets.

## Client
View:
- Contains a basic UI framework to display character based elements.
- Implements a service to display elements to console.
- Handles console key inputs.
- Contains various views to navigate through and control the client application

Pages:
- Menu: Displays hosting, joining, viewing and settings navigation.
- Connection: Allows hosting, joining or viewing of a game. When hosting, automatically creates a server instance and joins it.
- Game: Displays a basic chess board and allows interaction with. A game always needs to be connected to a server.
- Chat: Displays a basic chat and allows interaction with.
- Settings: Displays all available properties from the global settings.

Game: 
- Client WebSocket implementation. Sends and receives actions from a server.
- No (internal) offline play is available.
- Every interaction needs to be send over WebSocket.
- Received actions are directly handled and displayed.
- Can receive synchronisation calls to eliminate de-sync.

## Test
Testing Framework is _MS Test_ for Component Testing. Mainly testing functionallity in Logic.