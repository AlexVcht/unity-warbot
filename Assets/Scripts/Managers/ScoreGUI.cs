using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScoreGUI : MonoBehaviour
{
    [HideInInspector] public AgentManager m_AgentManager;

    private string scoreUI;

    public void OnGUI()
    {
        scoreUI = string.Empty;

        foreach (TankManager tank in m_AgentManager.m_Tanks)
        {
            scoreUI += "<b>Targets hit</b>" + " : " + tank.m_TargetsKilled + "\n" +
                       "<b>Time</b> : " + Environment.TickCount;
        }

        GUI.Label(new Rect(10, Screen.height - 40, 100, 50), scoreUI);
    }

    public void Update()
    {
        scoreUI = string.Empty;

        foreach (TankManager tank in m_AgentManager.m_Tanks)
        {
            scoreUI += tank.m_ColoredPlayerText + " : " + tank.m_TargetsKilled + "\n" + 
                " Time : " + Environment.TickCount;
        }
    }
}