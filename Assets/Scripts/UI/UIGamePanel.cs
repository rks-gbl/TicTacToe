using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGamePanel : UIPanel
{
    public UIPlayerCard pc1, pc2;

    void OnEnable()
    {
        SoundManager.Instance.StartBGM();
    }
}
