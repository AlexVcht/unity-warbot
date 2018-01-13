using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScoreGUI : MonoBehaviour
{
    [HideInInspector] public AgentManager m_AgentManager;

    private string scoreUI;

   /* public void OnGUI()
    {
        scoreUI = string.Empty;

        foreach (TankManager tank in teamManager.m_Tanks)
        {
            scoreUI += tank.m_ColoredPlayerText + " : " + tank.m_TargetsKilled + "\n";
        }

        GUI.Label(new Rect(10, Screen.height - 40, 75, 50), scoreUI);
    }

    public void Update()
    {
        scoreUI = string.Empty;

        foreach (TankManager tank in teamManager.m_Tanks)
        {
            scoreUI += tank.m_ColoredPlayerText + " : " + tank.m_TargetsKilled + "\n";
        }
    }*/
}