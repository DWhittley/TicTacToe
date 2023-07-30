using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameBox: MonoBehaviour
{
    public int boxNumber;
    public Transform boxTransform;
    public GameObject boxObject; // establish object for Prefab instantiation

    public GameBox(int number, Transform transform)
    {
        boxNumber = number;
        boxTransform = transform;
        boxObject = null; // Set the initial prefab value to null
    }
}
