using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private Vector3 _playerOffset;
    [SerializeField] private Transform _focusedPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Vector3 SwitchPlayer(Transform newPlayer)
    {
        return newPlayer.position + _playerOffset;
    }
}
