using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ActivePlayerHealth player = other.GetComponent<ActivePlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(500);
            TurnManager.TurnInstance.QuickEnd();
        }
    }
}
