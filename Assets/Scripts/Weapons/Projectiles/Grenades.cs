using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenades : MonoBehaviour
{
    [SerializeField] private float _throwForce;
    [SerializeField] private int _damage;
    [SerializeField] private float _timer;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private GameObject _mesh;
    [SerializeField] private GameObject _explosion;
    private bool _exploded;
    void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer <= 0 && !_exploded)
        {
            Collider[] hitObjects = Physics.OverlapSphere(transform.position, _explosionRadius);
            List<ActivePlayerHealth> hitPlayers = new List<ActivePlayerHealth>();
            foreach (Collider collider in hitObjects)
            {
                ActivePlayerHealth player = collider.gameObject.GetComponent<ActivePlayerHealth>();
                if(player != null && !hitPlayers.Contains(player))
                {
                    Debug.Log("Damage");
                    player.TakeDamage(_damage);
                    hitPlayers.Add(player);
                }
            }
            StartCoroutine(Explode());
            _exploded = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
    private IEnumerator Explode()
    {
        _mesh.SetActive(false);
        AudioManager.Instance.PlaySound("GrenadeExp");
        GameObject explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
        Rigidbody body = GetComponent<Rigidbody>();
        body.velocity = Vector3.zero;
        body.drag = 50;
        body.isKinematic = true;
        yield return new WaitForSeconds(3f);
        TurnManager.TurnInstance.QuickEnd();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
