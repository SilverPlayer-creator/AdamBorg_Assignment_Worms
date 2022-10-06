using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<Sound> _sounds;
    private AudioSource _source;

    private static AudioManager _audioInstance;

    private void Awake()
    {
        if (_audioInstance == null)
        {
            _audioInstance = this;
        }
        else
        {
            Destroy(this);
        }
        _source = GetComponent<AudioSource>();
    }
    public static AudioManager Instance => _audioInstance;
    public void PlaySound(string soundName)
    {
        foreach (Sound sound in _sounds)
        {
            if(soundName == sound.SoundName)
            {
                _source.PlayOneShot(sound.SoundClip, 0.5f);
            }
        }
    }
}
