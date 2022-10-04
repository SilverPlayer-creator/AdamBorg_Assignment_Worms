using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TurnManager : MonoBehaviour
{
    public delegate void TurnEnd(bool hasEnded);
    public event TurnEnd OnTurnEnding;
    public static TurnManager TurnInstance;
    [SerializeField] private float _maxTimeLimit;
    [SerializeField] private Image _timeLimitImage;
    [SerializeField] private GameObject _timeLimitObject;
    [SerializeField] private PlayerInput _inputSystem;
    [SerializeField] private PlayerManager _playerManager;
    private ActivePlayerWeapon _weaponInput;
    private float _checkIfFalling;
    private float _currentTimeLimit;
    private bool _timeCanPass;
    private bool _turnIsEnding;
    private bool _playerHasDoneAction;
    private bool _roundHasStarted;
    private void Awake()
    {
        _currentTimeLimit = _maxTimeLimit;
        _roundHasStarted = false;

        if (TurnInstance == null)
            TurnInstance = this;
        else
            Destroy(this);
        _weaponInput = GetComponent<ActivePlayerWeapon>();
        _weaponInput.OnThrow += StopTime;
        GetComponent<ActivePlayerInput>().OnRoundStart += StartRound;
        _playerManager.OnGameEnded += EndGame;
    }
    private void Update()
    {
        if (_roundHasStarted)
        {
            if (!_playerManager.GetCurrentPlayer.IsGrounded() && !_playerHasDoneAction)
                _checkIfFalling += Time.deltaTime;
            else
            {
                _checkIfFalling = 0;
            }
        }
        if (_checkIfFalling >= 5)
            _timeCanPass = false;
        if (_timeCanPass)
        {
            _currentTimeLimit -= Time.deltaTime;
            _timeLimitImage.fillAmount = (_currentTimeLimit / _maxTimeLimit);
        }
        if (_currentTimeLimit <= 0 && !_turnIsEnding)
            StartCoroutine(EndCurrentTurn());
        if (_checkIfFalling >= 5 || _playerHasDoneAction)
            _timeCanPass = false;
        if (Input.GetKeyDown(KeyCode.T))
            StartCoroutine(EndCurrentTurn());
    }
    private IEnumerator EndCurrentTurn()
    {
        _turnIsEnding = true;
        _timeCanPass = false;
        InvokeTurnEnd(false);
        Debug.Log("End turn");
        while (true)
        {
            if (_playerManager.GetCurrentPlayer.IsGrounded())
                break;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        StartNewTurn();
    }
    void StartNewTurn()
    {
        _turnIsEnding = false;
        _currentTimeLimit = _maxTimeLimit;
        _playerHasDoneAction = false;
        _timeCanPass = true;
        InvokeTurnEnd(true);
    }
    public void DecreaseTimeRemaining(int timeDecreased)
    {
        _currentTimeLimit -= timeDecreased;
    }
    private void StopTime(bool stopped)
    {
        _playerHasDoneAction = true;
        _timeCanPass = false;
    }
    private void OnDestroy()
    {
        _weaponInput.OnThrow -= StopTime;
    }
    public void InvokeTurnEnd(bool hasEnded)
    {
        OnTurnEnding?.Invoke(hasEnded);
    }
    public void QuickEnd()
    {
        StartCoroutine(EndCurrentTurn());
    }
    void StartRound()
    {
        _roundHasStarted = true;
        _timeCanPass = true;
    }
    void EndGame(int _int)
    {
        _timeCanPass = false;
    }
}
