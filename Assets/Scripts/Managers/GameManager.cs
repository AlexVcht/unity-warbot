using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Diagnostics;

public class GameManager : MonoBehaviour
{
    public int m_NumTargets = 10;
    public float m_StartDelay = 2f;
    public float m_EndDelay = 2f;
    public long maxTimeSimul = 60 * 1000; // 1min30s
    public CameraControl m_CameraControl;
    [HideInInspector] public Text m_MessageText;

    private Stopwatch stopWatch;
    private int m_RoundNumber;
    private int m_GenerationNumber;
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;
    private ScoreGUI scoreGUI;
    private AgentManager agentManager;
    private Genetique genetique;
    private Connaissances connaissances;
    private Squad squad;

    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);
 
        agentManager = GetComponent<AgentManager>();
        agentManager.InitAgents(m_NumTargets);

        stopWatch = Stopwatch.StartNew();
        genetique = new Genetique(10, 100, 0.4f);
        connaissances = new Connaissances();

        SetScoreUI();

        SetCameraTargets();

        StartCoroutine(GameLoop());
    }

    private void SetScoreUI()
    {
        ScoreGUI scoreGui = GetComponent<ScoreGUI>();

        scoreGui.m_Tank = agentManager.m_Tanks;
        scoreGui.m_Time = stopWatch;
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
        m_GenerationNumber++;
        m_RoundNumber = 0;
        genetique.makeNextGeneration();

        while (genetique.hasNext())
        {
            connaissances.Reset();
            squad = genetique.nextSquad();

            stopWatch.Reset();
            stopWatch.Start();
          
            yield return StartCoroutine(RoundStarting(connaissances));
            yield return StartCoroutine(RoundPlaying());
            yield return StartCoroutine(RoundEnding());

            // Le score c'est un long contenant les ms du temps de jeu
            // Récupérer le score et le mettre dans squad.setScore(score)

            UnityEngine.Debug.Log("FIN DE SQUAD : ");
        }

        UnityEngine.Debug.Log("FIN DE GENERATION : ");

        if (false)
        {
            // Peut etre la fonction reset ici meme
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator RoundStarting(Connaissances connaissances)
    {
        ResetAll();
        DisableControl();

        m_CameraControl.SetStartPositionAndSize();

        m_RoundNumber++;
        m_MessageText.text = "Generation "+ m_GenerationNumber+" Squad " + m_RoundNumber;

        agentManager.setIntelligence(squad.tireur, squad.eclaireur, connaissances);

        yield return m_StartWait;
    }


    private IEnumerator RoundPlaying()
    {
        EnableControl();

        m_MessageText.text = string.Empty;

        while (IsTargetsAlive() >= 1 && stopWatch.ElapsedMilliseconds <= maxTimeSimul)
        {
            // Meme si je met 15 * 1000 = 15s ca dure 30000 = 30s ...
           // RespawnTanksIfDead();
            yield return null;
        }
        stopWatch.Stop();
        // Get the elapsed time as a TimeSpan value.
        squad.score = ((maxTimeSimul - stopWatch.ElapsedMilliseconds)*1000 / maxTimeSimul + (m_NumTargets - IsTargetsAlive()) * 1000 / m_NumTargets);
    }

    private IEnumerator RoundEnding()
    {
        DisableControl();

        string message = EndMessage();
        m_MessageText.text = message;

        yield return m_EndWait;
    }


    private int IsTargetsAlive()
    {
        int nbTargetsAlive = 0;

        for (int i = 0; i < agentManager.m_Targets.Length; i++)
        {
            if (agentManager.m_Targets[i].activeSelf)
                nbTargetsAlive++;
        }

        return nbTargetsAlive;
    }

    private void RespawnTanksIfDead()
    {
        if (!agentManager.m_Tanks.m_Instance.activeSelf)
        {
            if (agentManager.m_Tanks.m_TargetsKilled > 0)
                agentManager.m_Tanks.m_TargetsKilled--;
            agentManager.m_Tanks.Reset();
        }
    }

    private string EndMessage()
    {
        string message = "Squad hit " + agentManager.m_Tanks.m_TargetsKilled + " targets !";
        return message;
    }

    private void ResetAll()
    {
        Color color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));

        agentManager.m_Scouts.m_PlayerColor = color;
        agentManager.m_Tanks.m_PlayerColor = color;

        agentManager.m_Tanks.Reset();
        agentManager.m_Scouts.Reset();

        foreach (var target in agentManager.m_Targets)
        {
            target.SetActive(true);
            MeshRenderer[] renderers = target.GetComponentsInChildren<MeshRenderer>();

            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = Color.white;
            }
        }
    }

    private void EnableControl()
    {
        agentManager.m_Tanks.EnableControl();
        agentManager.m_Scouts.EnableControl();
    }


    private void DisableControl()
    {
        agentManager.m_Tanks.DisableControl();
        agentManager.m_Scouts.DisableControl();
    }
}