using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AgentManager : MonoBehaviour
{
    public TankManager[] m_Tanks;
    public ScoutManager[] m_Scouts;
    public GameObject[] m_Targets;
    public GameObject m_TankPrefab;
    public GameObject m_PathfinderPrefab;
    public GameObject m_TargetPrefab;

    private Genetique genetique;

    public TankManager[] GetTankManagers()
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
        for (int i = 0; i < m_Tanks.Length; i++)
        {            
            m_Tanks[i].m_Instance =
                Instantiate(
                    m_TankPrefab,
                    m_Tanks[i].m_SpawnPoint.position,
                    m_Tanks[i].m_SpawnPoint.rotation
                ) as GameObject;

            m_Scouts[i].m_Instance =
                Instantiate(
                    m_PathfinderPrefab,
                    m_Scouts[i].m_SpawnPoint.position,
                    m_Scouts[i].m_SpawnPoint.rotation
                ) as GameObject;

            m_Tanks[i].m_PlayerNumber = i + 1;
            m_Tanks[i].Setup();

            m_Scouts[i].m_PlayerNumber = i + 1;
            m_Scouts[i].Setup();
        }
    }

    public void setIntelligence(ActionGame[] ADNTireur, ActionGame[] ADNScout, Connaissances connaissances, int equipe)
    {
        m_Tanks[equipe].setIntelligence(ADNTireur, connaissances);
        m_Scouts[equipe].setIntelligence(ADNScout, connaissances);
    }

    private void SpawnAllTargets(int m_nbreTargets)
    {
        m_Targets = new GameObject[m_nbreTargets];

        for (int i = 0; i < m_nbreTargets; i++)
        {
            float x = Random.Range(-35.0f, 35.0f), y = Random.Range(-35.0f, 35.0f);

            if (x <= Math.Abs(x))
                x += 15;
            if (y <= Math.Abs(y))
                y += 15;

            Vector3 position = new Vector3(x, 0f, y);
            Quaternion quaternion = Quaternion.identity;

            m_Targets[i] = Instantiate(m_TargetPrefab, position, quaternion) as GameObject;
            m_Targets[i].SetActive(true);
        }
    }

    public GameObject[] GetAllGameObjects()
    {
        int i = 0;

        GameObject[] completeList = new GameObject[m_Tanks.Length + m_Scouts.Length + m_Targets.Length];

        foreach (TankManager tm in m_Tanks)
        {
            completeList[i] = tm.m_Instance;
            i++;
        }

        foreach (ScoutManager pfm in m_Scouts)
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