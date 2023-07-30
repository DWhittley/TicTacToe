using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TTT
{
    public class TicTackToeAI : MonoBehaviour
    {
        private const int BoardSize = 3;
        private const int MaxDepth = 9; // Maximum depth for the Minimax algorithm

        // Minimax function
        public static int Minimax(int[] board, int depth, int player)
        {
            int winner = EvaluateBoard(board);
            if (winner != 0 || depth == MaxDepth)
            {
                return winner == 1 ? 10 - depth : (winner == -1 ? depth - 10 : 0);
            }

            int bestScore;
            if (player == 1)
            {
                bestScore = int.MinValue;
                for (int i = 0; i < BoardSize * BoardSize; i++)
                {
                    if (board[i] == 0)
                    {
                        board[i] = 1;
                        int currentScore = Minimax(board, depth + 1, 2);
                        board[i] = 0; // Undo the move
                        bestScore = Mathf.Max(bestScore, currentScore);
                    }
                }
            }
            else
            {
                bestScore = int.MaxValue;
                for (int i = 0; i < BoardSize * BoardSize; i++)
                {
                    if (board[i] == 0)
                    {
                        board[i] = 2;
                        int currentScore = Minimax(board, depth + 1, 1);
                        board[i] = 0; // Undo the move
                        bestScore = Mathf.Min(bestScore, currentScore);
                    }
                }
            }

            return bestScore;
        }

        // Function to find the best move for the AI using the Minimax algorithm
        public static int FindBestMove(int[] board)
        {
            // Check if the center box is available and select it as the best move
            if (board[4] == 0)
            {
                return 4;
            }

            int bestScore = int.MinValue;
            int bestMove = -1;

            for (int i = 0; i < BoardSize * BoardSize; i++)
            {
                if (board[i] == 0)
                {
                    board[i] = 1;
                    int currentScore = Minimax(board, 0, 2);
                    board[i] = 0; // Undo the move

                    if (currentScore > bestScore)
                    {
                        bestScore = currentScore;
                        bestMove = i;
                    }
                }
            }

            return bestMove;
        }

        // Function to evaluate the current state of the board and return the winner (1 for X, -1 for O, 0 for tie)
        public static int EvaluateBoard(int[] board)
        {
            int[][] winConditions = new int[][]
            {
                new int[] {0, 1, 2}, // Row 1
                new int[] {3, 4, 5}, // Row 2
                new int[] {6, 7, 8}, // Row 3
                new int[] {0, 3, 6}, // Column 1
                new int[] {1, 4, 7}, // Column 2
                new int[] {2, 5, 8}, // Column 3
                new int[] {0, 4, 8}, // Diagonal 1
                new int[] {2, 4, 6}  // Diagonal 2
            };

            for (int i = 0; i < winConditions.Length; i++)
            {
                int a = winConditions[i][0];
                int b = winConditions[i][1];
                int c = winConditions[i][2];

                if (board[a] != 0 && board[a] == board[b] && board[b] == board[c])
                {
                    return board[a];
                }
            }

            if (!board.Contains(0)) // Check for a tie
            {
                return 0;
            }

            return -1; // Game is still ongoing
        }
    }
}
