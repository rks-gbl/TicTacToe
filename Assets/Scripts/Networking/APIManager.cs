using System.Collections;
using System.Runtime.Serialization;
using RitikUtils;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MSingleton<APIManager>
{
    private string baseUrl = "http://localhost:8080";

    [System.Obsolete]
    public void FindRandomMatch()
    {
        StartCoroutine(RandomMatchmaking(DataManager.Instance.playerData,TictactoeUtils.boardSize));
    }

    [System.Obsolete]
    IEnumerator RandomMatchmaking(PlayerData p, int boardSize)
    {
        string url = $"{baseUrl}/random_matchmaking";
        Player player = new Player { ID = p.id, Name = p.Name };
        MatchmakingRequest request = new MatchmakingRequest { player = player, board_size = boardSize };

        string formData = JsonUtility.ToJson(request);

        UnityWebRequest www = UnityWebRequest.Post(url , formData);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error + $" in {url} with data {formData}");
        }
        else
        {
            // Handle response
            GameResponse response = JsonUtility.FromJson<GameResponse>(www.downloadHandler.text);
            Debug.Log("Matchmaking Response: " + response.RoomID);
        }
    }

    [System.Serializable]
    public class Player
    {
        public string ID;
        public string Name;
    }

    [System.Serializable]
    public class MatchmakingRequest
    {
        public Player player;
        public int board_size;
    }

    [System.Serializable]
    public class GameResponse
    {
        public string RoomID;
        public Player Player1;
        public Player Player2;
        public Board Board;
    }

    [System.Serializable]
    public class Board
    {
        public int Size;
        public string[][] Cells;
    }
}
