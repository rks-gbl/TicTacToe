using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class UIOnlinePanel : UIPanel
{
    [SerializeField] Button random , playWithFriends , bot , back;

    void Awake()
    {
        random.onClick.AddListener(RandomMM);
        playWithFriends.onClick.AddListener(PrivateMM);
        bot.onClick.AddListener(Bot);
        back.onClick.AddListener(Back);
    }

    void RandomMM()
    {

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
