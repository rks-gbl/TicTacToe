package server

import (
	"encoding/json"
	"fmt"
	"net/http"
	"sync"

	"github.com/rks-gbl/TicTacToe/game"
)

var matchmaker = game.NewMatchMaker()
var gameStore = make(map[string]*game.Game)
var storeMu sync.Mutex

func handleRandomMatchmaking(w http.ResponseWriter, r *http.Request) {
	fmt.Println("Handle Random Matchmaking Called")
	var input struct {
		Player    game.Player `json:"player"`
		BoardSize int         `json:"board_size"`
	}
	if err := json.NewDecoder(r.Body).Decode(&input); err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	g := matchmaker.AddPlayer(input.Player, input.BoardSize)
	if g != nil {
		storeMu.Lock()
		gameStore[g.RoomID] = g
		storeMu.Unlock()
		w.WriteHeader(http.StatusOK)
		json.NewEncoder(w).Encode(g)
	} else {
		w.WriteHeader(http.StatusAccepted)
	}
}

func handleCreateRoom(w http.ResponseWriter, r *http.Request) {
	fmt.Println("Handle Create Room Called")
	var input struct {
		Players   []game.Player `json:"players"`
		BoardSize int           `json:"board_size"`
	}
	if err := json.NewDecoder(r.Body).Decode(&input); err != nil || len(input.Players) != 2 {
		http.Error(w, "Invalid input", http.StatusBadRequest)
		return
	}

	g := matchmaker.CreateRoom(input.Players[0], input.Players[1], input.BoardSize)
	storeMu.Lock()
	gameStore[g.RoomID] = g
	storeMu.Unlock()
	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(g)
}

func handleJoinRoom(w http.ResponseWriter, r *http.Request) {
	fmt.Println("Handle Join Room Called")
	var input struct {
		RoomID string
		Player game.Player
	}
	if err := json.NewDecoder(r.Body).Decode(&input); err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	g := matchmaker.JoinRoom(input.RoomID, input.Player)
	if g != nil {
		storeMu.Lock()
		gameStore[g.RoomID] = g
		storeMu.Unlock()
		w.WriteHeader(http.StatusOK)
		json.NewEncoder(w).Encode(g)
	} else {
		http.Error(w, "Room not found or full", http.StatusNotFound)
	}
}

func handleMakeMove(w http.ResponseWriter, r *http.Request) {
	fmt.Println("Handle Make Move Called")
	var input struct {
		RoomID   string
		PlayerID string
		X, Y     int
	}
	if err := json.NewDecoder(r.Body).Decode(&input); err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	storeMu.Lock()
	g, exists := gameStore[input.RoomID]
	storeMu.Unlock()

	if !exists || !g.MakeMove(input.PlayerID, input.X, input.Y) {
		http.Error(w, "Invalid move", http.StatusBadRequest)
		return
	}

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(g)
}

func handleGetGame(w http.ResponseWriter, r *http.Request) {
	fmt.Println("Handle Get Game Called")
	roomID := r.URL.Query().Get("room_id")
	storeMu.Lock()
	g, exists := gameStore[roomID]
	storeMu.Unlock()

	if !exists {
		http.Error(w, "Game not found", http.StatusNotFound)
		return
	}

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(g)
}

func StartHTTPServer() {
	fmt.Println("Starting Server")
	http.HandleFunc("/random_matchmaking", handleRandomMatchmaking)
	http.HandleFunc("/create_room", handleCreateRoom)
	http.HandleFunc("/join_room", handleJoinRoom)
	http.HandleFunc("/make_move", handleMakeMove)
	http.HandleFunc("/get_game", handleGetGame)
	http.ListenAndServe(":8080", nil)
}
