using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Sound 
{
    [SerializeField] private string _soundName;
    public string SoundName { get { return _soundName; } }
    [SerializeField] private AudioClip _soundClip;
    public AudioClip SoundClip { get { return _soundClip; } }
}
