using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "WeaponData", menuName = "WeaponData")]
public class WeaponData : ScriptableObject
{
    public int Damage;
    public int MaxAmmo;
    public string WeaponName;
    public float ShootRate;
    public bool IsAutomatic;
    public int Force;
    public GameObject Prefab;
    public Sprite Icon;
}
