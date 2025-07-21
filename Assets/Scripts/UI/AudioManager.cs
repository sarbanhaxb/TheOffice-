using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {  get; private set; }

    private List<AudioSource> _audioSources = new List<AudioSource>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterAudioSource(AudioSource source)
    {
        if (!_audioSources.Contains(source))
        {
            _audioSources.Add(source);
        }
    }

    public void UpdateAllAudioPitches(float speedMultiplier)
    {
        foreach (AudioSource source in _audioSources)
        {
            source.pitch = speedMultiplier;
        }
    }
}
