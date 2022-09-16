using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Pistol : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _barrel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && GetComponent<PlayerController>().IsActivePlayer)
        {
            GameObject newProjectile = Instantiate(_projectilePrefab);
            newProjectile.transform.position = _barrel.position;
            newProjectile.GetComponent<Projectile>().Initialize(_barrel);
        }
    }
}
