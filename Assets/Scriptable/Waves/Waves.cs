using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Waves : ScriptableObject
{
    public string WaveName;
    public List<GameObject> Pickups = new List<GameObject>();
    public float SpawnTime;
}
