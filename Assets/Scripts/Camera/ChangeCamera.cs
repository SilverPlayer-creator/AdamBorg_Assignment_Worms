using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    private Vector3 _savedPosition;
    private Quaternion _savedRotation;
    Transform _playerParent;
    bool _focusingOnPlayer = true;

    private void Awake()
    {
        _playerParent = transform.parent;
    }
    public void GetCurrentPosition()
    {
        _savedPosition = transform.position;
        _savedRotation = transform.rotation;
        _focusingOnPlayer = false;
    }
    public void SetPosition()
    {
        transform.position = _savedPosition;
        transform.rotation = _savedRotation;
        transform.SetParent(_playerParent);
        _focusingOnPlayer = true;
    }
    private void Update()
    {
        if (!_focusingOnPlayer)
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
