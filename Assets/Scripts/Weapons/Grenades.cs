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
    private bool _exploded;
    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }
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
            if (!_exploded)
            {
                StartCoroutine(Explode());
                _exploded = true;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
    private IEnumerator Explode()
    {
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        PlayerManager.GetInstance().StartCoroutine(PlayerManager.GetInstance().EndCurrentTurn());
    }
}
