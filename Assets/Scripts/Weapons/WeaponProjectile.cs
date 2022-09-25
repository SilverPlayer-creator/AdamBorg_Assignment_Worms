using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    private int _damage;
    public void Initialize(int damage)
    {
        _damage = damage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        ActivePlayerHealth player = collision.gameObject.GetComponent<ActivePlayerHealth>();
        if(player != null)
        {
            player.TakeDamage(_damage);
        }
        Destroy(gameObject);
    }
}
