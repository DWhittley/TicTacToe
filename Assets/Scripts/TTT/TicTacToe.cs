using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TTT
{
    public class TicTacToe : MonoBehaviour
    {
        public GameObject xPrefab;
        public GameObject oPrefab;
        public GameBox[] gameBoxes; // Array of GameBox instances
        public GameObject resultPanel;    
        public Text resultText;
        public Button replayButton;
        public Button quitButton;

        private int[] board = new int[9];
        private bool[] cellClicked = new bool[9];
        private bool isPlayerTurn = true;
        public bool gameOver;
        private int currentPlayer = 1;

        private void Start()
        {
            gameOver = false;
            resultPanel.SetActive(false);
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

        private void ResetGame() // Clear the board and cellClicked array
        {
            Array.Clear(board, 0, board.Length);
            Array.Clear(cellClicked, 0, cellClicked.Length);
            gameOver = false;
            currentPlayer = 1;
        }

        public bool IsCellClickable(int index)
        {
            return !cellClicked[index]; // Check if the cell not occupied by X or O
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
            GameObject piece = Instantiate(prefab, position, Quaternion.identity); // Instantiate the new piece GameObject at the specified position
        }

        private void PostResults(int winner)
        {
            string resultMessage;
            if (winner == 0)
            {
                resultMessage = "Tied that one!";
            }
            else if (winner == 1)
            {
                resultMessage = "You Win!";
            }
            else
            {
                resultMessage = "Computer Won!";
            }

            resultText.text = resultMessage;
            resultPanel.SetActive(true);

            replayButton.onClick.AddListener(OnReplayClicked); // listener for the replay and quit buttons
            quitButton.onClick.AddListener(OnQuitClicked);
        }

        private void OnReplayClicked()
        {
            resultPanel.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnQuitClicked()
        {
            gameOver = true;
            Application.Quit(); // Quit the application in standalone build
        }

        private void MakeAIMove()
        {
            int bestMove = TicTackToeAI.FindBestMove(board);

            if (bestMove != -1) // AI chose a valid move
            {
                MakeMove(bestMove);
                isPlayerTurn = true;
            }
            else if (!Array.Exists(cellClicked, clicked => !clicked)) // AI chose a move resulting in a tie
            {
                gameOver = true;
                PostResults(0);
            }
        }
    }
}