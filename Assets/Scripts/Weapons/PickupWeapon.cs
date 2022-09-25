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
        //Debug.Log(_holdingFire);
        if(_isAutomatic && _holdingFire) //something wrong with holding fire bool, criteria not met
        {
            Debug.Log("Holding fire");
            if (Time.time >= _nextShootTime)
            {
                //Shoot();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHeldWeapons player = other.GetComponent<PlayerHeldWeapons>();
        if (player != null) 
        {
            //player.NewWeapon(this);
            //gameObject.SetActive(false);
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
        Debug.Log("Barrel position: " + _barrel.position);
        GameObject bullet = Instantiate(_prefab, _barrel.position, transform.rotation);
        bullet.AddComponent<WeaponProjectile>();
        bullet.GetComponent<WeaponProjectile>().Initialize(_damage);
        Debug.Log("Bullet instantiates at: " + bullet.transform.position);
        Rigidbody body = bullet.GetComponent<Rigidbody>();
        body.AddForce(transform.forward * _force);

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
}
