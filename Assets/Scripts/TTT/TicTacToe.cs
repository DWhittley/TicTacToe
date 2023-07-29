using System;
using UnityEngine;

namespace TTT
{
    public class TicTacToe : MonoBehaviour
    {
        public GameObject xPrefab;
        public GameObject oPrefab;

        private int[] board = new int[9];
        private bool[] cellClicked = new bool[9]; // To track if each cell has been clicked
        private bool isPlayerTurn = true; // To track whether it's the player's turn or AI's turn
        public bool gameOver;
        private int currentPlayer = 1;

        private void Start()
        {
            gameOver = false;
            StartPlay();
        }

        private Vector3 GetPositionFromIndex(int index)
        {
            // Get the individual box GameObject based on its name ("box1", "box2", ... "box9")
            GameObject box = GameObject.Find("box" + (index + 1));

            if (box != null)
            {
                // Return the transform position of the individual box
                return box.transform.position;
            }
            else
            {
                // Return a default position (you may want to adjust this based on your game's requirements)
                return Vector3.zero;
            }
        }

        private void ResetGame()
        {
            // Clear the board and cellClicked array
            Array.Clear(board, 0, board.Length);
            Array.Clear(cellClicked, 0, cellClicked.Length);
            gameOver = false;
            currentPlayer = 1;
        }

        private void StartPlay()
        {
            // Reset the game and display the initial board
            ResetGame();
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

                int winner = EvaluateBoard();
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

        private int EvaluateBoard()
        {
            // Evaluate board for winner or tie
            // Your logic to check for a winner or tie should be implemented here
            return 0;
        }

        private void PostResults(int winner)
        {
            // TODO: Implement logic to display the game results
        }

        private void AskReplayOrQuit()
        {
            // TODO: Implement logic to ask if the players want to replay or quit
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