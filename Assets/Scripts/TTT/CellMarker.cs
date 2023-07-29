using UnityEngine;
using UnityEngine.UI;

namespace TTT
{
    public class CellMarker : MonoBehaviour
    {
        public TicTacToe ticTacToe;
        public int boxNumber; // Assign a unique number (1 to 9) for each box

        private void Start ()
        {
            ticTacToe = FindObjectOfType<TicTacToe>();
        }

        public Vector3 GetPositionFromBox()
        {
            return transform.position;
        }

        private void OnMouseDown()
        {
            Debug.Log("Cell clicked");
            // Check if the cell is clickable (not occupied by X or O) and the game is still ongoing
            if (!ticTacToe.gameOver && ticTacToe.IsCellClickable(boxNumber - 1))
            {
                Debug.Log("Passing cell# " + (boxNumber -1) + " to tictactoe");
                ticTacToe.OnCellClicked(boxNumber - 1);
            }
        }
    }
}