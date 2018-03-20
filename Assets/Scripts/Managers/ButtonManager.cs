using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
    public void NewGame(string newGameLevel)
    {
        File.Delete(Application.persistentDataPath + "/SaveState.gd");
        UnityEngine.Debug.Log("Deleted SaveState : " + Application.persistentDataPath + "/SaveState.gd");
        SceneManager.LoadScene(newGameLevel);
    }

    public void LoadGame(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
