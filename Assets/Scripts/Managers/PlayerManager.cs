using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    private int _currentPlayerIndex;
    [Range(1, 4)] private int _playerAmount;
    [SerializeField] private List<PlayerUnit> _players;

    [SerializeField] private Camera _mainCamera;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
        _currentPlayerIndex = 0;
    }
    private void Start()
    {
        for (int i = 0; i < _players.Count; i++)
        {
            _playerAmount++;
            _players[i].UpdateIsActivePlayer(i == 0);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerEndedTurn();
        }
    }
    public static PlayerManager GetInstance()
    {
        return _instance;
    }

    public bool isItPlayersTurn(int index)
    {
        return index == _currentPlayerIndex;
    }
    public void PlayerEndedTurn()
    {
        _players[_currentPlayerIndex].UpdateIsActivePlayer(false);
        if (_currentPlayerIndex < _playerAmount - 1)
        {
            _currentPlayerIndex++;
            
        }
        else
        {
            _currentPlayerIndex = 0;
        }
        _players[_currentPlayerIndex].UpdateIsActivePlayer(true);
    }
}
