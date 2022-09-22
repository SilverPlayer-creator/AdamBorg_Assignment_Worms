using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody _body;

    private bool _isActive;
    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }
    public void Initialize(Transform _forward)
    {
        _isActive = true;
        _body.AddForce((_forward.forward + transform.up) * 200f);
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
