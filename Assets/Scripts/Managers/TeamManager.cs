using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public TankManager[] m_Tanks;
    public PathfinderManager[] m_Pathfinders;

    public GameObject m_TankPrefab;
    public GameObject m_PathfinderPrefab;

    public void Setup()
    {
    }


    public void SpawnAllTanksAndPathfinder()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].m_Instance =
                Instantiate(
                    m_TankPrefab,
                    m_Tanks[i].m_SpawnPoint.position,
                    m_Tanks[i].m_SpawnPoint.rotation
                ) as GameObject;

            m_Pathfinders[i].m_Instance =
                Instantiate(
                    m_PathfinderPrefab,
                    m_Tanks[i].m_SpawnPoint.position,
                    m_Tanks[i].m_SpawnPoint.rotation
                ) as GameObject;

            m_Tanks[i].m_PlayerNumber = i + 1;
            m_Tanks[i].Setup();

            m_Pathfinders[i].m_PlayerNumber = i + 1;
            m_Pathfinders[i].Setup();
        }
    }

    public GameObject[] GetAllGameObjects()
    {
        int i = 0;

        GameObject[] completeList = new GameObject[m_Tanks.Length + m_Pathfinders.Length];

        foreach (TankManager tm in m_Tanks)
        {
            completeList[i] = tm.m_Instance;
            i++;
        }

        foreach (PathfinderManager pfm in m_Pathfinders)
        {
            completeList[i] = pfm.m_Instance;
            i++;
        }

        return completeList;
    }
}