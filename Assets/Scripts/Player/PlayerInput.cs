using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput : MonoBehaviour
{
    private Vector2 _moveValue;
    public Vector2 MoveValue
    {
        get { return _moveValue; }
        private set { }
    }
    private float _rotateValue;
    public float RotateValue
    {
        get { return _rotateValue; }
        private set { }
    }
    [SerializeField] private float _rotateSpeed;
    public float RotateSpeed
    {
        get { return _rotateSpeed; }
        private set { }
    }
    private bool _pressedJump;
    public bool PressedJump
    {
        get { return _pressedJump; }
        private set { }
    }
    [SerializeField] private GameObject _weaponHolder;

    [Header("Weapon")]
    [SerializeField] private Pistol _pistol;
    public void MovePlayer(InputAction.CallbackContext context)
    {
        _moveValue = context.ReadValue<Vector2>();
    }
    public void RotatePlayer(InputAction.CallbackContext context)
    {
        _rotateValue = context.ReadValue<float>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump function reached");
        if (context.phase == InputActionPhase.Performed)
        {
            _pressedJump = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _pressedJump = false;
        }
    }
    public void Fire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _weaponHolder.GetComponent<WeaponHolder>().FireHeldWeapon(true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _weaponHolder.GetComponent<WeaponHolder>().FireHeldWeapon(false);
        }
    }
    public void AltFire(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed && GetComponent<PlayerActive>().IsActivePlayer)
        {
            //GetComponent<GrenadeThrow>().Throw(this);
        }
    }
}
