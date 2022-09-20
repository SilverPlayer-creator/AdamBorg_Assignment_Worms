using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    private List<Weapon> _heldWeapons;
    [SerializeField] private Weapon _defaultWeapon;
    // Start is called before the first frame update
    void Awake()
    {
        _heldWeapons = new List<Weapon>();
        _heldWeapons.Add(_defaultWeapon);
        foreach (Weapon weapon in _heldWeapons)
        {
            if (weapon != null)
            {
                weapon.Initialize();
            }
        }
    }
    private void Start()
    {
        
    }
    public void FireHeldWeapon(bool _inputPressed)
    {
        if (_heldWeapons[0].IsAutomatic)
        {
            _heldWeapons[0].HoldingFire(_inputPressed);
            Debug.Log("Holding");
        }
    }
    public void ReloadHeldWeapon()
    {
        _heldWeapons[0].Reload();
    }
}
