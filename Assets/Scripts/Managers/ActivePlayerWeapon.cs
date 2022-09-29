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
        PlayerHeldWeapons activePlayerWeapon = _manager.GetCurrentPlayer().WeaponHolder;
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
            PlayerHeldWeapons activePlayerWeapon = _manager.GetCurrentPlayer().WeaponHolder;
            activePlayerWeapon.SwitchWeapon(_mouseScrollValue);
        }
    }
    public void Reload(InputAction.CallbackContext context)
    {
        if (context.performed && _canInput)
        {
            PlayerHeldWeapons activePlayerWeapon = _manager.GetCurrentPlayer().WeaponHolder;
            activePlayerWeapon.Reload();
        }
    }
    public void Throw(InputAction.CallbackContext context)
    {
        if (context.performed && _canInput)
        {
            PlayerHeldWeapons activePlayerWeapon = _manager.GetCurrentPlayer().WeaponHolder;
            if(activePlayerWeapon.GrenadeAmount > 0)
            {
                activePlayerWeapon.ThrowGrenade();
                GetComponent<ActivePlayerInput>().SetCanMove(false);
                _manager.TimeCanPass(false);
                _manager.PlayerHasDoneAction();
                _canInput = false;
            }
        }
    }
    public void SetCanMakeInput(bool canInput)
    {
        _canInput = canInput;
        if (!canInput)
        {
            PlayerHeldWeapons activePlayerWeapon = _manager.GetCurrentPlayer().WeaponHolder;
            activePlayerWeapon.HoldingFire(false);
        }
    }
}
