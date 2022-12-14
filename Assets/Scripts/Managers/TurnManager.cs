using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private float _maxTimeLimit;
    [SerializeField] private Image _timeLimitImage;
    public delegate void TurnEnd(bool turnHasEnded);
    public event TurnEnd OnTurnEnding;
    public delegate void CountDown(int countDown);
    public event CountDown OnCountDown;
    public static TurnManager TurnInstance;
    private ActivePlayerWeapon _weaponInput;
    private float _timeToRoundStart = 1;
    private float _timeNotGrounded;
    private float _currentTimeLimit;
    private float _newMaxTimeLimit;
    private bool _timeCanPass;
    private bool _turnIsEnding;
    private bool _playerHasDoneAction;
    private bool _roundHasStarted;
    private int _countDown = 5;
    private int _turnsPassed;
    private void Awake()
    {
        _currentTimeLimit = _maxTimeLimit;
        _roundHasStarted = false;
        _newMaxTimeLimit = _maxTimeLimit;
        if (TurnInstance == null)
            TurnInstance = this;
        else
            Destroy(this);
        _weaponInput = GetComponent<ActivePlayerWeapon>();
        _weaponInput.OnThrow += StopTime;
        PlayerManager.Instance.OnGameEnded += EndGame;
    }
    private void Update()
    {
        if (_roundHasStarted)
        {
            CheckIfFalling();
        }
        else
        {
            CountDownUpdate();
        }
        if (_timeCanPass)
        {
            _currentTimeLimit -= Time.deltaTime;
            _timeLimitImage.fillAmount = (_currentTimeLimit / _newMaxTimeLimit);
        }
        if (_currentTimeLimit <= 0 && !_turnIsEnding)
            StartCoroutine(EndCurrentTurn());
        if (_timeNotGrounded >= 5 || _playerHasDoneAction)
            _timeCanPass = false;
        if (Input.GetKeyDown(KeyCode.T) && !_turnIsEnding && !_playerHasDoneAction)
            StartCoroutine(EndCurrentTurn());
    }
    private IEnumerator EndCurrentTurn()
    {
        _turnIsEnding = true;
        _timeCanPass = false;
        InvokeTurnEnd(false);
        while (true)
        {
            if (PlayerManager.Instance.GetCurrentPlayer.IsGrounded())
                break;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        StartNewTurn();
    }
    void StartNewTurn()
    {
        _turnsPassed++;
        if(_turnsPassed >= 5 && _newMaxTimeLimit >= 8)
        {
            _newMaxTimeLimit--;
            _turnsPassed = 0;
        }
        _turnIsEnding = false;
        _currentTimeLimit = _newMaxTimeLimit;
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
    public void InvokeTurnEnd(bool hasEnded)
    {
        if(!PlayerManager.Instance.GameHasEnded)
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
    void CountDownUpdate()
    {
        _timeToRoundStart -= Time.deltaTime;
        if (_timeToRoundStart <= 0)
        {
            if (_countDown > 0)
            {
                _countDown--;
                _timeToRoundStart = 1;
            }
            else
            {
                StartRound();
            }
            OnCountDown?.Invoke(_countDown);
        }
    }
    void EndGame()
    {
        _timeCanPass = false;
    }
    void CheckIfFalling()
    {
        if (!PlayerManager.Instance.GetCurrentPlayer.IsGrounded() && !_playerHasDoneAction)
            _timeNotGrounded += Time.deltaTime;
        else
        {
            _timeNotGrounded = 0;
        }
    }
    private void OnDestroy()
    {
        _weaponInput.OnThrow -= StopTime;
    }
}
