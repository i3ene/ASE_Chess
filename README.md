# Advanced Software Engineering - Chess
Chess client/server console application with C# and .NET

# Features
This console application implements a client and a server with shared logic.

## Logic
A basic chess game with two players.
- [x] 2D Vector for positioning (Vector2D Class as **ValueObject**)
	- [x] Position for Pieces (BoardPosition inherits from Vector2D as **ValueObject**)
- [x] Colors (PieceColor Enum as **ValueObject**)
- [x] (Board) Pieces ((Board/)PieceRepository Class as **Repository** (needs an **Aggregate**?))
	- [x] General Piece (Piece Class uses PieceRepository)
	- [x] Piece Information (PieceService as **Service**; for initial positioning)
	- [x] Initial Creation of Pieces (PieceFactory Class as **Factory** uses PieceService; for initial Piece creation)
- [x] Board (BoardService as **Service**; has logic for moving Pieces)
- [x] Players (PlayerRepository Class as **Repository**)
	- [x] Player (Player Class uses PieceRepository as **Entity**)
	- [x] Initial Creation of Players (PlayerFactory as **Factory** uses PieceService; for initial Player creation)
- [ ] Communication (CommunicationService as **Service**; this is where Client and Server extend their individual logic)
    - [ ] Socket (Socket Class as **Entity**)
	- [ ] Actions (Abstract SocketAction Class as **Entity**)
		- [ ] Participation for joining or leaving (ParticipationAction)
		- [ ] Move (MoveAction)
		- [ ] Turn (TurnAction)
		- [ ] Message (MessageAction)
		- [ ] Synchronisation (SyncAction; for synchronisation of the whole game for every participant) 
- [ ] Game (Game Class uses CommunicationService)

## Server
Uses HTTP and WebSocket functionallity.
Provides Channels for listening to Game and/or Chat Actions.
- [ ] Server implementation of Communication (ServerCommunicationService Class)
	- [ ] Server Socket implementation (ServerSocket Class)
	- [ ] List of all Connection (SocketRepository)
- [ ] Channels for Sockets (Abstract ChannelService Class uses ServerCommunicationService)
	- [ ] Game Channel (GameChannel uses Game)
	- [ ] Chat Channel (ChatChannel)
- [ ] _Optional_ Application (ServerProgramm)

## Client
Implements basic Shell console functionallity.
No Offline play, only Online. On "Host" creates a Server Instance.
- [ ] Client implementation of Connection (ClientCommunicationService Class)
	- [ ] Client Socket implementation (ClientSocket Class)
- [ ] Console input control (InteractionService)
	- [ ] Interface Interactable for Interactions (IInteractable Interface)
	- [ ] Interaction Event and Arguments (InteractionEvent)
- [ ] Various Settings (SettingsService)
- [ ] Navigation for Views (RouterService)
- [ ] Visual Elements (Abstract Component Class)
	- [ ] Text Element (TextComponent Class)
	- [ ] Input Element (InputComponent Class implements IInteractable)
	- [ ] Button Element (ButtonComponent Class inherits from TextComponent and impelments IInteractable)
	- [ ] Container Element (ContainerComponent Class uses InteractionService to interact; every Interaction is passed to childs)
	- [ ] Selection Element (SelectionComponent Class inherits from ContainerComponent; every Interaction is only passed to selected child)
- [ ] Visual Containers (View Class inherits from ContainerComponent and uses RouterService)
	- [ ] Visual Game (GameView uses ClientCommunicationService and Game)
	- [ ] Visual Chat (ChatView uses ClientCommunicationService)
	- [ ] Visual Menu (MenuView)
	- [ ] Visual Settings (SettingsView)
	- [ ] Visual Join/Host/View (LobbyView)
- [ ] Main Visual Container (ViewRepository uses ClientCommunicationService, InteractionService)
- [ ] Console character based rendering (DisplayService uses SettingsService and ViewRepository)
	- [ ] Interface for Rendering (IDisplayable Interface; Update() and Display())
- [ ] Main Application (ClientProgramm; instanciates SettingsService, ClientCommunicationService, InteractionService, ViewRepository and DisplayService)


## Test
Testing Framework is _MS Test_ for Component Testing. Mainly testing functionallity in Logic.