using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetPlayers : MonoBehaviour
{
    public void SetPlayerAmount(int playerAmount)
    {
        PlayerPrefs.SetInt("PlayerAmount", playerAmount);
        SceneManager.LoadScene("SampleScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
