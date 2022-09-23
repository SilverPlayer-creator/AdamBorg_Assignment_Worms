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
        if (context.performed)
        {
            _isHoldingFire = true;
        }
        else if(context.canceled)
        {
            _isHoldingFire = false;
        }
        _manager.GetCurrentPlayer().GetComponent<PlayerHeldWeapons>().HoldingFire(_isHoldingFire, true);
    }
}
