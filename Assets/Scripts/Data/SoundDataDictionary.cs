using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Sound Data" , menuName = "Scriptable/Sound/Data dictionary")]
public class SoundDataDictionary : ScriptableObject
{
    [SerializeField]List<SoundData> list;
    public Dictionary<string, AudioClip> dict = new Dictionary<string, AudioClip>();

    #if UNITY_EDITOR
    public void OnValidate()
    {
        SetDictionary();
    }
    #endif

    void SetDictionary()
    {
        dict.Clear();
        foreach (SoundData s in list)
        {
            if(!dict.ContainsKey(s.key))
                dict.Add(s.key , s.audioClip);
            else
                Debug.LogError("DUPLICATE KEY : " + s.key);
        } 
    }
}
[System.Serializable]
public class SoundData
{
    public string key;
    public AudioClip audioClip;
}
