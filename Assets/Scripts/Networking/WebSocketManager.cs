using System;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;

public class WebSocketManager : MonoBehaviour
{
    private WebSocket ws;

    void Start()
    {
        ws = new WebSocket("ws://localhost:8081/ws"); // Your WebSocket server URL
        ws.OnMessage += OnMessageReceived;
        ws.Connect();
    }

    private void OnMessageReceived(object sender, MessageEventArgs e)
    {
        Debug.Log("Message from server: " + e.Data);
        // Handle the message from the server
    }

    public void SendMessageToServer(string message)
    {
        ws.Send(message);
    }

    void OnDestroy()
    {
        ws.Close();
    }
}
