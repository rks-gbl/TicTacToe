package game

import (
	"fmt"
	"sync"
)

type Matchmaker struct {
	waitingPlayers []Player
	rooms          map[string]*Game
	mu             sync.Mutex
}

func NewMatchMaker() *Matchmaker {
	return &Matchmaker{
		rooms: make(map[string]*Game),
	}
}

func (m *Matchmaker) AddPlayer(player Player, boardSize int) *Game {
	m.mu.Lock()
	defer m.mu.Unlock()

	if len(m.waitingPlayers) > 0 {
		opponent := m.waitingPlayers[0]
		m.waitingPlayers = m.waitingPlayers[1:]
		roomID := fmt.Sprintf("%s-%s", player.ID, opponent.ID) // Unique room ID
		game := NewGame(roomID, player, opponent, boardSize)
		m.rooms[roomID] = game
		return game
	}

	m.waitingPlayers = append(m.waitingPlayers, player)
	return nil
}

func (m *Matchmaker) CreateRoom(p1, p2 Player, boardSize int) *Game {
	m.mu.Lock()
	defer m.mu.Unlock()

	roomID := fmt.Sprintf("%s-%s", p1.ID, p2.ID) // Unique room ID
	game := NewGame(roomID, p1, p2, boardSize)
	m.rooms[roomID] = game
	return game
}

func (m *Matchmaker) JoinRoom(roomID string, player Player) *Game {
	m.mu.Lock()
	defer m.mu.Unlock()

	game, exists := m.rooms[roomID]
	if !exists || game.Player2.ID != "" {
		return nil
	}

	game.Player2 = player
	return game
}

func (m *Matchmaker) GetRoom(roomID string) *Game {
	m.mu.Lock()
	defer m.mu.Unlock()

	return m.rooms[roomID]
}
