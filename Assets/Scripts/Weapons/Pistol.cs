using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    private bool _isShooting;

    // Update is called once per frame
    void Update()
    {
        if (_holdingFire)
        {
            if (Time.time >= _nextShootTime)
            {
                //Shoot();
                _nextShootTime = Time.time + 1 / _shootRate;
            }
        }
    }
    public void IsShooting(bool isShooting)
    {
        _isShooting = isShooting;
    }
    //public override void Shoot()
    //{
    //        if (_currentAmmo > 0)
    //        {
    //            _currentAmmo--;
    //            base.Shoot();
    //            GameObject newProjectile = Instantiate(_projectilePrefab);
    //            newProjectile.transform.position = _barrel.position;
    //            newProjectile.GetComponent<Projectile>().Initialize(_barrel);
    //            //OnAmmoChangedEvent?.Invoke(_currentAmmo);
    //            //EventInvoke();
    //            //Debug.Log("Current ammo: " + _currentAmmo);
    //        }
    //        else
    //        {
    //            Reload(_index);
    //        }
    //}
}
