using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOfflinePanel : UIPanel
{
    [SerializeField] Button twoPlayerButton , botButton , back;

    void Awake()
    {
        twoPlayerButton.onClick.AddListener(TwoPlayer);
        botButton.onClick.AddListener(Bot);
        back.onClick.AddListener(Back);
    }

    void OnDestroy()
    {
        twoPlayerButton.onClick.RemoveListener(TwoPlayer);
        botButton.onClick.RemoveListener(Bot);
        back.onClick.RemoveListener(Back);
    }

    void TwoPlayer()
    {
        GameManager.Instance.StartGame(GameMode.OfflineTwoPlayer, BotType.None);

        UIManager.Instance.EnablePanel(Consts.UIGamePanel);
        Disable();
    }

    void Bot()
    {
        UIManager.Instance.EnablePanel(Consts.UIBotSelectionPanel);
        Disable();
    }

    void Back()
    {
        UIManager.Instance.EnablePanel(Consts.UIModePanel);
        Disable();
    }
}
