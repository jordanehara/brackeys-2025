using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // "speakers" to play the sounds
    [SerializeField] AudioSource musicChannel;
    [SerializeField] List<AudioSource> sfxChannels = new List<AudioSource>();
    int currentSFXChannel = 0;
    int highestSFXChannel = 0;

    // sound references
    [Space(10)]
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic1;
    [SerializeField] AudioClip gameMusic2;
    [SerializeField] AudioClip gameMusic3;
    [Space(10)]
    [SerializeField] AudioClip speakingSFX;
    [SerializeField] AudioClip hurtSFX;
    [SerializeField] AudioClip clickSFX;

    void Awake()
    {
        if (instance == null) instance = this;

        highestSFXChannel = sfxChannels.Count - 1;
    }

    #region Music
    void PlayMusic(AudioClip music, float volume)
    {
        if (music == null) return;

        if (musicChannel.clip != music && musicChannel != null)
        {
            musicChannel.Stop();
            musicChannel.clip = music;
            musicChannel.Play();
            if (volume > 0)
            {
                musicChannel.volume = volume;
            }
        }
    }

    public void PlayMenuMusic(float volume = 0)
    {
        PlayMusic(menuMusic, volume);
    }

    public void PlayGameMusic(float volume = 0)
    {
        PlayMusic(gameMusic1, volume);
    }
    #endregion

    #region SFX
    void PlaySoundEffect(AudioClip sfx, float volume = 0, float pitch = 1f)
    {
        if (sfx == null) return;

        NextSFXChannel();
        if (!sfxChannels[currentSFXChannel].isPlaying)
        {
            sfxChannels[currentSFXChannel].Stop();
            sfxChannels[currentSFXChannel].clip = sfx;
            sfxChannels[currentSFXChannel].Play();
            sfxChannels[currentSFXChannel].pitch = pitch;
            if (volume > 0)
            {
                sfxChannels[currentSFXChannel].volume = volume;
            }
        }
    }

    public void PlayCustomSFX(AudioClip customSFX, float volume = 0, float pitch = 1f)
    {
        PlaySoundEffect(customSFX, volume, pitch);
    }

    void NextSFXChannel()
    {
        currentSFXChannel++;
        if (currentSFXChannel > highestSFXChannel)
        {
            currentSFXChannel = 0;
        }
    }

    public void PlaySpeakingSound(float volume = 0, float pitch = 1f)
    {
        PlaySoundEffect(speakingSFX, volume, pitch);
    }

    public void PlayHurtSound(float volume = 0, float pitch = 1f)
    {
        PlaySoundEffect(hurtSFX, volume, pitch);
    }

    public void PlayClickSound(float volume = 0, float pitch = 1f)
    {
        PlaySoundEffect(clickSFX, volume, pitch);
    }
    #endregion
}
