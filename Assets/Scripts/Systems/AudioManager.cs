using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] AudioClip gameMusic1;
    [SerializeField] AudioClip gameMusic2;
    [SerializeField] AudioClip gameMusic3;
    [Space(10)]
    [SerializeField] AudioClip speakingSFX;
    [SerializeField] AudioClip hurtSFX;
    [SerializeField] AudioClip clickSFX;
    [SerializeField] AudioClip buttonPushSFX;
    [SerializeField] AudioClip buttonReleaseSFX;
    [SerializeField] AudioClip biscuitSFX;
    [SerializeField] AudioClip goalDoorOpenSFX;
    [SerializeField] AudioClip startDoorCloseSFX;
    [SerializeField] AudioClip teleportSFX;

    void Awake()
    {
        if (instance == null) instance = this;

        highestSFXChannel = sfxChannels.Count - 1;
    }

    void Update()
    {
        switch (SceneChanger.instance.GetLevelNumber())
        {
            case 0:
            case 1:
            case 2:
            case 3:
                PlayMusic(gameMusic1, 0.05f);
                break;
            case 4:
            case 5:
            case 6:
                PlayMusic(gameMusic2, 0.05f);
                break;
            case 7:
            case 8:
            case 9:
            case 10:
                PlayMusic(gameMusic3, 0.1f);
                break;
        }
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

    public void PlayClickSound(float volume = 0)
    {
        PlaySoundEffect(clickSFX, volume);
    }

    public void PlayButtonPushSound(float volume = 0)
    {
        PlaySoundEffect(buttonPushSFX, volume);
    }


    public void PlayButtonReleaseSound(float volume = 0)
    {
        PlaySoundEffect(buttonReleaseSFX, volume);
    }

    public void PlayBiscuitCollectSound(float volume = 0)
    {
        PlaySoundEffect(biscuitSFX, volume);
    }

    public void PlayDoorOpenSound(float volume = 0)
    {
        PlaySoundEffect(goalDoorOpenSFX, volume);
    }

    public void PlayDoorCloseSound(float volume = 0)
    {
        PlaySoundEffect(startDoorCloseSFX, volume);
    }

    public void PlayTeleportSound(float volume = 0)
    {
        PlaySoundEffect(teleportSFX, volume);
    }
    #endregion
}
