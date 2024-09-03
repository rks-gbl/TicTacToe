using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundSettings", menuName = "Scriptable/Sound/Settings")]
[System.Serializable]
public class SoundSettings : ScriptableObject
{
    public float musicVolume;
    public float sfxVolume;
    public void SetMusicVolume(float v)
    {
        musicVolume = v;
    }
    public void SetSfxVolume(float v)
    {
        sfxVolume = v;
    }
}
