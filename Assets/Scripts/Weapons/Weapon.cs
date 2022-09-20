using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponData _data;
    protected int _maxAmmo;
    [SerializeField] protected GameObject _projectilePrefab;
    [SerializeField] protected Transform _barrel;
    protected int _currentAmmo;
    protected float _shootRate;
    protected float _nextShootTime;
    protected bool _isAutomatic;
    public bool IsAutomatic
    {
        get { return _isAutomatic; }
        private set { }
    }
    protected bool _holdingFire;

    public delegate void OnAmmoChanged(int currentAmmo);
    public static event OnAmmoChanged OnAmmoChangedEvent;

    public virtual void Initialize()
    {
        _maxAmmo = _data._maxAmmo;
        Debug.Log("Max ammo: " + _maxAmmo);
        _currentAmmo = _maxAmmo;
        Debug.Log("Current ammo: " + _currentAmmo);
        _shootRate = _data._shootRate;
        Debug.Log("Shoot rate: " + _shootRate);
        _isAutomatic = _data._isAutomatic;
        Debug.Log("Is automatic?: " + _isAutomatic);
    }
    public virtual void Shoot() {  }
    public void Reload()
    {
        _currentAmmo = _maxAmmo;
        PlayerManager.GetInstance().PlayerEndedTurn();
    }

    private void Awake()
    {
        //Initialize(); 
    }
    public void HoldingFire(bool holdingFire)
    {
        _holdingFire = holdingFire;
    }
    public void EventInvoke()
    {
        OnAmmoChangedEvent?.Invoke(_currentAmmo);
        Debug.Log(_currentAmmo);
    }
}
