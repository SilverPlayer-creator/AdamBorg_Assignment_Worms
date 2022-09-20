using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenades : MonoBehaviour
{
    [SerializeField] private float _throwForce;
    [SerializeField] private int _damage;
    private Rigidbody _body;
    [SerializeField] private float _timer;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private LayerMask _playerLayer;
    private PlayerUnit _thrownPlayer;
    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer <= 0)
        {
            Debug.Log("Grenade explosion.");
            Collider[] hitObjects = Physics.OverlapSphere(transform.position, _explosionRadius);
            foreach (Collider collider in hitObjects)
            {
                PlayerStats player = collider.gameObject.GetComponent<PlayerStats>();
                if(player != null)
                {
                    player.TakeDamage(_damage);
                }
            }
            _thrownPlayer.CanMove(true);
            Destroy(gameObject);
        }
    }
    public void Initialize(PlayerUnit player)
    {
        _body.AddForce((transform.forward + transform.up) * _throwForce);
        _thrownPlayer = player;
        _thrownPlayer.CanMove(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
