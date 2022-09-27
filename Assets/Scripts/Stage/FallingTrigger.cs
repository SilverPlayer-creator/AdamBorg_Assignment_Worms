using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        ActivePlayerHealth player = other.GetComponent<ActivePlayerHealth>();
        if (player != null)
            player.TakeDamage(500);
    }
}
