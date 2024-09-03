using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using RitikUtils;

public class MLTrainer : Agent
{
    [HideInInspector] public int turnNum;
    public char[,] board;
    public void StartGame()
    {
        TictactoeUtils.InitializeBoard(ref board, 5);
    }

    public void PlayerMove()
    {
        
    }
    public void AI_Move((int,int) pos)
    {
        if(pos == TictactoeUtils.GetWinningBox(board , TictactoeUtils.PLAYER_O))
        {
            AddReward(+3);
        }
        else if(pos == TictactoeUtils.GetWinningBox(board , TictactoeUtils.PLAYER_X))
        {
            AddReward(+2);
        }
        else
        {
            AddReward(-0.1f);
        }

        CheckStateAndReward();
    }

    public void CheckStateAndReward()
    {
        if(TictactoeUtils.IsDraw(board))
        {
            AddReward(1);
        }
        else if(TictactoeUtils.IsWinner(board,TictactoeUtils.PLAYER_X).Item1)
        {
            AddReward(-2);
        }
    }

    public void EndGame()
    {

    }
}
