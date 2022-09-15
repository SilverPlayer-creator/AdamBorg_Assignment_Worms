using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    private int _currentPlayerIndex;
    [Range(1, 4)] private int _playerAmount;

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
        _currentPlayerIndex = 1;
    }
    public static PlayerManager GetInstance()
    {
        return _instance;
    }

    public bool isItPlayersTurn(int index)
    {
        return index == _currentPlayerIndex;
    }
}
