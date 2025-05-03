using System;
using UnityEngine;

[Serializable]
public class ServerTextMessage
{
    public string message;
}

[Serializable]
public class ServerStateUpdate
{
    public string[,] board; // 3x3 board
    public string turn;
}

[Serializable]
public class BoardWrapper
{
    public string[][] board; // JSON parses into jagged array
    public string turn;
}

public class WebSocketMessageHandler 
{
    public static void HandleStateMessage(string json)
    {
        try
        {
            // Workaround for 2D array parsing:
            json = json.Replace("\"[", "[").Replace("]\"", "]");

            // Parse manually as wrapper
            var wrapper = JsonUtility.FromJson<BoardWrapper>(json);

            Debug.Log("Turn: " + wrapper.turn);
            PrintBoard(wrapper.board);

            // TODO: Update your UI here
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to parse state: " + ex.Message);
        }
    }

    static void PrintBoard(string[][] board)
    {
        for (int i = 0; i < board.Length; i++)
        {
            string row = "";
            for (int j = 0; j < board[i].Length; j++)
            {
                row += board[i][j] == "" ? "-" : board[i][j];
            }
            Debug.Log(row);
        }
    }


    public static void HandleTextMessage(string json)
    {
        try
        {
            ServerTextMessage msg = JsonUtility.FromJson<ServerTextMessage>(json);
            Debug.Log("Server says: " + msg.message);

            // TODO: Display message in UI (e.g., chat box or log)
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to parse text message: " + ex.Message);
        }
    }

}