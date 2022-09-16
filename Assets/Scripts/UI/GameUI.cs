using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _playerName;

    private void Start()
    {
        _button.onClick.AddListener(OnButtonPressed);
    }

    public void OnButtonPressed()
    {
        float randomValue = Random.Range(0f, 1f);
        _progressBar.fillAmount = randomValue;
    }
}
