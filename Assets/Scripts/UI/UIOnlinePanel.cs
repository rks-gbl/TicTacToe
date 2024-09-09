using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class UIOnlinePanel : UIPanel
{
    [SerializeField] Button random , playWithFriends , bot , back;

    [System.Obsolete]
    void Awake()
    {
        random.onClick.AddListener(RandomMM);
        playWithFriends.onClick.AddListener(PrivateMM);
        bot.onClick.AddListener(Bot);
        back.onClick.AddListener(Back);
    }

    [System.Obsolete]
    void RandomMM()
    {
        APIManager.Instance.FindRandomMatch();
    }

    void PrivateMM()
    {

    }

    void Bot()
    {

    }

    void Back()
    {
        UIManager.Instance.EnablePanel(Consts.UIModePanel);
        Disable();
    }
}
