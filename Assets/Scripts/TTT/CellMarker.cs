using UnityEngine;

namespace TTT
{
    public class CellMarker : MonoBehaviour
    {
        public TicTacToe ticTacToe;
        public int boxNumber; // Assign a unique number (1 to 9) for each box

        // Method to get the box transform based on the box number
        private Transform GetBoxTransform()
        {
            switch (boxNumber)
            {
                case 1:
                    Debug.Log("cell 1 clicked");
                    return transform;
                case 2:
                    return transform;
                case 3:
                    return transform;
                case 4:
                    return transform;
                case 5:
                    return transform;
                case 6:
                    return transform;
                case 7:
                    return transform;
                case 8:
                    return transform;
                case 9:
                    return transform;
                default:
                    return null;
            }
        }

        private void OnMouseDown()
        {
            // Check if the cell is clickable (not occupied by X or O) and the game is still ongoing
            Debug.Log("cell clicked");
            if (!ticTacToe.gameOver && ticTacToe.IsCellClickable(boxNumber - 1))
            {
                Transform boxTransform = GetBoxTransform();
                if (boxTransform != null)
                {
                    ticTacToe.OnCellClicked(boxNumber - 1);
                }
            }
        }

        public Vector3 GetPositionFromBox()
        {
            return transform.position;
        }
    }
}