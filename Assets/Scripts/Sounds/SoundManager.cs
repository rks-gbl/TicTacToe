using System.Collections;
using System.Collections.Generic;
using RitikUtils;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundManager : MPersistentSingleton<SoundManager>
{
    public SoundSettings settings;
    public SoundDataDictionary data;
    public AudioSource bgm;
    public AudioSourcePool aSourcePool;
    public Dictionary<string,AudioSource> playing = new Dictionary<string, AudioSource>();

    public void StartBGM()
    {
        if(bgm.isPlaying)
        return;

        bgm.loop = true;
        bgm.Play();
    }

    public void StopBGM()
    {
        bgm.Stop();
    }
    public void PlayOnce(string key)
    {
        var a = aSourcePool.GetObject();
        a.clip = GetClip(key);
        a.volume = settings.sfxVolume;
        if(a.clip == null)
            return;

        a.loop = false;

        a.Play();
    }
    public void PlayLoop(string key)
    {
        var a = aSourcePool.GetObject();
        
        if(playing.ContainsKey(key))
            a = playing[key];
        
        a.volume = settings.sfxVolume;
        a.clip = GetClip(key);

        if(a.clip == null)
            return;

        a.loop = true;
            
        if(!playing.ContainsKey(key))
            playing.Add(key,a);

        a.Play();
    }
    public void StopSound(string key)
    {
        if(playing.ContainsKey(key))
            playing[key].Stop();
    }
    public AudioClip GetClip(string key)
    {
        if(data.dict.ContainsKey(key))
            return data.dict[key];

        return null;
    }

    public void StopAll()
    {
        StopBGM();
        aSourcePool.ResetAll();
    }

    public void UpdateSFXVolume(float volume)
    {
        Debug.Log(volume);
        settings.SetSfxVolume(volume);
        foreach(AudioSource a in playing.Values)
        {
            a.volume = volume;
        }
    }

    public void UpdateBGMVolume(float volume)
    {
        Debug.Log(volume);
        settings.SetMusicVolume(volume);
        bgm.volume = volume;
    }
}
