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
    // Start is called before the first frame update
    void Start()
    {
        _initialOffset = transform.position - _focusedPlayer.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    Rotate();
        //}
    }
    private void FixedUpdate()
    {
        _cameraPosition = _focusedPlayer.position + _initialOffset;
        //transform.position = Vector3.Lerp(transform.position, _cameraPosition, _smoothness * Time.fixedDeltaTime);
    }
}
