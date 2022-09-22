using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    private string _weaponName;
    [SerializeField] protected WeaponData _data;
    protected int _maxAmmo;
    [SerializeField] protected GameObject _projectilePrefab;
    protected Transform _barrel;
    protected int _currentAmmo;
    protected float _shootRate;
    public float ShootRate
    {
        get { return _shootRate; }
        private set { }
    }
    protected float _nextShootTime;
    public float NextShootTime
    {
        get { return _nextShootTime; }
        set { _nextShootTime = value; }
    }
    protected bool _isAutomatic;
    protected int _index;
    public bool IsAutomatic
    {
        get { return _isAutomatic; }
        private set { }
    }
    protected bool _holdingFire;
    protected bool _pickedUp;

    public delegate void OnAmmoChanged(int currentAmmo, int maxAmmo, int index);
    public static event OnAmmoChanged OnAmmoChangedEvent;

    public virtual void Initialize(int index, Transform barrel)
    {
        Debug.Log("Initialize");
        _weaponName = _data._weaponName;
        gameObject.name = _weaponName;
        _maxAmmo = _data._maxAmmo;
        _currentAmmo = _maxAmmo;
        _shootRate = _data._shootRate;
        _isAutomatic = _data._isAutomatic;
        _index = index;
        _barrel = barrel;
        EventInvoke();
    }
    public virtual void Shoot(Transform barrel) 
    {
        if (_currentAmmo > 0)
        {
            _currentAmmo--;
            GameObject newProjectile = Instantiate(_projectilePrefab);
            newProjectile.transform.position = _barrel.position;
            newProjectile.GetComponent<Projectile>().Initialize(_barrel);
            EventInvoke();
        }
        else
        {
            Reload(_index);
        }
    }
    public void Reload(int index)
    {
        if (_currentAmmo < _maxAmmo && index == _index) //if the ammo is not at max and the index matches that of the player reloading
        {
            _currentAmmo = _maxAmmo; //refill the ammo
            EventInvoke(); //tell the UI to update the ammo
            PlayerManager.GetInstance().StartCoroutine(PlayerManager.GetInstance().EndCurrentTurn());
        }
    }
    public void HoldingFire(bool holdingFire)
    {
        _holdingFire = holdingFire;
    }
    public void EventInvoke()
    {
        OnAmmoChangedEvent?.Invoke(_currentAmmo, _maxAmmo, _index);
        //Debug.Log("Invoke ammo");
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerUnit _player = other.GetComponent<PlayerUnit>();
        if (_player != null && !_pickedUp)
        {
            //transform.SetParent(_player.GetComponent<weaponh>)
            transform.SetParent(_player.WeaponHolder.transform);
            _player.WeaponHolder.AddWeapon(this);
            _pickedUp = true;
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
