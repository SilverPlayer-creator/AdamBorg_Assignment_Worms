using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeldWeapons : MonoBehaviour
{
    private List<PickupWeapon> _heldWeapons;
    private PickupWeapon _selectedWeapon;
    private bool _holdingFire;
    [SerializeField] private Transform _barrel;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _fireRate;
    private float _nextShootTime;
    private bool _isAutomatic = true;
    private bool _isActivePlayer;

    [SerializeField] private PickupWeapon _defaultWeapon;

    private void Awake()
    {
        _selectedWeapon = _defaultWeapon;
    }
    private void Update()
    {
        //Debug.Log(_holdingFire);
        if(_holdingFire && _isActivePlayer)
        {
            _selectedWeapon.Shoot(_barrel);
            //Debug.Log("Shoot");
        }
    }
    public void HoldingFire(bool holdingFire, bool isActivePlayer)
    {
        _holdingFire = holdingFire;
        _isActivePlayer = isActivePlayer;
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(_projectile, _barrel.position, Quaternion.identity);
        Rigidbody body = bullet.GetComponent<Rigidbody>();
        body.AddForce(_barrel.transform.forward * 200);
        _nextShootTime = Time.time + 1f / _fireRate;
    }
}
