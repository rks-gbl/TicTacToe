using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class UIModePanel : UIPanel
{
    public Button online, offline , back;

    void Awake()
    {
        online.onClick.AddListener(OnOnlineClicked);
        offline.onClick.AddListener(OnOfflineClicked);
        back.onClick.AddListener(Back);
    }
    void OnDestroy()
    {
        online.onClick.RemoveListener(OnOnlineClicked);
        back.onClick.RemoveListener(Back);
        offline.onClick.RemoveListener(OnOfflineClicked);
    }

    void OnOnlineClicked()
    {
        UIManager.Instance.EnablePanel(Consts.UIOnlinePanel);
        Disable();
    }

    void OnOfflineClicked()
    {
        UIManager.Instance.EnablePanel(Consts.UIOfflinePanel);
        Disable();
    }

    void Back()
    {
        UIManager.Instance.EnablePanel(Consts.UIBoardSelectionPanel);
        Disable();
    }
}
