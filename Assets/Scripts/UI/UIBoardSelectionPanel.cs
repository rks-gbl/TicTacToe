using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBoardSelectionPanel : UIPanel
{
    [SerializeField] Button Three  , Four, Five , settings;

    void Awake()
    {
        Three.onClick.AddListener(()=>{SetBoardSize(3);});
        Four.onClick.AddListener(()=>{SetBoardSize(4);});
        Five.onClick.AddListener(()=>{SetBoardSize(5);});
        settings.onClick.AddListener(Settings);
    }

    void OnDestroy()
    {
        Three.onClick.RemoveAllListeners();
        Four.onClick.RemoveAllListeners();
        Five.onClick.RemoveAllListeners();
        settings.onClick.RemoveListener(Settings);
    }

    public void SetBoardSize(int size)
    {
        GameManager.Instance.SetBoardSize(size);
        UIManager.Instance.EnablePanel(Consts.UIModePanel);
        Disable();
    }

    public void Settings()
    {
        UIManager.Instance.EnablePanel(Consts.UISettingsPanel);
    }
}
