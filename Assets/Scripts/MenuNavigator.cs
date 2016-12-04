using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigator : MonoBehaviour {

    public void loadGameLevel(string argSceneName)
    {
        SceneManager.LoadScene(argSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit was initialized");
    }
}
