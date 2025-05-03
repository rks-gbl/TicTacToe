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
        if(GameManager.Instance.connectionMode == ConnectionMode.Offline)
        {
            UIManager.Instance.EnablePanel(Consts.UIGamePanel);
            GameManager.Instance.StartGame(GameMode.Bot , BotType.Hard);    
            Disable();
        }
        else
        {
            MatchmakingHandler.PlayWithBot("easy");
        }
    }

    void Medium()
    {

        if(GameManager.Instance.connectionMode == ConnectionMode.Offline)
        {
            UIManager.Instance.EnablePanel(Consts.UIGamePanel);
            GameManager.Instance.StartGame(GameMode.Bot , BotType.Medium);    
            Disable();
        }
        else
        {
            MatchmakingHandler.PlayWithBot("medium");
        }
    }

    void Easy()
    {
        
        if(GameManager.Instance.connectionMode == ConnectionMode.Offline)
        {
            UIManager.Instance.EnablePanel(Consts.UIGamePanel);
            GameManager.Instance.StartGame(GameMode.Bot , BotType.Easy);    
            Disable();
        }
        else
        {
            MatchmakingHandler.PlayWithBot("hard");
        }
    }

    void Back()
    {
        UIManager.Instance.EnablePanel(Consts.UIOfflinePanel);
        Disable();
    }
}
