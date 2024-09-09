using System.Collections.Generic;
using UnityEngine;
using RitikUtils;
using UnityEngine.Profiling;
using Unity.VisualScripting;

public class GameManager : MPersistentSingleton<GameManager>
{
    public Dictionary<(int,int),CheckBox> checkBoxes=new Dictionary<(int, int), CheckBox>();
    [SerializeField]private int turnNum = 0;
    public GameObject AIPrefab;
    [SerializeField] CheckboxPool checkBoxPool;
    [SerializeField] GameObject vert , hor , diagLeft , diagRight;
    
    AIManager ai;
    public AIManager AI
    {
        get{
            if (this.ai == null)
                this.ai = Instantiate(AIPrefab).GetComponent<AIManager>();

            return this.ai;
        }
        private set{
            this.ai = value;
        }
    }


    public bool gameStarted = false;
    public GameMode gameMode;
    public char[,] board;

    public override void Awake()
    { 
        base.Awake();
    }

    public void InitializeBoard()
    {
        TictactoeUtils.InitializeBoard(ref board);
    }
    
    public bool IsP1Turn()
    {
        return turnNum % 2 == 0;
    }
    /// <summary>
    /// Updates the turn number , enabling and disabling interaction from client side based on turn num and showing result screen if win, lose or draw
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="Cross"></param>
    public void UpdateTurn((int,int) pos , bool Cross)
    {
        turnNum++;
        board[pos.Item1 , pos.Item2] = Cross?TictactoeUtils.PLAYER_X:TictactoeUtils.PLAYER_O;

        UIResultPanel rp = UIManager.Instance.allPanels[Consts.UIResultPanel] as UIResultPanel;

        var result = TictactoeUtils.IsWinner(board , TictactoeUtils.PLAYER_X);
        if(result.Item1)
        {
            EndGame();
            rp.UpdateResult(true);
            ShowStrike(result.Item2 , (StrikeType)result.Item3);
            SoundManager.Instance.PlayOnce(Consts.GameWin);
            UIManager.Instance.EnablePanel(rp.id);
            return;
        }
        else 
        {
            result = TictactoeUtils.IsWinner(board , TictactoeUtils.PLAYER_O);
            if(result.Item1)
            {
                EndGame();
                rp.UpdateResult(false);
                ShowStrike(result.Item2, (StrikeType)result.Item3);
                SoundManager.Instance.PlayOnce(Consts.GameLost);
                UIManager.Instance.EnablePanel(rp.id);
                return;
            }
            else
            {
                if(TictactoeUtils.IsDraw(board)) //LAST TURN and no one won then show draw
                {   
                    EndGame();
                    rp = UIManager.Instance.allPanels[Consts.UIResultPanel] as UIResultPanel;
                    rp.ShowDraw();
                    UIManager.Instance.EnablePanel(rp.id);
                    return;
                }
                else
                {
                    if(AI != null && AI.botType != BotType.None)
                    {   
                        if(turnNum%2!=0)
                            AI.Move();
                    }
                }
            }
        }
    }

    public void SetBoardSize(int size)
    {
        TictactoeUtils.boardSize = size;
    }

    public void StartGame(GameMode mode , BotType botType)
    {
        BoardGenerator.Instance.Generate(TictactoeUtils.boardSize);
        InitializeBoard();

        vert.SetActive(false);
        hor.SetActive(false);
        diagLeft.SetActive(false);
        diagRight.SetActive(false);

        turnNum = 0;
        gameMode = mode;

        if(AI != null)
            AI.botType = botType;
    }
    public void EndGame()
    {
        gameStarted = false;
        UIManager.Instance.DisablePanel(Consts.UIGamePanel);
    }
    public void ShowStrike((int,int) position , StrikeType strikeType)
    {
        switch (strikeType)
        {
            case StrikeType.Vertical:
                vert.transform.localPosition = new Vector3(position.Item1 , position.Item2);
                vert.GetComponent<StrikeAnimator>().ResetInitPos();
                vert.SetActive(true);
                break;
            case StrikeType.Horizontal:
                hor.transform.localPosition = new Vector3(position.Item1 , position.Item2);
                hor.GetComponent<StrikeAnimator>().ResetInitPos();
                hor.SetActive(true);
                break;
            case StrikeType.DiagonalLeft:
                diagLeft.transform.localPosition = new Vector3(position.Item1 , position.Item2);
                diagLeft.GetComponent<StrikeAnimator>().ResetInitPos();
                diagLeft.SetActive(true);
                break;
            case StrikeType.DiagonalRight:
                diagRight.transform.localPosition = new Vector3(position.Item1 , position.Item2);
                diagRight.GetComponent<StrikeAnimator>().ResetInitPos();
                diagRight.SetActive(true);
                break;
            case StrikeType.None:
                break;
        }
    }
}