using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AgentManager : MonoBehaviour
{
    public TankManager[] m_Tanks;
    public PathfinderManager[] m_Pathfinders;
    public GameObject[] m_Targets;

    public GameObject m_TankPrefab;
    public GameObject m_PathfinderPrefab;
    public GameObject m_TargetPrefab;

    public void SpawnAgents(int nbreTargets)
    {
        SpawnAllTargets(nbreTargets);

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
                    m_Pathfinders[i].m_SpawnPoint.position,
                    m_Pathfinders[i].m_SpawnPoint.rotation
                ) as GameObject;

            m_Tanks[i].m_PlayerNumber = i + 1;
            m_Tanks[i].Setup();

            m_Pathfinders[i].m_PlayerNumber = i + 1;
            m_Pathfinders[i].Setup();
        }
    }

    private void SpawnAllTargets(int nbreTargets)
    {
        m_Targets = new GameObject[nbreTargets];

        for (int i = 0; i < nbreTargets; i++)
        {
            Vector3 position = new Vector3(Random.Range(-35.0f, 35.0f), 0f, Random.Range(-35.0f, 35.0f));
            Quaternion quaternion = Quaternion.identity;

            m_Targets[i] = Instantiate(m_TargetPrefab, position, quaternion) as GameObject;
            m_Targets[i].SetActive(true);
        }
    }

    public GameObject[] GetAllGameObjects()
    {
        int i = 0;

        GameObject[] completeList = new GameObject[m_Tanks.Length + m_Pathfinders.Length + m_Targets.Length];

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

        foreach (GameObject go in m_Targets)
        {
            completeList[i] = go;
            i++;
        }

        return completeList;
    }
}