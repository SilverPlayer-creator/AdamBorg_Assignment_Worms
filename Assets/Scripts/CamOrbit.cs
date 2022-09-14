using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamOrbit : MonoBehaviour
{
    [SerializeField]
    private Transform followTarget;
    [Header("Follow Options")]
    [SerializeField]
    private float distance = 10f;
    [Header("Look Options")]
    [SerializeField]
    private Vector2 sensitivity = new(500, 800);
    [SerializeField, Range(-89f, 89f)]
    private float minYAngle = -30f, maxYAngle = 89f;

    private Vector2 lookAngles = new(45, 0);
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        GetAngles();
        ConstrainAngles();
        UpdateTracking();
    }

    private void GetAngles()
    {
        Vector2 input = new Vector2(
            Input.GetAxis("Mouse Y"),
            Input.GetAxis("Mouse X")
        );

        lookAngles += sensitivity * Time.deltaTime * input;
    }

    private void ConstrainAngles()
    {
        lookAngles.x = Mathf.Clamp(lookAngles.x, minYAngle, maxYAngle);

        if (lookAngles.y < 0f)
        {
            lookAngles.y += 360f;
        }
        else if (lookAngles.y >= 360f)
        {
            lookAngles.y -= 360f;
        }
    }

    private void UpdateTracking()
    {
        Quaternion lookRotation = Quaternion.Euler(lookAngles);
        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = followTarget.position - lookDirection * distance;
        transform.SetPositionAndRotation(lookPosition, lookRotation);
    }
}
