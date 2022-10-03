using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHeldWeapons : MonoBehaviour
{
    public int GrenadeAmount { get { return _grenadeAmount; } }
    public PlayerWeapon SelectedWeapon { get { return _selectedWeapon; } }
    [SerializeField] private List<PlayerWeapon> _heldWeapons;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _fireRate;
    [SerializeField] private PlayerWeapon _defaultWeapon;
    [SerializeField] private GameObject _grenadePrefab;
    [SerializeField] private float _throwForce;
    [SerializeField] private Transform _exit;
    [SerializeField] private WeaponTrajectory _trajectory;
    [SerializeField] private PlayerManager _manager;
    [SerializeField] private Image _activeWeaponImage;
    [SerializeField] private TextMeshProUGUI _ammoText;
    private int _grenadeAmount = 1;
    private bool _canFire;
    private List<PlayerWeapon> _activatedWeapons = new List<PlayerWeapon>();
    private PlayerWeapon _selectedWeapon;
    private int _selectedWeaponIndex;
    private bool _holdingFire;
    private void Awake()
    {
        _selectedWeapon = _defaultWeapon;
        _activatedWeapons.Add(_defaultWeapon);
        _selectedWeaponIndex = 0;
        _canFire = true;
        _activeWeaponImage.sprite = _selectedWeapon.Image;
    }
    private void Update()
    {
        if (_canFire)
        {
            if (_selectedWeapon.WeaponIsAutomatic && _holdingFire)
            {
                _selectedWeapon.IsHoldingFire(_holdingFire);
            }
            else if (_selectedWeapon.WeaponIsAutomatic && !_holdingFire)
            {
                _selectedWeapon.IsHoldingFire(false);
            }
            DisplayAmmo();
        }
        Vector3 force = _selectedWeapon.GetForce * transform.forward;
        _trajectory.DrawTrajectory(force, _exit.position, _selectedWeapon.GetPrefab);
    }
    public void HoldingFire(bool holdingFire)
    {
        _holdingFire = holdingFire;
    }
    public void SingleFire()
    {
        if (!_selectedWeapon.WeaponIsAutomatic)
            _selectedWeapon.Shoot();
    }
    public void Reload()
    {
        _selectedWeapon.Reload();
        DisplayAmmo();
    }
    public void SwitchWeapon(float input)
    {
        int currentSelectedWeaponIndex = _selectedWeaponIndex;
        bool switchingWeapons = true;
        if (switchingWeapons)
        {
            if (input >= 1)
            {
                if (_selectedWeaponIndex < _activatedWeapons.Count -1)
                {
                    _selectedWeaponIndex++;
                }
                else
                {
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
                    _selectedWeaponIndex = _activatedWeapons.Count - 1;
                }
            }
            _selectedWeapon = _activatedWeapons[_selectedWeaponIndex];
            switchingWeapons = false;
        }
        if(_selectedWeaponIndex != currentSelectedWeaponIndex)
        {
            AudioManager.AudioInstance().PlaySound("WeaponChange");
        }
        _activeWeaponImage.sprite = _selectedWeapon.Image;
        DisplayAmmo();
    }
    public void NewTurn()
    {
        for (int i = 0; i < _heldWeapons.Count; i++)
        {
            _heldWeapons[i].PlayerTurn();
            _canFire = true;
            _holdingFire = false;
        }
        _activeWeaponImage.sprite = _manager.GetCurrentPlayer.WeaponHolder.SelectedWeapon.Image;
    }
    void DisplayAmmo()
    {
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
        _grenadeAmount--;
    }
    public void IncreaseDamage(int increaseAmount)
    {
        _selectedWeapon.IncreaseDamage(increaseAmount);
    }
    public void AddWeapon(string newWeaponname)
    {
        foreach (PlayerWeapon weapon in _heldWeapons)
        {
            if(weapon.gameObject.name == newWeaponname && !weapon.gameObject.activeSelf)
            {
                weapon.gameObject.SetActive(true);
                _activatedWeapons.Add(weapon);
            }
        }
    }
    public void AddGrenades()
    {
        _grenadeAmount += 2;
    }
}
