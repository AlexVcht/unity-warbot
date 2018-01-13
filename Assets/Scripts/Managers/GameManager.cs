using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_NumRoundsToWin = 5;
    public int m_NumTargets = 5;
    public float m_StartDelay = 2f;
    public float m_EndDelay = 2f;
    public CameraControl m_CameraControl;
    public Text m_MessageText;
    public GameObject m_TargetPrefab;

    private int m_RoundNumber;
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;
    private TankManager m_RoundWinner;
    private TankManager m_GameWinner;
    private GameObject[] targets;
    private ScoreGUI scoreGUI;
    private TeamManager teamManager;

    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        teamManager = GetComponent<TeamManager>();

        SpawnAllTargets();
        teamManager.SpawnAllTanksAndPathfinder();

        SetScoreUI();

        SetCameraTargets();

        StartCoroutine(GameLoop());
    }

    private void SpawnAllTargets()
    {
        targets = new GameObject[m_NumTargets];

        for (int i = 0; i < m_NumTargets; i++)
        {
            Vector3 position = new Vector3(Random.Range(-35.0f, 35.0f), 0f, Random.Range(-35.0f, 35.0f));
            Quaternion quaternion = Quaternion.identity;

            targets[i] = Instantiate(m_TargetPrefab, position, quaternion) as GameObject;
            targets[i].SetActive(true);
        }
    }
    
    private void SetScoreUI()
    {
        ScoreGUI scoreGui = GetComponent<ScoreGUI>();

        scoreGui.teamManager = teamManager;
    }

    // On set la camera pour voir tank + targets
    private void SetCameraTargets()
    {
        int iterator = 0;
        GameObject[] allGameObjects = teamManager.GetAllGameObjects();
        int size = allGameObjects.Length + m_NumTargets;
        Transform[] tranformTargets = new Transform[size];

        foreach (GameObject go in allGameObjects)
        {
            tranformTargets[iterator] = go.transform;
            iterator++;
        }

        foreach (GameObject target in targets)
        {
            tranformTargets[iterator] = target.transform;
            iterator++;
        }

        m_CameraControl.m_Targets = tranformTargets;
    }


    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (m_GameWinner != null)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator RoundStarting()
    {
        ResetAll();
        DisableControl();

        m_CameraControl.SetStartPositionAndSize();

        m_RoundNumber++;
        m_MessageText.text = "Generation " + m_RoundNumber;

        yield return m_StartWait;
    }


    private IEnumerator RoundPlaying()
    {
        EnableControl();

        m_MessageText.text = string.Empty;

        while (!IsTargetsAlive())
        {
            RespawnTanksIfDead();
            yield return null;
        }
    }


    private IEnumerator RoundEnding()
    {
        DisableControl();

        m_RoundWinner = null;

        m_RoundWinner = GetRoundWinner();

        if (m_RoundWinner != null)
        {
            m_RoundWinner.m_Wins++;
        }

        m_GameWinner = GetGameWinner();

        string message = EndMessage();
        m_MessageText.text = message;

        yield return m_EndWait;
    }


    private bool IsTargetsAlive()
    {
        int nbTargetsAlive = 0;

        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].activeSelf)
                nbTargetsAlive++;
        }

        return nbTargetsAlive < 1;
    }

    private void RespawnTanksIfDead()
    {
        foreach (TankManager tank in teamManager.m_Tanks)
        {
            if (!tank.m_Instance.activeSelf)
            {
                if (tank.m_TargetsKilled > 0)
                    tank.m_TargetsKilled--;
                tank.Reset();
            }
        }
    }

    private TankManager GetRoundWinner()
    {
        TankManager tankWinner = null;

        for (int i = 0; i < teamManager.m_Tanks.Length - 1; i++)
        {
            if (teamManager.m_Tanks[i].m_TargetsKilled > teamManager.m_Tanks[i + 1].m_TargetsKilled)
                tankWinner = teamManager.m_Tanks[i];
        }

        return tankWinner;
    }


    private TankManager GetGameWinner()
    {
        for (int i = 0; i < teamManager.m_Tanks.Length; i++)
        {
            if (teamManager.m_Tanks[i].m_Wins == m_NumRoundsToWin)
                return teamManager.m_Tanks[i];
        }

        return null;
    }


    private string EndMessage()
    {
        string message = "DRAW!";

        if (m_RoundWinner != null)
            message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

        message += "\n\n\n\n";

        for (int i = 0; i < teamManager.m_Tanks.Length; i++)
        {
            message += teamManager.m_Tanks[i].m_ColoredPlayerText + ": " + teamManager.m_Tanks[i].m_Wins + " WINS\n";
        }

        if (m_GameWinner != null)
            message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

        return message;
    }

    private void ResetAll()
    {
        foreach (TankManager tm in teamManager.m_Tanks)
        {
            tm.Reset();
        }

        foreach (var target in targets)
        {
            target.SetActive(true);
        }

        foreach (PathfinderManager pfm in teamManager.m_Pathfinders)
        {
            pfm.Reset();
        }

    }

    private void EnableControl()
    {
        foreach (TankManager tm in teamManager.m_Tanks)
        {
            tm.EnableControl();
        }

        foreach (PathfinderManager pfm in teamManager.m_Pathfinders)
        {
            pfm.EnableControl();
        }
    }


    private void DisableControl()
    {
        foreach (TankManager tm in teamManager.m_Tanks)
        {
            tm.DisableControl();
        }

        foreach (PathfinderManager pfm in teamManager.m_Pathfinders)
        {
            pfm.DisableControl();
        }
    }
}