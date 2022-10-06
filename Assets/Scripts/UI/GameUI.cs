using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject[] _uiToTurnOff;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _winnerText;
    [SerializeField] private TextMeshProUGUI _startText;
    private bool _roundStarted;
    private void Start()
    {
        _playerManager.OnGameEnded += StartFadeIn;
        TurnManager.TurnInstance.OnCountDown += ChangeStartText;
    }
    void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        _anim.SetTrigger("FadeIn");
        foreach (GameObject item in _uiToTurnOff)
        {
            item.SetActive(false);
        }
        yield return new WaitForSeconds(2f);
        _anim.SetTrigger("FadeOut");
        _gameOverPanel.SetActive(true);
        _winnerText.text = "Player " + PlayerManager.Instance.VictoryInt.ToString() + " wins!";
    }
    void ChangeStartText(int countDown)
    {
        if(countDown > 0)
        {
            _startText.text = "Round Starts In " + "\n" + countDown.ToString();
            AudioManager.Instance.PlaySound("CountDown");
        }
        else if(countDown <= 0 && !_roundStarted)
        {
            AudioManager.Instance.PlaySound("RoundStart");
            StartCoroutine(RemoveText());
        }
    }
    IEnumerator RemoveText()
    {
        _startText.text = "START!";
        _roundStarted = true;
        yield return new WaitForSeconds(1f);
        _startText.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        _playerManager.OnGameEnded -= StartFadeIn;
    }
}
