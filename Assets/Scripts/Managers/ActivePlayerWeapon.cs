using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ActivePlayerWeapon : MonoBehaviour
{
    public delegate void ThrowDelegate(bool thrown);
    public event ThrowDelegate OnThrow;
    [SerializeField] private PlayerManager _playerManager;
    private bool _isHoldingFire;
    private float _mouseScrollValue;
    private bool _canInput = false;
    private bool _playersSwitched;
    private void Start()
    {
        TurnManager.TurnInstance.OnTurnEnding += ChangeInput;
        GetComponent<ActivePlayerInput>().OnRoundStart += EnableInput;
        _playerManager.OnGameEnded += DisableInput;
    }
    public void Fire(InputAction.CallbackContext context)
    {
        PlayerHeldWeapons activePlayerWeapon = _playerManager.GetCurrentPlayer.WeaponHolder;
        if (context.performed && _canInput)
        {
            if (!_playersSwitched)
                _isHoldingFire = true;
        }
        else
            _isHoldingFire = false;
        if(context.canceled && _canInput)
        {
            if (!_playersSwitched)
            {
                _isHoldingFire = false;
                activePlayerWeapon.SingleFire();
            }
            else
                _playersSwitched = false;
        }
        activePlayerWeapon.HoldingFire(_isHoldingFire);
    }
    public void Scroll(InputAction.CallbackContext context)
    {
        if (context.performed && _canInput)
        {
            _mouseScrollValue = context.ReadValue<float>();
            PlayerHeldWeapons activePlayerWeapon = _playerManager.GetCurrentPlayer.WeaponHolder;
            activePlayerWeapon.SwitchWeapon(_mouseScrollValue);
        }
    }
    public void Reload(InputAction.CallbackContext context)
    {
        if (context.performed && _canInput)
        {
            PlayerHeldWeapons activePlayerWeapon = _playerManager.GetCurrentPlayer.WeaponHolder;
            activePlayerWeapon.Reload();
        }
    }
    public void Throw(InputAction.CallbackContext context)
    {
        if (context.performed && _canInput)
        {
            PlayerHeldWeapons activePlayerWeapon = _playerManager.GetCurrentPlayer.WeaponHolder;
            if(activePlayerWeapon.GrenadeAmount > 0)
            {
                activePlayerWeapon.ThrowGrenade();
                OnThrow?.Invoke(false);
                _canInput = false;
            }
        }
    }
    private void ChangeInput(bool active)
    {
        _canInput = active;
        if (!active)
        {
            PlayerHeldWeapons activePlayerWeapon = _playerManager.GetCurrentPlayer.WeaponHolder;
            activePlayerWeapon.HoldingFire(false);
        }
    }
    //Different events need to access the inputs, but they need different parameters...easy fix?
    private void EnableInput()
    {
        _canInput = true;
    }
    private void DisableInput(int _int)
    {
        _canInput = false;
    }
    private void OnDisable()
    {
        TurnManager.TurnInstance.OnTurnEnding -= ChangeInput;
        GetComponent<ActivePlayerInput>().OnRoundStart -= EnableInput;
        _playerManager.OnGameEnded -= DisableInput;
    }
}
