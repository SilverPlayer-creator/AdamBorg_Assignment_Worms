using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _smoothness;
    [SerializeField] private Transform _lookTarget;
    [SerializeField] private Transform _followTarget;
    [SerializeField] private float _maxX, _maxZ, _minX, _minZ;
    private Vector3 _grenadeOffset;
    private bool _followingGrenade;
    private bool _gameHasEnded;
    private void FixedUpdate()
    {
        if (!_followingGrenade && !_gameHasEnded)
        {
            Vector3 desiredPos = _followTarget.position;
            Vector3 follow = Vector3.Lerp(transform.position, desiredPos, _smoothness * Time.fixedDeltaTime);
            transform.position = follow;
        }
        else
            transform.position = _followTarget.position + _grenadeOffset;
        transform.LookAt(_lookTarget);
    }
    public void ChangePlayer(Transform newPlayer)
    {
        _lookTarget = newPlayer.GetChild(0);
        _followTarget = newPlayer.GetChild(1);
        Vector3 faceDirection = -newPlayer.transform.forward;
        _followingGrenade = false;
    }
    public void LookAtGrenade(Transform grenade)
    {
        _lookTarget = grenade.GetChild(0);
        _followTarget = grenade;
        Vector3 faceDirection = -grenade.transform.forward;
        _followingGrenade = true;
        _grenadeOffset = RandomPosition();
    }
    private Vector3 RandomPosition()
    {
        Vector3 newPos = new Vector3();
        newPos.x = Random.Range(-2f, 2f);
        newPos.y = Random.Range(1f, 5f);
        newPos.z = Random.Range(3f, 6f);
        return newPos;
    }
}
