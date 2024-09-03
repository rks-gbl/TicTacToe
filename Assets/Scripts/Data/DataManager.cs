using UnityEngine;
using RitikUtils;

public class DataManager : MPersistentSingleton<DataManager>
{
    //Data should be saved on cloud ... need more details on what to do ...
    public PlayerData playerData;

    public PlayerData GetOpponentData()
    {
        return new PlayerData("Player 2" , 1 , null);
    }

    public void Start()
    {
        GetAllData();
    }

    public void GetAllData()
    {
        if(PlayerPrefs.HasKey(Consts.DATA_PLAYER))
            playerData = new PlayerData(PlayerPrefs.GetString(Consts.DATA_PLAYER));
        else
            playerData = new();

        if(PlayerPrefs.HasKey(Consts.DATA_MUSIC_VOL) && PlayerPrefs.HasKey(Consts.DATA_SFX_VOL)){
            SoundManager.Instance.UpdateBGMVolume(PlayerPrefs.GetFloat(Consts.DATA_MUSIC_VOL));
            SoundManager.Instance.UpdateSFXVolume(PlayerPrefs.GetFloat(Consts.DATA_SFX_VOL));
        }
        else{

            SoundManager.Instance.UpdateBGMVolume(0.5f);
            SoundManager.Instance.UpdateSFXVolume(0.5f);
        }
    }

    public void SaveAllData()
    {
        PlayerPrefs.SetString(Consts.DATA_PLAYER , playerData.JSON());
        SaveSoundData();
    }

    public void SaveSoundData()
    { 
        PlayerPrefs.SetFloat(Consts.DATA_MUSIC_VOL , SoundManager.Instance.settings.musicVolume);
        PlayerPrefs.SetFloat(Consts.DATA_SFX_VOL , SoundManager.Instance.settings.sfxVolume);
    }

    public void OnDestroy()
    {
        SaveAllData();
    }
}