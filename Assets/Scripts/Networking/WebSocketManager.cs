using System;
using System.Text;
using RitikUtils;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;

public class WebSocketManager : MSingleton<WebSocketManager>
{
    private WebSocket ws;

    void Start()
    {
        ws = new WebSocket("ws://localhost:8080/ws"); // Your WebSocket server URL
        ws.OnMessage += OnMessageReceived;
        ws.Connect();
    }

    private void OnMessageReceived(object sender, MessageEventArgs e)
    {
        Debug.Log("Message from server: " + e.Data);
        string json = e.Data;
        Debug.Log("Received: " + json);

        if (json.Contains("board") && json.Contains("turn"))
        {
            WebSocketMessageHandler.HandleStateMessage(json);
        }
        else if (json.Contains("message"))
        {
            WebSocketMessageHandler.HandleTextMessage(json);
        }
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
