using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
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
    private int _timeDecrease;
    private bool _canFire = true;
    [SerializeField] private Transform _barrel;
    [SerializeField]private GameObject _prefab;
    private Sprite _image;
    public Sprite Image

    {
        get { return _image; }
    }
    private AudioClip _fireSound;
    private AudioSource _source;
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
        _timeDecrease = _data.TimeDecrease;
        _image = _data.Icon;
        _fireSound = _data.FireSound;
        _source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(_isAutomatic && _holdingFire && _canFire) 
        {
           if (Time.time >= _nextShootTime)
            {
                Shoot();
            }
        }
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
            GameObject bullet = Instantiate(_prefab, _barrel.position, Quaternion.identity);
            bullet.GetComponent<WeaponProjectile>().Initialize(_damage);
            Rigidbody body = bullet.GetComponent<Rigidbody>();
            body.AddForce(transform.forward * _force);
            _nextShootTime = Time.time + 1f / _fireRate;
            _currentAmmo--;
            PlayerManager manager = PlayerManager.GetInstance();
            manager.DecreaseTimeRemaining(_timeDecrease);
            AudioManager.AudioInstance().PlaySound(_weaponName);
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
            Debug.Log("Reloading " + transform.name);
            _currentAmmo = _maxAmmo;
            _holdingFire = false;
            PlayerManager manager = PlayerManager.GetInstance();
            _canFire = false;
            manager.StartCoroutine(manager.EndCurrentTurn());
            //AudioManager.AudioInstance().PlaySound("Reload");
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
    public void IncreaseDamage(int increasedDamage)
    {
        _damage += increasedDamage;
    }
    public GameObject GetPrefab()
    {
        return _prefab;
    }
    public int GetForce()
    {
        return _force;
    }
}
