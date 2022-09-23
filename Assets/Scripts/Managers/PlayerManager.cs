using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    private int _currentPlayerIndex;
    [SerializeField] private List<ActivePlayer> _players;
    [SerializeField] private Camera[] _playerCameras;
    private List<ActivePlayer> _activePlayers;
    private int _amountOfPlayers;

    private ActivePlayer _currentPlayer;
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
        _currentPlayer = _players[0];
        _amountOfPlayers = PlayerPrefs.GetInt("PlayerAmount");
        _activePlayers = new List<ActivePlayer>();

        //WHAT I WANT
        //Go through all the controllers
        //Check if the amount of controllers match the amount of people that want to play
        for (int i = 0; i < _amountOfPlayers; i++)
        {
            if(i<= _amountOfPlayers -1)
            _activePlayers.Add(_players[i]);

            if(i > _amountOfPlayers-1)
            _players[i].gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerEndedTurn();
        }
        for (int i = 0; i < _activePlayers.Count; i++)
        {
            //_activePlayers[i].GetComponent<>
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
        //Debug.Log("End current turn for player " + (_currentPlayerIndex+1));
        //_players[_currentPlayerIndex].UpdateIsActivePlayer(false);
        //if (_currentPlayerIndex < _amountOfPlayers - 1)
        //{
        //    _currentPlayerIndex++;
        //    //Debug.Log("Increase player index to " + _currentPlayerIndex);
        //}
        //else
        //{
        //    _currentPlayerIndex = 0;
        //}
        //_players[_currentPlayerIndex].UpdateIsActivePlayer(true);
        //_players[_currentPlayerIndex].CanDoActions();
        //_players[_currentPlayerIndex].ChangeInput(true);


        if(_currentPlayerIndex < _amountOfPlayers - 1)
        {
            _currentPlayerIndex++;
        }
        else
        {
            _currentPlayerIndex = 0;
        }
        _currentPlayer = _activePlayers[_currentPlayerIndex];
        for (int i = 0; i < _activePlayers.Count; i++)
        {
            if(i != _currentPlayerIndex)
            {
                _playerCameras[i].depth = 0;
            }
            else
            {
                _playerCameras[i].depth = 1;
            }
        }
    }
    public void PlayerKilled()
    {

    }
    public IEnumerator EndCurrentTurn()
    {
        Debug.Log("End turn");
        yield return new WaitForSeconds(0.5f);
        PlayerEndedTurn();
    }
    public List<ActivePlayer> GetAllPlayers()
    {
        return _activePlayers;
    }
}
