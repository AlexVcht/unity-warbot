using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreGUI : MonoBehaviour
{
    [HideInInspector] public TankManager m_Tank;
    [HideInInspector] public Stopwatch m_Time;

    private string scoreUI;

    public void Awake()
    {
        if (m_Time != null) m_Time.Reset();
    }

    public void OnGUI()
    {
        scoreUI = string.Empty;

        scoreUI += "<b>Targets hit</b>" + " : " + m_Tank.m_TargetsKilled + "\n" + "<b>Time</b> : " + m_Time.ElapsedMilliseconds / 1000 + "s";

        GUI.Label(new Rect(10, Screen.height - 40, 100, 50), scoreUI);

        bool exitButton = GUI.Button(new Rect(Screen.width - 102, Screen.height - 42, 100, 40), "Exit");
        string sceneToLoad = "Menu";

        // If we click on the exit button we go to the menu
        if (exitButton) SceneManager.LoadScene(sceneToLoad);
    }

    public void Update()
    {
        scoreUI = string.Empty;

        scoreUI += m_Tank.m_ColoredPlayerText + " : " + m_Tank.m_TargetsKilled + "\n" + " Time : " + m_Time.ElapsedMilliseconds / 1000 + "s";
    }
}