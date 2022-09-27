using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ActivePlayerWeapon : MonoBehaviour
{
    [SerializeField] private PlayerManager _manager;
    private bool _isHoldingFire;
    private float _mouseScrollValue;
    private bool _canInput = true;
    private bool _playersSwitched;
    public void Fire(InputAction.CallbackContext context)
    {
        PlayerHeldWeapons activePlayerWeapon = _manager.GetCurrentPlayer().GetComponent<PlayerHeldWeapons>();
        if (context.performed && _canInput)
        {
            if (!_playersSwitched)
                _isHoldingFire = true;
        }
        else
        {
            _isHoldingFire = false;
        }
        if(context.canceled && _canInput)
        {
            if (!_playersSwitched)
            {
                _isHoldingFire = false;
                activePlayerWeapon.SingleFire();
                Debug.Log("BUtton let go");
            }
            else
            {
                _playersSwitched = false;
            }
        }
        activePlayerWeapon.HoldingFire(_isHoldingFire);
    }
    public void Scroll(InputAction.CallbackContext context)
    {
        if (context.performed && _canInput)
        {
            _mouseScrollValue = context.ReadValue<float>();
            PlayerHeldWeapons activePlayerWeapon = _manager.GetCurrentPlayer().GetComponent<PlayerHeldWeapons>();
            activePlayerWeapon.SwitchWeapon(_mouseScrollValue);
        }
    }
    public void Reload(InputAction.CallbackContext context)
    {
        if (context.performed && _canInput)
        {
            PlayerHeldWeapons activePlayerWeapon = _manager.GetCurrentPlayer().GetComponent<PlayerHeldWeapons>();
            activePlayerWeapon.Reload();
        }
    }
    public void Throw(InputAction.CallbackContext context)
    {
        if (context.performed && _canInput)
        {
            PlayerHeldWeapons activePlayerWeapon = _manager.GetCurrentPlayer().GetComponent<PlayerHeldWeapons>();
            activePlayerWeapon.ThrowGrenade();
            GetComponent<ActivePlayerInput>().SetCanMove(false);
            _canInput = false;
        }
    }
    public void SetCanMakeInput(bool canInput)
    {
        _canInput = canInput;
        if (!canInput)
        {
            PlayerHeldWeapons activePlayerWeapon = _manager.GetCurrentPlayer().GetComponent<PlayerHeldWeapons>();
            activePlayerWeapon.HoldingFire(false);
        }
    }
    public void PlayersSwitched()
    {
        _playersSwitched = true;
    }
}
