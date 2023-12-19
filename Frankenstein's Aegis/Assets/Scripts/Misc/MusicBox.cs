using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : AudioSourceManager
{
    public AudioClip ambianceMusic;
    public AudioClip victoryMusic;
    AudioSourceManager asm;

    static MusicBox instance = null;

    public static MusicBox Instance => instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);

        asm = GetComponent<AudioSourceManager>();
        if (!asm) Debug.Log("Please give the music box a source manager");

        PlayAmbianceMusic();
        //GameManager.Instance.OnDeath.AddListener(ShowGameOver);
    }

    public void PlayAmbianceMusic()
    {
        asm.PlayOneShot(ambianceMusic, true);
    }

    public void PlayVictoryMusic()
    {
        // Stop the currently playing ambiance music
        StopAmbianceMusic();

        // Play victory music
        asm.PlayOneShot(victoryMusic, true);
    }

    private void StopAmbianceMusic()
    {
        List<AudioSource> currentAudioSources = asm.GetCurrentAudioSources();

        foreach (AudioSource source in currentAudioSources)
        {
            if (source.clip == ambianceMusic && source.isPlaying)
            {
                source.Stop();
                return;
            }
        }
    }
}
