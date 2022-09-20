using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Grenades _grenadePrefab;
    
    public void Throw(PlayerUnit player)
    {
        Grenades newGrenade = Instantiate(_grenadePrefab, _spawnPoint.position, Quaternion.identity);
        newGrenade.Initialize(player);
    }
}
