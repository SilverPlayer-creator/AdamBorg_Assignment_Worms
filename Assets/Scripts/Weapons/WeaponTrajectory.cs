using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrajectory : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _stepCount = 10;
    public void DrawTrajectory(Vector3 force, Vector3 startPos, GameObject prefab)
    {
        float projectileMass = prefab.GetComponent<Rigidbody>().mass;
        Vector3 velocity = (force / projectileMass) * Time.fixedDeltaTime;
        float flightDuration = (2 * velocity.y) - Physics.gravity.y;
        float stepTime = flightDuration / (float)_stepCount;

        _lineRenderer.positionCount = _stepCount;

        for (int i = 0; i < _stepCount; i++)
        {
            float timePassed = stepTime * i;
            float height = velocity.y * timePassed - (0.5f * -Physics.gravity.y * timePassed * timePassed);
            Vector3 curvePoint = startPos + new Vector3(velocity.x * timePassed, height, velocity.z * timePassed);
            _lineRenderer.SetPosition(i, curvePoint);
        }
    }
}
