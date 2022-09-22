using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetPlayers : MonoBehaviour
{
    public void SetPlayerAmount(int playerAmount)
    {
        switch (playerAmount)
        {
            case 2:
                PlayerPrefs.SetInt("PlayerAmount", 2);
                break;

            case 3:
                PlayerPrefs.SetInt("PlayerAmount", 3);
                break;
            case 4:
                PlayerPrefs.SetInt("PlayerAmount", 4);
                break;
            default:
                break;
        }
        SceneManager.LoadScene("SampleScene");
    }
}
