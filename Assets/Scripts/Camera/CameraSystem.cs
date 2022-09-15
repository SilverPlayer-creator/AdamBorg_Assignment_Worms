using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private float _smoothness;
    [SerializeField] private Vector3 _playerOffset;
    [SerializeField] private Transform _focusedPlayer;
    private Vector3 _cameraPosition;
    private Vector3 _initialOffset;
    private Vector3 _currentOffset;
    // Start is called before the first frame update
    void Start()
    {
        _initialOffset = transform.position - _focusedPlayer.position;
        _currentOffset = _initialOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Rotate();
        }
    }
    private void FixedUpdate()
    {
        _cameraPosition = _focusedPlayer.position + _currentOffset;
        transform.position = Vector3.Lerp(transform.position, _cameraPosition, _smoothness * Time.fixedDeltaTime);
    }
    Vector3 SwitchPlayer(Transform newPlayer)
    {
        return newPlayer.position + _playerOffset;
    }
    void Rotate()
    {
        Vector3 newOffset = new Vector3(_focusedPlayer.position.x + _playerOffset.x, _focusedPlayer.position.y + _playerOffset.y, _focusedPlayer.position.z + _playerOffset.z);
            _currentOffset = transform.position + newOffset;

    }
}
