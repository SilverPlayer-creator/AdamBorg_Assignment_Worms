using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("Amount of players: " + _amountOfPlayers);
        _activePlayers = new List<ActivePlayer>();

        //WHAT I WANT
        //Go through all the controllers
        //Check if the amount of controllers match the amount of people that want to play
        for (int i = 0; i < 4; i++)
        {
            if(i <= _amountOfPlayers - 1)
            {
                _activePlayers.Add(_players[i]);
                Debug.Log("Add player " + (i+1) + " to active players.");
            }

            if(i > _amountOfPlayers - 1)
            {
                _players[i].gameObject.SetActive(false);
                Debug.Log("Set player " + (i + 1) + " to false");
            }
            foreach (ActivePlayer player in _activePlayers)
            {
                ActivePlayerHealth playerHealth = player.GetComponent<ActivePlayerHealth>();
                playerHealth.OnEnemyDied += EnemyDied;
            }
        }
        _playersAlive = _amountOfPlayers;
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
    public ActivePlayer GetCurrentPlayer()
    {
        return _currentPlayer;
    }
    public void PlayerEndedTurn()
    {
        if(_currentPlayerIndex < _activePlayers.Count - 1)
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
        _currentPlayer.GetComponent<PlayerHeldWeapons>().NewTurn();
        GetComponent<ActivePlayerInput>().SetCanMove(true);
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
    private void EnemyDied()
    {
        _playersAlive--;
        Debug.Log("One player died");
        if(_playersAlive == 1)
        {
            //Debug.Log("PLAYER WINS");
        }
    }
    public void RemovePlayer(ActivePlayerHealth _player)
    {
        _playersAlive--;
        ActivePlayer player = _player.GetComponent<ActivePlayer>();
        for (int i = 0; i < _activePlayers.Count; i++)
        {
            if (player == _activePlayers[i]) 
            _playerCameras.RemoveAt(i);
        }
        _activePlayers.Remove(player);
    }
}
