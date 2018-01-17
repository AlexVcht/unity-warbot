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

    private int m_RoundNumber;
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;
    private TankManager m_RoundWinner;
    private TankManager m_GameWinner;
    private ScoreGUI scoreGUI;
    private AgentManager agentManager;
    private Genetique genetique;

    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);
 
        agentManager = GetComponent<AgentManager>();
        agentManager.InitAgents(m_NumTargets);

        genetique = new Genetique(100, 5, 0.5f);

        SetScoreUI();

        SetCameraTargets();

        StartCoroutine(GameLoop());
    }

    private void SetScoreUI()
    {
        ScoreGUI scoreGui = GetComponent<ScoreGUI>();

        scoreGui.m_AgentManager = agentManager;
    }

    // On set la camera pour voir tank + targets
    private void SetCameraTargets()
    {
        int iterator = 0;
        GameObject[] allGameObjects = agentManager.GetAllGameObjects();
        int size = allGameObjects.Length;
        Transform[] tranformTargets = new Transform[size];

        foreach (GameObject go in allGameObjects)
        {
            tranformTargets[iterator] = go.transform;
            iterator++;
        }

        m_CameraControl.m_Targets = tranformTargets;
    }


    private IEnumerator GameLoop()
    {
        // Mixage / brassage
        genetique.makeNextGeneration();

        while (genetique.hasNext())
        { 
            Connaissances connaissances = new Connaissances();
            Squad squad = genetique.nextSquad();

            yield return StartCoroutine(RoundStarting(connaissances, squad));
            yield return StartCoroutine(RoundPlaying());
            yield return StartCoroutine(RoundEnding());

            // Le score c'est un long contenant les ms du temps de jeu
            // Récupérer le score et le mettre dans squad.setScore(score)

            Debug.Log("FIN DE SQUAD : ");
        }

        Debug.Log("FIN DE GENERATION : ");

        if (m_GameWinner != null)
        {
            // Peut etre la fonction reset ici meme
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator RoundStarting(Connaissances connaissances, Squad squad)
    {
        ResetAll();
        DisableControl();

        m_CameraControl.SetStartPositionAndSize();

        m_RoundNumber++;
        m_MessageText.text = "Generation " + m_RoundNumber;

        for (int i = 0; i < agentManager.m_Tanks.Length; i++)
        {
            agentManager.setIntelligence(squad.tireur, squad.eclaireur, connaissances, i);
            Debug.Log("Round starting : " + i);
        }

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

        for (int i = 0; i < agentManager.m_Targets.Length; i++)
        {
            if (agentManager.m_Targets[i].activeSelf)
                nbTargetsAlive++;
        }

        return nbTargetsAlive < 1;
    }

    private void RespawnTanksIfDead()
    {
        foreach (TankManager tank in agentManager.m_Tanks)
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

        for (int i = 0; i < agentManager.m_Tanks.Length - 1; i++)
        {
            if (agentManager.m_Tanks[i].m_TargetsKilled > agentManager.m_Tanks[i + 1].m_TargetsKilled)
                tankWinner = agentManager.m_Tanks[i];
        }

        return tankWinner;
    }


    private TankManager GetGameWinner()
    {
        for (int i = 0; i < agentManager.m_Tanks.Length; i++)
        {
            if (agentManager.m_Tanks[i].m_Wins == m_NumRoundsToWin)
                return agentManager.m_Tanks[i];
        }

        return null;
    }


    private string EndMessage()
    {
        string message = "DRAW!";

        if (m_RoundWinner != null)
            message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

        message += "\n\n\n\n";

        for (int i = 0; i < agentManager.m_Tanks.Length; i++)
        {
            message += agentManager.m_Tanks[i].m_ColoredPlayerText + ": " + agentManager.m_Tanks[i].m_Wins + " WINS\n";
        }

        if (m_GameWinner != null)
            message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

        return message;
    }

    private void ResetAll()
    {
        foreach (TankManager tm in agentManager.m_Tanks)
        {
            tm.Reset();
        }

        foreach (var target in agentManager.m_Targets)
        {
            target.SetActive(true);
        }

        foreach (ScoutManager pfm in agentManager.m_Scouts)
        {
            pfm.Reset();
        }

    }

    private void EnableControl()
    {
        foreach (TankManager tm in agentManager.m_Tanks)
        {
            tm.EnableControl();
        }

        foreach (ScoutManager pfm in agentManager.m_Scouts)
        {
            pfm.EnableControl();
        }
    }


    private void DisableControl()
    {
        foreach (TankManager tm in agentManager.m_Tanks)
        {
            tm.DisableControl();
        }

        foreach (ScoutManager pfm in agentManager.m_Scouts)
        {
            pfm.DisableControl();
        }
    }
}