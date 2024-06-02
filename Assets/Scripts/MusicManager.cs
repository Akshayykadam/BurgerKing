using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume;";

    private AudioSource audioSource;
    private float volume = .3f;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, .3f);
    }

    public void ChangeVolume(float newVolume)
    {
        volume = Mathf.Clamp(newVolume, 0f, 1f);
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
    }

    public float GetVolume()
    {
        return volume;
    }
}
