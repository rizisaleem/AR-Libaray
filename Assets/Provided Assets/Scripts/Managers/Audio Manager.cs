using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource audioSource;
    private Dictionary<string, AudioSource> sounds = new Dictionary<string, AudioSource>();

    private bool sfxEnabled = true;
    private float currentVolume;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        AudioSource[] sources = GetComponents<AudioSource>();

        foreach (AudioSource src in sources)
        {
            if (src == null || src.clip == null) continue;

            if (src.loop)
            {
                audioSource = src;
                audioSource.Play(); 
            }
            else
                sounds[src.clip.name] = src;
        }
    }

    public void PlaySound(string soundName)
    {
        if (!sfxEnabled) return;

        if (sounds.TryGetValue(soundName, out AudioSource src))
            src.Play();
        else
            Debug.LogWarning($"Sound: '{soundName}' not found.");
    }

    public float GetCurrentVolume()
    {
        return currentVolume;
    }

    public void VolumeControl(float newVal)
    {
        currentVolume = newVal;

        if (audioSource != null)
            audioSource.volume = newVal;

        foreach (var src in sounds.Values)
            src.volume = newVal;
    }

    public void TurnSFXOnOff(bool turnOn)
    {
        sfxEnabled = turnOn;

        foreach (var src in sounds.Values)
            src.mute = !turnOn;
    }

    public bool IsSFXEnabled()
    {
        return sfxEnabled;
    }

    public void TurnMusicOnOff(bool turnOn)
    {
        if (audioSource == null) return;

        if (turnOn && !audioSource.isPlaying)
            audioSource.Play();
        else if (!turnOn && audioSource.isPlaying)
            audioSource.Stop();
    }

    public bool IsMusicPlaying()
    {
        return audioSource != null && audioSource.isPlaying;
    }
}
