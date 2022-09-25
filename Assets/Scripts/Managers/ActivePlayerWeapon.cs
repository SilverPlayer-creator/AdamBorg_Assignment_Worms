using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ActivePlayerWeapon : MonoBehaviour
{
    [SerializeField] private PlayerManager _manager;
    private bool _isHoldingFire;
    public void Fire(InputAction.CallbackContext context)
    {
        PlayerHeldWeapons activePlayerWeapon = _manager.GetCurrentPlayer().GetComponent<PlayerHeldWeapons>();
        if (context.performed)
        {
            _isHoldingFire = true;
        }
        if(context.canceled)
        {
            _isHoldingFire = false;
            activePlayerWeapon.Shoot();
            Debug.Log("Mouse let go");
        }
        //activePlayerWeapon.HoldingFire(_isHoldingFire);
    }
}
