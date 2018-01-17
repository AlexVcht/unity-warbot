using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AgentManager : MonoBehaviour
{
    public TankManager m_Tanks;
    public ScoutManager m_Scouts;
    public GameObject[] m_Targets;
    public GameObject m_TankPrefab;
    public GameObject m_PathfinderPrefab;
    public GameObject m_TargetPrefab;

    private Genetique genetique;

    public TankManager GetTankManager()
    {
        return m_Tanks;
    }

    public void InitAgents(int nbreTargets)
    {
        SpawnAllTargets(nbreTargets);

        SpawnAgents();
    }

    public void SpawnAgents()
    {
        m_Tanks.m_Instance =
            Instantiate(
                m_TankPrefab,
                m_Tanks.m_SpawnPoint.position,
                m_Tanks.m_SpawnPoint.rotation
            ) as GameObject;

        m_Scouts.m_Instance =
            Instantiate(
                m_PathfinderPrefab,
                m_Scouts.m_SpawnPoint.position,
                m_Scouts.m_SpawnPoint.rotation
            ) as GameObject;

        m_Tanks.m_PlayerNumber = 1;
        m_Tanks.Setup();

        m_Scouts.m_PlayerNumber = 1;
        m_Scouts.Setup();
    }

    public void setIntelligence(ActionGame[] ADNTireur, ActionGame[] ADNScout, Connaissances connaissances)
    {
        m_Tanks.setIntelligence(ADNTireur, connaissances);
        m_Scouts.setIntelligence(ADNScout, connaissances);
    }

    private void SpawnAllTargets(int m_nbreTargets)
    {
        m_Targets = new GameObject[m_nbreTargets];

        for (int i = 0; i < m_nbreTargets; i++)
        {
            Vector3 position = new Vector3(Random.Range(-35.0f, 35.0f), 0f, Random.Range(-35.0f, 35.0f));
            Quaternion quaternion = Quaternion.identity;

            m_Targets[i] = Instantiate(m_TargetPrefab, position, quaternion) as GameObject;
            m_Targets[i].SetActive(true);
        }
    }

    public GameObject[] GetAllGameObjects()
    {
        int i = 2;

        GameObject[] completeList = new GameObject[2 + m_Targets.Length];

        completeList[0] = m_Tanks.m_Instance;
        completeList[1] = m_Scouts.m_Instance;

        foreach (GameObject go in m_Targets)
        {
            completeList[i] = go;
            i++;
        }

        return completeList;
    }
}