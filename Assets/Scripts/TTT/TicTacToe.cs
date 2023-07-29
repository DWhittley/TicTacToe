using UnityEngine;
using System;

namespace TTT
{
    public class TicTacToe : MonoBehaviour
    {
        public GameObject xPrefab;
        public GameObject oPrefab;
        public GameBox[] gameBoxes; // Array of GameBox instances

        private int[] board = new int[9];
        private bool[] cellClicked = new bool[9];
        private bool isPlayerTurn = true;
        public bool gameOver;
        private int currentPlayer = 1;

        private void Start()
        {
            gameOver = false;
            StartPlay();
        }

        private void StartPlay()
        {
            ResetGame();
        }

        private Vector3 GetPositionFromIndex(int index)
        {
            return gameBoxes[index].boxTransform.position;
        }

        private void ResetGame()
        {
            // Clear the board and cellClicked array
            Array.Clear(board, 0, board.Length);
            Array.Clear(cellClicked, 0, cellClicked.Length);
            gameOver = false;
            currentPlayer = 1;
        }

        public bool IsCellClickable(int index)
        {
            // Check if the cell is clickable (not occupied by X or O)
            return !cellClicked[index];
        }

        public void OnCellClicked(int cellIndex, Transform boxTransform)
        {
            if (!gameOver && isPlayerTurn && IsCellClickable(cellIndex))
            {
                cellClicked[cellIndex] = true; // Mark the cell as clicked
                isPlayerTurn = false; // Switch to AI's turn
                MakeMove(cellIndex);
            }
        }

        public void MakeMove(int index)
        {
            if (!gameOver && board[index] == 0)
            {
                board[index] = currentPlayer;
                InstantiatePiece(currentPlayer == 1 ? xPrefab : oPrefab, GetPositionFromIndex(index));

                int winner = TicTackToeAI.EvaluateBoard(board);
                if (winner != -1)
                {
                    gameOver = true;
                    PostResults(winner);
                    AskReplayOrQuit();
                }
                else
                {
                    currentPlayer = currentPlayer == 1 ? 2 : 1;
                    if (currentPlayer == 2)
                    {
                        MakeAIMove();
                    }
                }
            }
        }

        private void InstantiatePiece(GameObject prefab, Vector3 position)
        {
            // Instantiate the new piece GameObject at the specified position
            GameObject piece = Instantiate(prefab, position, Quaternion.identity);
        }

        private void PostResults(int winner)
        {
            if (winner == 0)
            {
                // post game is a tie
                return;
            }
            else
            {
                // post computer wins
                return;
            }
        }

        private void AskReplayOrQuit()
        {
            // show text element "replay?"
            // show buttons "replay" and "quit"
            // if replay then StartPlay();
            // if quit then end game.
        }

        private void MakeAIMove()
        {
            int bestMove = TicTackToeAI.FindBestMove(board);

            if (bestMove != -1) // AI chose a valid move
            {
                MakeMove(bestMove);
            }
            else if (!Array.Exists(cellClicked, clicked => !clicked)) // AI chose a move resulting in a tie
            {
                gameOver = true;
                PostResults(0);
                AskReplayOrQuit();
            }
        }
    }
}