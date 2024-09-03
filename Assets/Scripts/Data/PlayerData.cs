using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public string Name;
    public int Level;
    public string ProfileImage;

    public PlayerData()
    {
        this.Name = "Player 1";
        this.Level = 1;
        this.ProfileImage = null;
    }

    public PlayerData(string JSON)
    {
        PlayerData data = JsonUtility.FromJson<PlayerData>(JSON);
        this.Name = data.Name;
        this.Level = data.Level;
        this.ProfileImage = data.ProfileImage;
    }

    public PlayerData(string name , int level , string profileImage)
    {
        this.Name = name;
        this.Level = level;
        this.ProfileImage = profileImage;
    }

    public string JSON()
    {
        string s = JsonUtility.ToJson(this);
        Debug.Log(s);
        return s;
    }
}
