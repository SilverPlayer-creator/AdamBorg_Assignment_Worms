using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerActive : MonoBehaviour
{
    private PlayerController _controller;
    public void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }
    [Header("Active")]
    [SerializeField] private int _playerIndex;
    [SerializeField] private Camera _playerCamera;
    private bool _isActivePlayer;
    public bool IsActivePlayer
    {
        get { return _isActivePlayer; }
        private set { }
    }
    public int PlayerIndex
    {
        get { return _playerIndex; }
        private set { }
    }
    public void UpdateIsActivePlayer(bool isActive)
    {
        _isActivePlayer = isActive;
        _playerCamera.enabled = isActive;
        GetComponent<PlayerMove>().CanMove(isActive);
    }
}
