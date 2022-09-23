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
    public float FireRate
    {
        get { return _fireRate; }
    }
    private float _nextShootTime;
    private bool _isAutomatic;
    public bool IsAutomatic
    {
        get { return _isAutomatic; }
    }
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

        _nextShootTime = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHeldWeapons player = other.GetComponent<PlayerHeldWeapons>();
        if (player != null) 
        {
            //player.NewWeapon(this);
            gameObject.SetActive(false);
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
    public void Shoot(Transform barrel)
    {
        Debug.Log("Shoot");

            GameObject bullet = Instantiate(_prefab, _barrel.position, Quaternion.identity);
            Rigidbody body = bullet.GetComponent<Rigidbody>();
            body.AddForce(transform.forward * _force);

    }
}
