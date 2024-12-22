using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip[] musicClips; // Add your music tracks here
    public AudioClip[] sfxClips;   // Add your sound effects here

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist the AudioManager between scenes
    }

    /// <summary>
    /// Play a music track by index.
    /// </summary>
    /// <param name="index">Index of the music track in the musicClips array.</param>
    public void PlayMusic(int index)
    {
        if (musicSource.isPlaying && musicSource.clip == musicClips[index])
        {
            // Music is already playing, no need to restart
            return;
        }

        if (index >= 0 && index < musicClips.Length)
        {
            musicSource.clip = musicClips[index];
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid music index!");
        }
    }

    /// <summary>
    /// Play a sound effect by index.
    /// </summary>
    /// <param name="index">Index of the SFX in the sfxClips array.</param>
    public void PlaySFX(int index)
    {
        if (index >= 0 && index < sfxClips.Length)
        {
            sfxSource.PlayOneShot(sfxClips[index]);
        }
        else
        {
            Debug.LogWarning("Invalid SFX index!");
        }
    }

    /// <summary>
    /// Stop the currently playing music.
    /// </summary>
    public void StopMusic()
    {
        musicSource.Stop();
    }
}
