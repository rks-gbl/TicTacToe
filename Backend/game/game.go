package game

import "fmt"

type Player struct {
	ID   string `json:"ID,omitempty"`
	Name string `json:"Name,omitempty"`
}

type Board struct {
	Size  int
	Cells [][]string
}

type Game struct {
	RoomID        string
	Player1       Player
	Player2       Player
	CurrentPlayer string
	Board         Board
	Status        string
}

func NewBoard(size int) Board {
	cells := make([][]string, size)
	for i := range cells {
		cells[i] = make([]string, size)
	}
	return Board{Size: size, Cells: cells}
}

func NewGame(roomID string, p1, p2 Player, boardSize int) *Game {
	return &Game{
		RoomID:  roomID,
		Player1: p1,
		Player2: p2,
		Board:   NewBoard(boardSize),
		Status:  "ongoing",
	}
}

func (g *Game) MakeMove(playerID string, x, y int) bool {
	if g.Status != "ongoing" ||
		g.CurrentPlayer != playerID ||
		x < 0 ||
		x >= g.Board.Size ||
		y < 0 ||
		y >= g.Board.Size ||
		g.Board.Cells[x][y] != "" {

		return false
	}
	g.Board.Cells[x][y] = playerID
	if g.CurrentPlayer == g.Player1.ID {
		g.CurrentPlayer = g.Player2.ID
	} else {
		g.CurrentPlayer = g.Player1.ID
	}
	// TODO: Check for win or draw
	return true
}

func (b Board) Print() {
	for _, row := range b.Cells {
		fmt.Println(row)
	}
}
