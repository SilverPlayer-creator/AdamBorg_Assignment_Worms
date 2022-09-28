using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private string _weaponName;
    
    public string WeaponName()
    {
        return _weaponName;
    }
}
