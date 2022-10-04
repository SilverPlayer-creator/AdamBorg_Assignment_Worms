using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject[] _uiToTurnOff;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _winnerText;
    private int _victoryInt;
    private void Start()
    {
        _playerManager.OnGameEnded += StartFadeIn;
    }
    void StartFadeIn(int victoryInt)
    {
        Debug.Log("Start fade in");
        StartCoroutine(FadeIn());
        _victoryInt = victoryInt;
    }
    IEnumerator FadeIn()
    {
        _anim.SetTrigger("FadeIn");
        Debug.Log("Fade in");
        foreach (GameObject item in _uiToTurnOff)
        {
            item.SetActive(false);
        }
        yield return new WaitForSeconds(2f);
        _anim.SetTrigger("FadeOut");
        _gameOverPanel.SetActive(true);
        _winnerText.text = "Player " + _victoryInt.ToString() + " wins!";
    }
    private void OnDisable()
    {
        _playerManager.OnGameEnded -= StartFadeIn;
    }
}
