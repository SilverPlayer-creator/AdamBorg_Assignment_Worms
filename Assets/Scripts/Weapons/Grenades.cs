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
            Collider[] hitObjects = Physics.OverlapSphere(transform.position, _explosionRadius);
            List<ActivePlayerHealth> hitPlayers = new List<ActivePlayerHealth>();
            foreach (Collider collider in hitObjects)
            {
                ActivePlayerHealth player = collider.gameObject.GetComponent<ActivePlayerHealth>();
                if(player != null && !hitPlayers.Contains(player))
                {
                    player.TakeDamage(_damage);
                    hitPlayers.Add(player);
                }
            }
            PlayerManager.GetInstance().StartCoroutine(PlayerManager.GetInstance().EndCurrentTurn());
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
