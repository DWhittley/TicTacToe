using System.Linq;
using UnityEngine;
using UnityEngine;

namespace TTT
{
    public class CellMarker : MonoBehaviour
    {
        public TicTacToe ticTacToe;
        public int boxNumber; // Assign a unique number (1 to 9) for each box

        private void Start()
        {
            ticTacToe = FindObjectOfType<TicTacToe>();
        }

        private void OnMouseDown()
        {
            if (!ticTacToe.gameOver && ticTacToe.IsCellClickable(boxNumber - 1)) // Check if cell is unoccupied and game is still ongoing
            {
                GameBox box = ticTacToe.gameBoxes.FirstOrDefault(b => b.boxNumber == boxNumber); // Find corresponding box in array
                if (box != null)
                {
                    Transform boxTransform = box.boxTransform; // Get the clicked box's transform
                    ticTacToe.OnCellClicked(box.boxNumber - 1, boxTransform); // Pass the transform to TicTacToe
                }
            }
        }
    }
}