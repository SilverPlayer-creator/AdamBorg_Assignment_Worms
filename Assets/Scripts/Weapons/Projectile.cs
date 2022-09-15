using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody _body;

    private bool _isActive;
    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {
            //transform.Translate(transform.forward * _speed * Time.deltaTime);
            //_body.MovePosition(transform.position + transform.forward * _speed * Time.deltaTime);

            
        } 
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
            damageableObject.TakeDamage(0);
        }
        Destroy(gameObject);
    }
}
