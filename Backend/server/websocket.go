package server

import (
	"fmt"
	"net/http"

	"github.com/gorilla/websocket"
)

var upgrader = websocket.Upgrader{}

func handleWebSocketConnection(w http.ResponseWriter, r *http.Request) {
	conn, err := upgrader.Upgrade(w, r, nil)
	if err != nil {
		panic(err)
	}
	defer conn.Close()

	for {
		_, msg, err := conn.ReadMessage()
		if err != nil {
			break
		}

		fmt.Println("Message : ", msg)
		// TODO: Implement dynamic board handling
	}
}

func StartWebSocketServer() {
	http.HandleFunc("/ws", handleWebSocketConnection)
	http.ListenAndServe(":8081", nil)
}
