using UnityEngine;

namespace TTT
{
    public class CellMarker : MonoBehaviour
    {
        public TicTacToe ticTacToe;
        public int boxNumber; // Assign a unique number (1 to 9) for each box

        public Transform box1Transform;
        public Transform box2Transform;
        public Transform box3Transform;
        public Transform box4Transform;
        public Transform box5Transform;
        public Transform box6Transform;
        public Transform box7Transform;
        public Transform box8Transform;
        public Transform box9Transform;

        public int box1Number = 1;
        public int box2Number = 2;
        public int box3Number = 3;
        public int box4Number = 4;
        public int box5Number = 5;
        public int box6Number = 6;
        public int box7Number = 7;
        public int box8Number = 8;
        public int box9Number = 9;

        // Method to get the box transform based on the box number
        private Transform GetBoxTransform(int boxNumber)
        {
            switch (boxNumber)
            {
                case 1:
                    Debug.Log("cell 1 clicked");
                    return box1Transform;
                case 2:
                    return box2Transform;
                case 3: 
                    return box3Transform;
                case 4: 
                    return box4Transform;
                case 5: 
                    return box5Transform;
                case 6: 
                    return box6Transform;
                case 7: 
                    return box7Transform;
                case 8: 
                    return box8Transform;
                case 9: 
                    return box9Transform;

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
                Transform boxTransform = GetBoxTransform(boxNumber);
                if (boxTransform != null)
                {
                    ticTacToe.OnCellClicked(boxNumber - 1);
                }
            }
        }

        public Vector3 GetPositionFromBox()
        {
            switch (boxNumber)
            {
                case 1:
                    Debug.Log("cell 1 position passed");
                    return box1Transform.position;
                case 2:
                    return box2Transform.position;
                case 3:
                    return box3Transform.position;
                case 4:
                    return box4Transform.position;
                case 5:
                    return box5Transform.position;
                case 6:
                    return box6Transform.position;
                case 7:
                    return box7Transform.position;
                case 8:
                    return box8Transform.position;
                case 9:
                    return box9Transform.position;
                default:
                    return Vector3.zero; // Return zero if the boxNumber is invalid.
            }
        }
    }
}