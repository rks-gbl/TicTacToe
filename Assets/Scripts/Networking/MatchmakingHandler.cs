using System;
using UnityEngine;
using System.Text;
using System.Collections;
using UnityEngine.SceneManagement;

[Serializable]
public class MatchRequest {
    public string action;
    public string roomCode;
    public string playerName;
    public string difficulty;
}

[Serializable]
public class MoveMessage {
    public string action = "move";
    public int row;
    public int col;
}

public class MatchmakingHandler {
    public static void JoinRandomGame() {
        MatchRequest req = new MatchRequest {
            action = "join_random",
            playerName = "UnityPlayer"
        };
        string json = JsonUtility.ToJson(req);
        WebSocketManager.Instance.SendMessageToServer(json);
    }

    public static void CreatePrivateGame(string code) {
        MatchRequest req = new MatchRequest {
            action = "create_private",
            roomCode = code,
            playerName = "UnityPlayer"
        };
        string json = JsonUtility.ToJson(req);
        WebSocketManager.Instance.SendMessageToServer(json);
    }

    public static void JoinPrivateGame(string code) {
        MatchRequest req = new MatchRequest {
            action = "join_private",
            roomCode = code,
            playerName = "UnityPlayer"
        };
        string json = JsonUtility.ToJson(req);
        WebSocketManager.Instance.SendMessageToServer(json);
    }

    public static void PlayWithBot(string difficulty) {
        MatchRequest req = new MatchRequest {
            action = "play_bot",
            difficulty = difficulty
        };
        string json = JsonUtility.ToJson(req);
        WebSocketManager.Instance.SendMessageToServer(json);
    }

    public static void SendMove(int row, int col) {
        MoveMessage move = new MoveMessage {
            row = row,
            col = col
        };
        string json = JsonUtility.ToJson(move);
        WebSocketManager.Instance.SendMessageToServer(json);
    }
}
