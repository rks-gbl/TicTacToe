using System;
using UnityEditor;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public string id;
    public string Name;
    public int Level;
    public string ProfileImage;

    public PlayerData()
    {
        this.id = Guid.NewGuid().ToString();
        this.Name = "Player 1";
        this.Level = 1;
        this.ProfileImage = null;
    }

    public PlayerData(string JSON)
    {
        PlayerData data = JsonUtility.FromJson<PlayerData>(JSON);
        this.id = data.id;
        this.Name = data.Name;
        this.Level = data.Level;
        this.ProfileImage = data.ProfileImage;
    }

    public PlayerData(string id,string name , int level , string profileImage)
    {
        this.id = id != null ? id : Guid.NewGuid().ToString();
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
