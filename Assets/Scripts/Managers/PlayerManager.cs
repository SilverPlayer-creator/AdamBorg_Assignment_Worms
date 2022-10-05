using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public delegate void GameEnd();
    public event GameEnd OnGameEnded;
    public static PlayerManager Instance { get { return _instance; } }
    [SerializeField] private List<ActivePlayer> _players;
    [SerializeField] private List<GameObject> _lineRenderers;
    [SerializeField] private CameraFollow _mainCamera;
    [SerializeField] private Transform _playerEndPosition;
    [SerializeField] private TurnManager _turnManager;
    private static PlayerManager _instance;
    private List<ActivePlayer> _activePlayers = new List<ActivePlayer>();
    private ActivePlayer _currentPlayer;
    private bool _gameHasEnded;
    private int _victoryIndex;
    private int _amountOfPlayers;
    private int _currentPlayerIndex;
    private void Awake()
    {
        Debug.Log("Player manager Awake");
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
            {
                _players[i].gameObject.SetActive(false);
                _lineRenderers[i].SetActive(false);
            }
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
        ChangeActivePlayer(true);
        _currentPlayer.Controller.enabled = false;
        _currentPlayer.transform.position = _playerEndPosition.position;
        _currentPlayer.Controller.enabled = true;
        for (int i = 0; i < _players.Count; i++)
        {
            if (_currentPlayer == _players[i])
            {
                Debug.Log("The player that won is player " + i);
                _victoryIndex = i + 1;
            }
        }
        OnGameEnded?.Invoke();
        Debug.Log("Invoke");
        _gameHasEnded = true;
        yield return new WaitForSeconds(2f);
    }
    private void RemoveDeadPlayer(ActivePlayer playerToRemove)
    {
        _activePlayers.Remove(playerToRemove);
        if (_activePlayers.Count == 1 && !_gameHasEnded)
        {
            Debug.Log("End game");
            StartCoroutine(GameEnded());
        }
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
    public void FocusCamOnGrenade(Transform grenade)
    {
        _mainCamera.LookAtGrenade(grenade);
    }
    public int VictoryInt => _victoryIndex;
    public bool GameHasEnded => _gameHasEnded;
}
