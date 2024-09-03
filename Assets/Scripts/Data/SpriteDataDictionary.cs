using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sprite Data Dictionary" , menuName = "Scriptable/SpriteDataScriptable")]
public class SpriteDataDictionary : ScriptableObject
{
    [SerializeField]List<SpriteData> list;
    public Dictionary<string, Sprite> dict = new Dictionary<string, Sprite>();

    #if UNITY_EDITOR
    public void OnValidate()
    {
        SetDictionary();
    }
    #endif

    void SetDictionary()
    {
        dict.Clear();
        foreach (SpriteData s in list)
        {
            if(!dict.ContainsKey(s.key))
                dict.Add(s.key , s.sprite);
            else
                Debug.LogError("DUPLICATE KEY : " + s.key);
        } 
    }
}
[System.Serializable]
public class SpriteData
{
    public string key;
    public Sprite sprite;
}
