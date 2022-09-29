using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float _speed;
    protected Rigidbody _body;
    [SerializeField] protected GameObject _prefab;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }
    public void Initialize(Transform forward)
    {
        _body.AddForce((forward.forward + transform.up) * 200f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;
        IDamageable damageableObject = collisionObject.GetComponent<IDamageable>();
        if(damageableObject != null)
        {
            damageableObject.TakeDamage(5);
        }
        Destroy(gameObject);
    }
}
