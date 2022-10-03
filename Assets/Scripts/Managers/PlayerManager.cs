using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public delegate void GameEnd();
    public event GameEnd OnGameEnded;
    [SerializeField] private List<ActivePlayer> _players;
    [SerializeField] private CameraFollow _mainCamera;
    private static PlayerManager _instance;
    private int _currentPlayerIndex;
    private List<ActivePlayer> _activePlayers = new List<ActivePlayer>();
    private int _amountOfPlayers;
    private ActivePlayer _currentPlayer;
    private bool _gameResultReached;
    private SceneManagement _sceneManager;
    private TurnManager _turnManager;
    private void Awake()
    {
        _sceneManager = GetComponent<SceneManagement>();
        _turnManager = GetComponent<TurnManager>();
        _turnManager.OnTurnEnding += ChangeActivePlayer;
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
        _currentPlayerIndex = 0;
        _currentPlayer = _players[0];
        _amountOfPlayers = PlayerPrefs.GetInt("PlayerAmount");

        for (int i = 0; i < 4; i++)
        {
            if(i <= _amountOfPlayers - 1)
                _activePlayers.Add(_players[i]);

            if(i > _amountOfPlayers - 1)
                _players[i].gameObject.SetActive(false);
            foreach (ActivePlayer player in _activePlayers)
            {
                ActivePlayerHealth playerHealth = player.PlayerHealth;
                playerHealth.OnPlayerDeath += RemoveDeadPlayer;
            }
        }
    }
    public ActivePlayer GetCurrentPlayer => _currentPlayer;
    public List<ActivePlayer> GetAllPlayers => _activePlayers;
    private IEnumerator GameEnded()
    {
        OnGameEnded?.Invoke();
        _gameResultReached = true;
        yield return new WaitForSeconds(5f);
        _sceneManager.ReloadScene();
    }
    private void RemoveDeadPlayer(ActivePlayer playerToRemove)
    {
        _activePlayers.Remove(playerToRemove);
        if (_activePlayers.Count == 1 && !_gameResultReached)
            StartCoroutine(GameEnded());
    }
    private void ChangeActivePlayer(bool newTurn)
    {
        if (newTurn)
        {
            if (_currentPlayerIndex < _activePlayers.Count - 1)
                _currentPlayerIndex++;
            else
                _currentPlayerIndex = 0;
            _currentPlayer = _activePlayers[_currentPlayerIndex];
            _mainCamera.ChangePlayer(_currentPlayer.transform);
            _currentPlayer.WeaponHolder.NewTurn();
        }
    }
}
