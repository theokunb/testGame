using System;
using System.Linq;
using UnityEngine;

public class SoundContainer : MonoBehaviour
{
    [SerializeField] private AudioClip _backgroundMusic;

    private AudioSource[] _audiSources;

    private void Awake()
    {
        _audiSources = GetComponentsInChildren<AudioSource>();
    }

    private void Start()
    {
        ConfigureFreeAudioSource(audiosource =>
        {
            audiosource.clip = _backgroundMusic;
            audiosource.volume = 0.15f;
            audiosource.loop = true;
        });
    }

    public void ConfigureFreeAudioSource(Action<AudioSource> audioSource)
    {
        var freeSource = _audiSources.FirstOrDefault(element => element.isPlaying == false);

        if(freeSource == null)
        {
            return;
        }

        audioSource?.Invoke(freeSource);

        freeSource.Play();
    }
}
