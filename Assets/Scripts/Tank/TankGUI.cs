using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGUI : MonoBehaviour
{
    public Transform tankPosition;
    public string score;

    // Use this for initialization
    private void OnGUI()
    {
        GUI.Label(new Rect(tankPosition.position.x, tankPosition.position.y, 50, 50), score);
    }
}