using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIResultPanel : UIPanel
{
    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] Button home , playAgain;

    void Awake(){
        home.onClick.AddListener(Home);
        playAgain.onClick.AddListener(PlayAgain);
    }

    void OnDestroy(){
        home.onClick.RemoveListener(Home);
        playAgain.onClick.RemoveListener(PlayAgain);
    }

    void Home()
    {
        UIManager.Instance.EnablePanel(Consts.UIBoardSelectionPanel);
        SoundManager.Instance.StopAll();
        Disable();
    }

    void PlayAgain()
    {
        UIManager.Instance.EnablePanel(Consts.UIGamePanel);
        GameManager.Instance.StartGame(GameManager.Instance.gameMode , GameManager.Instance.AI.botType);
        Disable();
    }

    public void UpdateResult(bool p1Won)
    {
        if(p1Won)
        {
            resultText.text = $"{DataManager.Instance.playerData.Name} Won";
        }
        else
        {
            resultText.text = $"{DataManager.Instance.GetOpponentData().Name} Won";
        }
    }

    public void ShowDraw()
    {
        resultText.text = $"DRAW";
    }
}
