using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private WeaponData _data;
    [SerializeField] private Transform _barrel;
    public Sprite Image { get { return _data.Icon; } }
    private int _currentDamage;
    private int _currentAmmo;
    private float _nextShootTime;
    private bool _holdingFire;
    private bool _canFire = true;
    private void Awake()
    {
        _currentDamage = _data.Damage;
        _currentAmmo = _data.MaxAmmo;
    }
    private void Update()
    {
        if(_data.IsAutomatic && _holdingFire && _canFire) 
        {
           if (Time.time >= _nextShootTime)
            {
                Shoot();
            }
        }
    }
    public void Shoot()
    {
        if(_currentAmmo > 0)
        {
            GameObject projectile = ProjectilePooler.PoolInstance.SpawnFromPool(_data.WeaponName, _barrel.position, _data.Prefab.transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(_barrel.transform.forward * _data.Force);
            projectile.GetComponent<WeaponProjectile>().Initialize(_currentDamage);
            _nextShootTime = Time.time + 1f / _data.FireRate;
            _currentAmmo--;
            TurnManager.TurnInstance.DecreaseTimeRemaining(_data.TimeDecrease);
            AudioManager.AudioInstance().PlaySound(_data.WeaponName);
        }
        else
            Reload();
    }
    public void Reload()
    {
        if(_currentAmmo < _data.MaxAmmo)
        {
            _currentAmmo = _data.MaxAmmo;
            _holdingFire = false;
            _canFire = false;
            TurnManager.TurnInstance.QuickEnd();
            AudioManager.AudioInstance().PlaySound("Reload");
        }
    }
    public void PlayerTurn()
    {
        _canFire = true;
    }
    public void IsHoldingFire(bool isHoldingFire)
    {
        _holdingFire = isHoldingFire;
    }
    public bool WeaponIsAutomatic => _data.IsAutomatic;

    public int[] GetAmmo()
    {
        int[] ammo = new int[2];
        ammo[0] = _currentAmmo;
        ammo[1] = _data.MaxAmmo;
        return ammo;
    }
    public void IncreaseDamage(int increasedDamage)
    {
        _currentDamage += increasedDamage;
    }
    public GameObject GetPrefab => _data.Prefab;
    public int GetForce => _data.Force;
}
