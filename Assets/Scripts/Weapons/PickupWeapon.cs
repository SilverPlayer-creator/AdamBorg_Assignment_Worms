using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupWeapon : MonoBehaviour
{
    [SerializeField] private WeaponData _data;
    private int _damage;
    private string _weaponName;
    private int _maxAmmo;
    private int _currentAmmo;
    private float _fireRate;
    private float _nextShootTime;
    private bool _isAutomatic;
    private bool _holdingFire;
    private int _force;
    private bool _canFire = true;
    [SerializeField] private Transform _barrel;
    [SerializeField]private GameObject _prefab;
    private Sprite _image;
    private void Awake()
    {
        _damage = _data.Damage;
        _weaponName = _data.WeaponName;
        _maxAmmo = _data.MaxAmmo;
        _currentAmmo = _maxAmmo;
        _fireRate = _data.ShootRate;
        _force = _data.Force;
        _prefab = _data.Prefab;
        name = _data.WeaponName;
        _image = _data.Icon;
        _isAutomatic = _data.IsAutomatic;
    }
    private void Update()
    {
        if(_isAutomatic && _holdingFire && _canFire) 
        {
           if (Time.time >= _nextShootTime)
            Shoot();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHeldWeapons player = other.GetComponent<PlayerHeldWeapons>();
    }
    public string GetWeaponName()
    {
        return _weaponName;
    }
    public Sprite GetIcon()
    {
        return _image;
    }
    public void Shoot()
    {
        if(_currentAmmo > 0)
        {
            _currentAmmo--;
            //Debug.Log("Barrel position: " + _barrel.position);
            GameObject bullet = Instantiate(_prefab, _barrel.position, transform.rotation);
            bullet.AddComponent<WeaponProjectile>();
            bullet.GetComponent<WeaponProjectile>().Initialize(_damage);
            //Debug.Log("Bullet instantiates at: " + bullet.transform.position);
            Rigidbody body = bullet.GetComponent<Rigidbody>();
            body.AddForce(transform.forward * _force);
            _nextShootTime = Time.time + 1f / _fireRate;
        }
        else
        {
            Reload();
        }
    }
    public void Reload()
    {
        if(_currentAmmo < _maxAmmo)
        {
            _currentAmmo = _maxAmmo;
            _holdingFire = false;
            PlayerManager manager = PlayerManager.GetInstance();
            _canFire = false;
            manager.StartCoroutine(manager.EndCurrentTurn());
        }
    }
    public void PlayerTurn()
    {
        _canFire = true;
    }
    public void IsHoldingFire(bool isHoldingFire)
    {
        //Debug.Log("Is holding fire: " + _holdingFire);
        _holdingFire = isHoldingFire;
    }
    public bool WeaponIsAutomatic()
    {
        return _data.IsAutomatic;
    }
    public int[] GetAmmo()
    {
        int[] ammo = new int[2];
        ammo[0] = _currentAmmo;
        ammo[1] = _maxAmmo;
        return ammo;
    }
}
