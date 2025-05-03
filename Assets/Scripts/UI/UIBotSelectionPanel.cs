using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class UIBotSelectionPanel : UIPanel
{
    [SerializeField] Button  easyBot ,mediumBot , hardBot , back;

    void Awake()
    {
        easyBot.onClick.AddListener(Easy);
        mediumBot.onClick.AddListener(Medium);
        hardBot.onClick.AddListener(Hard);
        back.onClick.AddListener(Back);
    }

    void OnDestroy()
    {
        easyBot.onClick.RemoveListener(Easy);
        mediumBot.onClick.RemoveListener(Medium);
        hardBot.onClick.RemoveListener(Hard);
        back.onClick.RemoveListener(Back);
    }

    void Hard()
    {
        UIManager.Instance.EnablePanel(Consts.UIGamePanel);
        if(GameManager.Instance.connectionMode == ConnectionMode.Offline)
        {
            GameManager.Instance.StartGame(GameMode.Bot , BotType.Hard);    
        }
        else
        {
            MatchmakingHandler.PlayWithBot("easy");
        }
        Disable();
    }

    void Medium()
    {
        UIManager.Instance.EnablePanel(Consts.UIGamePanel);

        if(GameManager.Instance.connectionMode == ConnectionMode.Offline)
        {
            GameManager.Instance.StartGame(GameMode.Bot , BotType.Medium);    
        }
        else
        {
            MatchmakingHandler.PlayWithBot("medium");
        }
        Disable();
    }

    void Easy()
    {
        UIManager.Instance.EnablePanel(Consts.UIGamePanel);
        if(GameManager.Instance.connectionMode == ConnectionMode.Offline)
        {
            GameManager.Instance.StartGame(GameMode.Bot , BotType.Easy);    
        }
        else
        {
            MatchmakingHandler.PlayWithBot("hard");
        }
        Disable();
    }

    void Back()
    {
        UIManager.Instance.EnablePanel(Consts.UIOfflinePanel);
        Disable();
    }
}
