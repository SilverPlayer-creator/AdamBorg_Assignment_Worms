using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagement : MonoBehaviour
{
    public void GoToScene (int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void SetPlayerAmount(int playerAmount)
    {
        PlayerPrefs.SetInt("PlayerAmount", playerAmount);
        SceneManager.LoadScene("SampleScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
