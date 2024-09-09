package main

import "github.com/rks-gbl/TicTacToe/server"

func main() {
	go server.StartHTTPServer()
	server.StartWebSocketServer()
}
