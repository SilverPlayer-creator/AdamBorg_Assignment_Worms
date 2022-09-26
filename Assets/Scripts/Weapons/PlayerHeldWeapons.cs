using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHeldWeapons : MonoBehaviour
{
    [SerializeField] private List<PickupWeapon> _heldWeapons;
    private PickupWeapon _selectedWeapon;
    private int _selectedWeaponIndex;
    private bool _holdingFire;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _fireRate;
    [SerializeField] private PickupWeapon _defaultWeapon;
    bool _canFire;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private GameObject _grenadePrefab;
    [SerializeField] private float _throwForce;
    [SerializeField] private Transform _exit;

    private void Awake()
    {
        _selectedWeapon = _defaultWeapon;
        _selectedWeaponIndex = 0;
        _canFire = true;
    }
    private void Update()
    {
        if (_canFire)
        {
            if (_selectedWeapon.WeaponIsAutomatic() && _holdingFire)
            {
                _selectedWeapon.IsHoldingFire(_holdingFire);
                Debug.Log(transform.name + " is shooting");
            }
            else if (_selectedWeapon.WeaponIsAutomatic() && !_holdingFire)
            {
                _selectedWeapon.IsHoldingFire(false);
            }
            else if(!_selectedWeapon.WeaponIsAutomatic() && _holdingFire)
            {
                _selectedWeapon.Shoot();
                _canFire = false;
            }
        }
    }
    public void HoldingFire(bool holdingFire)
    {
        _holdingFire = holdingFire;
    }
    public void Shoot()
    {
        _selectedWeapon.Shoot();
        //DisplayAmmo();
    }
    public void Reload()
    {
        _selectedWeapon.Reload();
        //DisplayAmmo();
    }
    public void SwichWeapon(float input)
    {
        bool switchingWeapons = true;
        Debug.Log("Held weapons: " + _heldWeapons.Count);
        if (switchingWeapons)
        {
            if (input >= 1)
            {
                if (_selectedWeaponIndex < _heldWeapons.Count)
                {
                    _selectedWeaponIndex++;
                    Debug.Log("Going up by one weapon");
                }
                if(_selectedWeaponIndex >= _heldWeapons.Count)
                {
                    Debug.Log("At final weapon, switching to first weapon");
                    _selectedWeaponIndex = 0;
                }
            }
            if (input <= -1)
            {
                if (_selectedWeaponIndex > 0)
                {
                    _selectedWeaponIndex--;
                }
                else
                {
                    _selectedWeaponIndex = 0;
                }
            }
            Debug.Log("Weapon index: " + _selectedWeaponIndex);
            _selectedWeapon = _heldWeapons[_selectedWeaponIndex];
            switchingWeapons = false;
        }
        //DisplayAmmo();
    }
    public void NewTurn()
    {
        for (int i = 0; i < _heldWeapons.Count; i++)
        {
            _heldWeapons[i].PlayerTurn();
            Debug.Log("Next players turn");
            _canFire = true;
            _holdingFire = false;
        }
    }
    void DisplayAmmo()
    {
        Debug.Log("Display");
        int[] ammo = _selectedWeapon.GetAmmo();
        int currentAmmo = ammo[0];
        int maxAmmo = ammo[1];
        string currentAmmoText = currentAmmo.ToString();
        string maxAmmoText = maxAmmo.ToString();
        _ammoText.text = currentAmmoText + "/" + maxAmmoText;
    }
    public void ThrowGrenade()
    {
        GameObject grenade = Instantiate(_grenadePrefab, _exit.position, Quaternion.identity);
        Rigidbody body = grenade.GetComponent<Rigidbody>();
        body.AddForce(transform.forward * _throwForce);
        PlayerManager.GetInstance().GetComponent<ActivePlayerInput>().SetCanMove(false);
    }
}
