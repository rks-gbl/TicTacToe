using UnityEngine;

namespace RitikUtils
{
    public class TictactoeUtils
    {
        public const char PLAYER_X = 'X';
        public const char PLAYER_O = 'O';
        public const char EMPTY = ' ';
        public static int boardSize; 
        public static int matchSize;

        public static void InitializeBoard(ref char[,] board, int _boardSize=-1)
        {
            if(_boardSize == -1)
                return;

            boardSize = _boardSize;
            
            if(boardSize > 4)
                matchSize = boardSize - 1;

            board = new char[boardSize , boardSize];

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    board[i, j] = EMPTY;
                }
            }
        }
        public static (bool,(int,int),int) IsWinner(char[,] board, char player)
        {
            // Check rows, columns, and diagonals for sequences of 3
            for (int i = 0; i < boardSize; i++)
            {
                // Check rows
                for (int j = 0; j <= boardSize - 3; j++)
                {
                    if (board[i, j] == player && board[i, j + 1] == player && board[i, j + 2] == player)
                        return (true,(i,j+1),1);
                }
                
                // Check columns
                for (int j = 0; j <= boardSize - 3; j++)
                {
                    if (board[j, i] == player && board[j + 1, i] == player && board[j + 2, i] == player)
                        return (true,(j+1,i),2);
                }
            }

            // Check diagonals
            for (int i = 0; i <= boardSize - 3; i++)
            {
                for (int j = 0; j <= boardSize - 3; j++)
                {
                    // Top-right to bottom-left diagonal
                    if (board[i, j] == player && board[i + 1, j + 1] == player && board[i + 2, j + 2] == player)
                        return (true,(i+1,j+1),4);
                    
                    // Bottom-right to top-left diagonal
                    if (board[i + 2, j] == player && board[i + 1, j + 1] == player && board[i, j + 2] == player)
                        return (true,(i+1,j+1),3);
                }
            }

            return (false,(-1,-1),0);
        }
        public static (int,int) GetWinningBox(char[,] board , char player)
        {
            // Check rows, columns, and diagonals for sequences of 3
            for (int i = 0; i < boardSize; i++)
            {
                // Check rows
                for (int j = 0; j <= boardSize - 3; j++)
                {
                    if (board[i, j] == player && board[i, j + 1] == player && board[i, j + 2] == EMPTY)
                        return (i,j + 2);
                    if (board[i, j] == player && board[i, j + 1] == EMPTY && board[i, j + 2] == player)
                        return (i,j + 1);
                    if (board[i, j] == EMPTY && board[i, j + 1] == player && board[i, j + 2] == player)
                        return (i,j);
                }
                
                // Check columns
                for (int j = 0; j <= boardSize - 3; j++)
                {
                    if (board[j, i] == player && board[j + 1, i] == player && board[j + 2, i] == EMPTY)
                        return (j + 2,i);
                    if (board[j, i] == player && board[j + 1, i] == EMPTY && board[j + 2, i] == player)
                        return (j + 1,i);
                    if (board[j, i] == EMPTY && board[j + 1, i] == player && board[j + 2, i] == player)
                        return (j,i);
                }
            }

            // Check diagonals
            for (int i = 0; i <= boardSize - 3; i++)
            {
                for (int j = 0; j <= boardSize - 3; j++)
                {
                    if (board[i, j] == player && board[i + 1, j + 1] == player && board[i + 2, j + 2] == EMPTY)
                        return (i + 2 , j + 2);
                    if (board[i, j] == player && board[i + 1, j + 1] == EMPTY && board[i + 2, j + 2] == player)
                        return (i + 1 , j + 1);
                    if (board[i, j] == EMPTY && board[i + 1, j + 1] == player && board[i + 2, j + 2] == player)
                        return (i , j);

                    if (board[i + 2, j] == player && board[i + 1, j + 1] == player && board[i, j + 2] == EMPTY)
                        return (i,j + 2);
                    if (board[i + 2, j] == player && board[i + 1, j + 1] == EMPTY && board[i, j + 2] == player)
                        return (i + 1,j + 1);
                    if (board[i + 2, j] == EMPTY && board[i + 1, j + 1] == player && board[i, j + 2] == player)
                        return (i + 2,j);
                }
            }

            return (-1,-1);
        }
        public static bool IsDraw(char[,] board)
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (board[i, j] == EMPTY)
                        return false;
                }
            }
            return !IsWinner(board, PLAYER_X).Item1 && !IsWinner(board, PLAYER_O).Item1;
        }

        public static (int, int) FindBestMove(char[,] board, char player)
        {
            int bestMoveRow = -1;
            int bestMoveCol = -1;
            int bestScore = int.MinValue;

            iterations = 0;

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (board[i, j] == EMPTY)
                    {
                        board[i, j] = player;
                        int moveScore = Minimax(board, 0, false, int.MinValue, int.MaxValue);
                        Debug.Log($"({i},{j}) : {moveScore}");
                        board[i, j] = EMPTY;

                        if (moveScore > bestScore)
                        {
                            bestScore = moveScore;
                            bestMoveRow = i;
                            bestMoveCol = j;
                        }
                    }
                }
            }
            Debug.Log("Total Iterations for last move : "+iterations);
            return (bestMoveRow, bestMoveCol);
        }
        private static int EvaluateBoard(char[,] board)
        {
            if (IsWinner(board, PLAYER_O).Item1) return 10;
            if (IsWinner(board, PLAYER_X).Item1) return -10;
            return 0;
        }

        static int iterations=0;
        private static int Minimax(char[,] board, int depth, bool isMaximizing, int alpha, int beta)
        {
            int score = EvaluateBoard(board);

            if (score == 10) return score - depth;
            if (score == -10) return score + depth;
            if (IsDraw(board)) return 0;

            if (isMaximizing)
            {
                int bestScore = int.MinValue;

                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        if (board[i, j] == EMPTY)
                        {
                            board[i, j] = PLAYER_O;
                            iterations++;
                            bestScore = Mathf.Max(bestScore, Minimax(board, depth + 1, false, alpha, beta));
                            board[i, j] = EMPTY;

                            alpha = Mathf.Max(alpha, bestScore);
                            if (beta <= alpha)
                            {
                                return bestScore;
                            }
                        }
                    }
                }
                            
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;

                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        if (board[i, j] == EMPTY)
                        {
                            board[i, j] = PLAYER_X;
                            iterations++;
                            bestScore = Mathf.Min(bestScore, Minimax(board, depth + 1, true, alpha, beta));
                            board[i, j] = EMPTY;

                            beta = Mathf.Min(beta, bestScore);
                            if (beta <= alpha)
                            {
                                return bestScore;
                            }
                        }
                    }
                }
                return bestScore;
            }
        }
    }
}