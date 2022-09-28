using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    private int _currentPlayerIndex;
    [SerializeField] private List<ActivePlayer> _players;
    [SerializeField] private List<Camera> _playerCameras;
    private List<ActivePlayer> _activePlayers;
    private int _amountOfPlayers;
    private int _playersAlive;
    private ActivePlayer _currentPlayer;
    private bool _gameResultReached;
    [SerializeField] private float _maxTimeLimit;
    private float _currentTimeLimit;
    private bool _timeCanPass;
    [SerializeField] private Image _timeLimitImage;
    private bool _turnIsEnding;
    private float _checkIfFalling;
    private bool _playerHasDoneAction;
    private ActivePlayerInput _playerInput;
    private ActivePlayerWeapon _weaponInput;
    private SceneManagement _sceneManager;
    [SerializeField] private UnityEngine.InputSystem.PlayerInput _input;

    private void Awake()
    {
        _playerInput = GetComponent<ActivePlayerInput>();
        _weaponInput = GetComponent<ActivePlayerWeapon>();
        _sceneManager = GetComponent<SceneManagement>();
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
        _currentPlayerIndex = 0;
        _currentPlayer = _players[0];
        _amountOfPlayers = PlayerPrefs.GetInt("PlayerAmount");
        _activePlayers = new List<ActivePlayer>();

        for (int i = 0; i < 4; i++)
        {
            if(i <= _amountOfPlayers - 1)
            {
                _activePlayers.Add(_players[i]);
            }

            if(i > _amountOfPlayers - 1)
            {
                _players[i].gameObject.SetActive(false);
            }
            foreach (ActivePlayer player in _activePlayers)
            {
                ActivePlayerHealth playerHealth = player.PlayerHealth;
            }
        }
        _playersAlive = _amountOfPlayers;
        _currentTimeLimit = _maxTimeLimit;
        _timeCanPass = true;
    }
    private void Update()
    {
        if (!GetCurrentPlayer().IsGrounded() && !_playerHasDoneAction)
        {
            _checkIfFalling += Time.deltaTime;
            if(_checkIfFalling >= 5)
            {
                _timeCanPass = false;
            }
        }
        else
        {
            _checkIfFalling = 0;
            _timeCanPass = true;
        }
        if (_playerHasDoneAction)
        {
            _timeCanPass = false;
        }
        if (_timeCanPass)
        {
            _currentTimeLimit -= Time.deltaTime;
        }
        if (_currentTimeLimit <= 0 && !_turnIsEnding)
        {
            StartCoroutine(EndCurrentTurn());
        }
        _timeLimitImage.fillAmount = (_currentTimeLimit / _maxTimeLimit);
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerEndedTurn();
        }
    }
    public static PlayerManager GetInstance()
    {
        return _instance;
    }
    public ActivePlayer GetCurrentPlayer()
    {
        return _currentPlayer;
    }
    public void PlayerEndedTurn()
    {
        if(_currentPlayerIndex < _activePlayers.Count - 1)
            _currentPlayerIndex++;
        else
            _currentPlayerIndex = 0;

        _currentPlayer = _activePlayers[_currentPlayerIndex];
        for (int i = 0; i < _activePlayers.Count; i++)
        {
            if(i != _currentPlayerIndex)
                _playerCameras[i].depth = 0;
            else
                _playerCameras[i].depth = 1;
        }
        _currentPlayer.WeaponHolder.NewTurn();
        _playerInput.SetCanMove(true);
        _weaponInput.SetCanMakeInput(true);
        _turnIsEnding = false;
        _currentTimeLimit = _maxTimeLimit;
        GetComponent<PickupManager>().TryToSpawn();
        _weaponInput.PlayersSwitched();
        _playerHasDoneAction = false;
        _timeCanPass = true;
    }
    public IEnumerator EndCurrentTurn()
    {
        _turnIsEnding = true;
        _playerInput.SetCanMove(false);
        _weaponInput.SetCanMakeInput(false);
        Debug.Log("End turn");
        while (true)
        {
            if (_currentPlayer.IsGrounded())
            {
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        PlayerEndedTurn();
    }
    public IEnumerator EnableInput()
    {
        yield return new WaitForSeconds(0.5f);
        _input.enabled = true;
    }
    public List<ActivePlayer> GetAllPlayers()
    {
        return _activePlayers;
    }
    public void RemovePlayer(ActivePlayerHealth playerHealth)
    {
        ActivePlayer player = playerHealth.ActivePlayer;
        for (int i = 0; i < _activePlayers.Count; i++)
        {
            if (player == _activePlayers[i]) 
            _playerCameras.RemoveAt(i);
        }
        _activePlayers.Remove(player);
        _playersAlive--;
        if(_playersAlive == 1 && !_gameResultReached)
        {
            StartCoroutine(GameEnded());
        }
    }
    private IEnumerator GameEnded()
    {
        _playerInput.SetCanMove(false);
        _weaponInput.SetCanMakeInput(false);
        _gameResultReached = true;
        yield return new WaitForSeconds(5f);
        _sceneManager.ReloadScene();
    }
    public Camera GetActiveCamera()
    {
        for (int i = 0; i < _playerCameras.Count; i++)
        {
            if(_playerCameras[i].depth == 1)
            {
                return _playerCameras[i];
            }
        }
        return null;
    }
    public void DecreaseTimeRemaining(int timeDecreased)
    {
        _currentTimeLimit-= timeDecreased;
    }
    public void TimeCanPass(bool canPass)
    {
        _timeCanPass = canPass;
    }
    public void PlayerHasDoneAction()
    {
        _playerHasDoneAction = true;
    }
}
