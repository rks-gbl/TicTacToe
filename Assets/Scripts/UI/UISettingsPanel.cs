using System;
using UnityEngine.UI;

public class UISettingsPanel : UIPanel
{
    public Slider musicVolume;
    public Slider sfxVolume;
    public Button back;


    public void Awake()
    {
        musicVolume.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxVolume.onValueChanged.AddListener(OnSFXVolumeChanged);
        back.onClick.AddListener(OnBack);
    }

    public void OnBack()
    {
        Disable();
    }
    
    void OnEnable()
    {
        musicVolume.value = SoundManager.Instance.settings.musicVolume;
        sfxVolume.value = SoundManager.Instance.settings.sfxVolume;
    }

    void OnDisable()
    {
        DataManager.Instance.SaveSoundData();
    }

    public void OnMusicVolumeChanged(float volume)
    {
        SoundManager.Instance.UpdateBGMVolume((float)Math.Round(volume,2));
    }
    public void OnSFXVolumeChanged(float volume)
    {
        SoundManager.Instance.UpdateSFXVolume((float)Math.Round(volume,2));
    }

    public void OnDestroy()
    {
        musicVolume.onValueChanged.RemoveListener(OnMusicVolumeChanged);
        sfxVolume.onValueChanged.RemoveListener(OnSFXVolumeChanged);
        back.onClick.RemoveListener(OnBack);
    }
}
