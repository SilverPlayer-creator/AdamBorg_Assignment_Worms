using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    protected int _damage;
    protected float _currentTimeAlive;
    public void Initialize(int damage)
    {
        _damage = damage;
    }
    private void Update()
    {
        _currentTimeAlive += Time.deltaTime;
        if(_currentTimeAlive >= 5)
            DisableObject();
    }
    private void OnCollisionEnter(Collision collision)
    {
        ActivePlayerHealth player = collision.gameObject.GetComponent<ActivePlayerHealth>();
        if(player != null)
        {
            player.TakeDamage(_damage);
        }
        gameObject.SetActive(false);
        DisableObject();
    }
    protected void DisableObject()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        _currentTimeAlive = 0;
        gameObject.SetActive(false);
    }
}
