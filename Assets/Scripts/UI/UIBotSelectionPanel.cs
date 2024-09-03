using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class UIBotSelectionPanel : UIPanel
{
    [SerializeField] Button  easyBot ,normalBot , hardBot , back;

    void Awake()
    {
        easyBot.onClick.AddListener(PlayWithEasyBot);
        normalBot.onClick.AddListener(PlayWithNormalBot);
        hardBot.onClick.AddListener(PlayWithUnbeatableBot);
        back.onClick.AddListener(Back);
    }

    void OnDestroy()
    {
        easyBot.onClick.RemoveListener(PlayWithEasyBot);
        normalBot.onClick.RemoveListener(PlayWithNormalBot);
        hardBot.onClick.RemoveListener(PlayWithUnbeatableBot);
        back.onClick.RemoveListener(Back);
    }

    void PlayWithUnbeatableBot()
    {
        UIManager.Instance.EnablePanel(Consts.UIGamePanel);
        GameManager.Instance.StartGame(GameMode.OfflineBot , BotType.Undefeatable);
        Disable();
    }

    void PlayWithNormalBot()
    {
        UIManager.Instance.EnablePanel(Consts.UIGamePanel);
        GameManager.Instance.StartGame(GameMode.OfflineBot , BotType.Normal);
        Disable();
    }

    void PlayWithEasyBot()
    {
        UIManager.Instance.EnablePanel(Consts.UIGamePanel);
        GameManager.Instance.StartGame(GameMode.OfflineBot , BotType.Easy);
        Disable();
    }

    void Back()
    {
        UIManager.Instance.EnablePanel(Consts.UIOfflinePanel);
        Disable();
    }
}
