using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using RitikUtils;
using System.Linq;
using System.Xml.Serialization;
using System.Collections;
using System.Drawing;
using Unity.MLAgents;

public class AIManager : MonoBehaviour
{
    public BotType botType;
    List<CheckBox> allCheckboxes;
    [SerializeField]AIConfigScriptable aiData;
    GameObject mlAgentPrefab;
    private MLAgent mlAgent;
    public MLAgent MLAgent
    {
        get
        {
            if(mlAgent == null)
            {
                mlAgent = Instantiate(mlAgentPrefab, transform).GetComponent<MLAgent>();
            }
            return mlAgent;
        }
        private set{
            mlAgent = value;
        }
    }

    public AIManager(BotType type)
    {
        botType = type;
    }

    public void Reset()
    {
        if(allCheckboxes == null)
            allCheckboxes = new List<CheckBox>();
        else{
            allCheckboxes.Clear();
        }

        foreach(var c in GameManager.Instance.checkBoxes)
        {
            allCheckboxes.Add(c.Value);
        }
    }

    IEnumerator SetCheckboxCoroutine()
    {
        yield return new WaitForSeconds(aiData.dict[botType].delay);
        SetCheckbox();
    }

    void SetCheckbox()
    {
        switch(botType)
        {
            case BotType.Easy:
            GetRandom().SetOccupied();
            break;
            case BotType.Normal:
            GetNormal().SetOccupied();
            break;
            case BotType.Undefeatable:
            if(TictactoeUtils.boardSize < 5)
                GetUndefeatable().SetOccupied();
            else
            {
                //Check if ML onnx file is present
                if(aiData.modelDict.ContainsKey(TictactoeUtils.boardSize))
                {
                    //TO DO
                }
                else //Do a normal move for now
                {
                    GetNormal().SetOccupied();
                }
            }
            break;
            case BotType.Online:
            //Get response from server
            break;
        }
    }
    CheckBox GetRandom()
    {
        int rand = Random.Range(0, allCheckboxes.Count);
        return allCheckboxes[rand];
    }
    CheckBox GetNormal()
    {
        (int,int) pos = TictactoeUtils.GetWinningBox(GameManager.Instance.board , TictactoeUtils.PLAYER_O); //Checking for self winning first
        if(pos == (-1,-1))
        {
            pos = TictactoeUtils.GetWinningBox(GameManager.Instance.board , TictactoeUtils.PLAYER_X); //Checking for opponent winning next
            if(pos !=(-1,-1))
            {
                return GameManager.Instance.checkBoxes[pos];
            }
        }
        else
            return GameManager.Instance.checkBoxes[pos];

        return GetRandom();
    }
    CheckBox GetUndefeatable()
    {
        if(TictactoeUtils.boardSize < 5)
            return GameManager.Instance.checkBoxes[TictactoeUtils.FindBestMove(GameManager.Instance.board, TictactoeUtils.PLAYER_O)];
        else
        {
            //for now using normal check for bigger boards
            //Even with alpha beta pruning Minimax algo gets stuck for bigger board
            return GetNormal();
        }
    }

    public void RemoveCheckbox(CheckBox cb)
    {
        allCheckboxes.Remove(cb);
    }
    public void Move()
    {
        StartCoroutine(SetCheckboxCoroutine());
    }
}

