using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _playerToFollow;
    [SerializeField] private Vector3 _distance;
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _smoothness;
    [SerializeField] private Transform _lookTarget;
    [SerializeField] private Transform _followTarget;

    private void FixedUpdate()
    {
        Vector3 desiredPos = _followTarget.position;
        Vector3 follow = Vector3.Lerp(transform.position, desiredPos, _smoothness * Time.fixedDeltaTime);
        transform.position = follow;
        transform.LookAt(_lookTarget);
    }
    public void ChangePlayer(Transform newPlayer)
    {
        _playerToFollow = newPlayer;
        _lookTarget = newPlayer.GetChild(0);
        _followTarget = newPlayer.GetChild(1);

        Vector3 faceDirection = -newPlayer.transform.forward;
    }
}
