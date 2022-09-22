using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    private int _currentPlayerIndex;
    [SerializeField] private List<PlayerUnit> _players;
    private int _amountOfPlayers;
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
        _amountOfPlayers = PlayerPrefs.GetInt("PlayerAmount");

        //WHAT I WANT
        //Go through all the controllers
        //Check if the amount of controllers match the amount of people that want to play
        for (int i = 0; i < 4; i++)
        {
            if(i > _amountOfPlayers-1)
            _players[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < _amountOfPlayers; i++)
        {
            //Debug.Log("Activate players");
            _players[i].ChangeInput(i == 0);
            _players[i].UpdateIsActivePlayer(i == 0);
            _players[0].CanDoActions();
        }
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerEndedTurn();
        }
    }
    private void FixedUpdate()
    {
        
    }
    public static PlayerManager GetInstance()
    {
        return _instance;
    }
    public void PlayerEndedTurn()
    {
        Debug.Log("End current turn for player " + (_currentPlayerIndex+1));
        _players[_currentPlayerIndex].UpdateIsActivePlayer(false);
        if (_currentPlayerIndex < _amountOfPlayers - 1)
        {
            _currentPlayerIndex++;
            //Debug.Log("Increase player index to " + _currentPlayerIndex);
        }
        else
        {
            _currentPlayerIndex = 0;
        }
        _players[_currentPlayerIndex].UpdateIsActivePlayer(true);
        _players[_currentPlayerIndex].CanDoActions();
        _players[_currentPlayerIndex].ChangeInput(true);

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
}
