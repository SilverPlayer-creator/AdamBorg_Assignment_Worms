using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePickup : MonoBehaviour
{
    [SerializeField] private int _damageGain;
    private void OnTriggerEnter(Collider other)
    {
        PlayerHeldWeapons player = other.GetComponent<PlayerHeldWeapons>();
        if (player != null)
        {
            player.IncreaseDamage(_damageGain);
            Destroy(gameObject);
        }
    }
}
