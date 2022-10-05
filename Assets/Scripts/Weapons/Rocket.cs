using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : WeaponProjectile
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private GameObject _explosion;
    private void OnCollisionEnter(Collision collision)
    {
        ActivePlayerHealth player = collision.gameObject.GetComponent<ActivePlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(_damage);
        }
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, _explosionRadius);
        List<ActivePlayerHealth> hitPlayers = new List<ActivePlayerHealth>();
        foreach (Collider collider in hitObjects)
        {
            ActivePlayerHealth _player = collider.gameObject.GetComponent<ActivePlayerHealth>();
            if (player != null && !hitPlayers.Contains(_player))
            {
                player.TakeDamage(_damage);
                hitPlayers.Add(_player);
                Debug.Log("Player caught in radius");
            }
        }
        GameObject explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
        AudioManager.AudioInstance().PlaySound("RocketCollide");
        gameObject.SetActive(false);
    }
}
