using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int _healthGain;
    private void OnTriggerEnter(Collider other)
    {
        ActivePlayerHealth player = other.GetComponent<ActivePlayerHealth>();
        if (player != null)
        {
            player.AddHealth(_healthGain);
            Destroy(gameObject);
        }
    }
}
