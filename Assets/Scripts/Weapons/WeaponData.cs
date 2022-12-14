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
    public float FireRate;
    public bool IsAutomatic;
    public float ForwardForce;
    public float UpwardForce;
    public int TimeDecrease;
    public GameObject Prefab;
    public Sprite Icon;
    public AudioClip FireSound;
}
