using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace TTT
{
    public class TicTacToe : MonoBehaviour
    {
        public GameObject xPrefab;
        public GameObject oPrefab;

        private int[] board = new int[9];
        private List<GameObject> pieces = new List<GameObject>();
        private int moves = 0;
        private int[] score;
        public bool gameOver;
        private int currentPlayer = 1;
        private Transform[] boxTransforms; // Array to store the transforms of the boxes

        private void Start()
        {
            gameOver = false;
            StartPlay();
        }

        private void UpdateBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == 1)
                {
                    if (pieces[i] == null)
                    {
                        pieces[i] = Instantiate(xPrefab, GetPositionFromIndex(i), Quaternion.identity);
                        pieces[i].transform.SetParent(transform); // Set the parent to the TicTacToe object
                    }
                }
                else if (board[i] == -1)
                {
                    if (pieces[i] == null)
                    {
                        pieces[i] = Instantiate(oPrefab, GetPositionFromIndex(i), Quaternion.identity);
                        pieces[i].transform.SetParent(transform); // Set the parent to the TicTacToe object
                    }
                }
            }
        }

        private Vector3 GetPositionFromIndex(int index)
        {
            if (index >= 0 && index < pieces.Count && pieces[index] != null)
            {
                return pieces[index].GetComponent<CellMarker>().GetPositionFromBox();
            }
            else
            {
                // Return a default position (you may want to adjust this based on your game's requirements)
                return Vector3.zero;
            }
        }

        private void ResetGame()
        {
            // Clear the pieces list and reset the board
            pieces.Clear();
            for (int i = 0; i < 9; i++)
            {
                board[i] = 0;
            }
            gameOver = false;
            moves = 0;
            currentPlayer = 1;
        }

        private void StartPlay()
        {

            // Reset the game and display the initial board
            ResetGame();

            // Instantiate the cells and assign their box transforms to the CellMarker script
            CellMarker[] cellMarkers = FindObjectsOfType<CellMarker>();
            for (int i = 0; i < cellMarkers.Length; i++)
            {
                cellMarkers[i].ticTacToe = this;
            }
        }

        private void MakeMove(int index)
        {
            if (!gameOver && board[index] == 0)
            {
                board[index] = currentPlayer;
                moves++;
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
            GameObject piece = Instantiate(prefab, position, Quaternion.identity);
            pieces.Add(piece);
        }

        private int EvaluateBoard()
        {
            // evaluate board for winner or tie
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

        public bool IsCellClickable(int index)
        {
            // Check if the cell is clickable (not occupied by X or O)
            return board[index] == 0;
        }

        public void OnCellClicked(int cellIndex)
        {
            if (!gameOver)
            {
                MakeMove(cellIndex);
            }
        }

        private void MakeAIMove()
        {
            int bestMove = TicTackToeAI.FindBestMove(board);

            if (bestMove != -1) // AI chose a valid move
            {
                MakeMove(bestMove);
            }
            else if (moves >= 9) // AI chose a move resulting in a tie
            {
                gameOver = true;
                PostResults(0);
                AskReplayOrQuit();
            }
        }
    }
}