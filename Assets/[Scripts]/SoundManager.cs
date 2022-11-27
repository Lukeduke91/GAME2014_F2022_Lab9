using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class SoundManager : MonoBehaviour
{

    public List<AudioSource> channels;
    private List<AudioClip> audioClips;

    // Start is called before the first frame update
    void Awake()
    {
        channels = GetComponents<AudioSource>().ToList();
        audioClips = new List<AudioClip>();
        InitializeSoundFX();
    }

    private void InitializeSoundFX()
    {
        audioClips.Add(Resources.Load<AudioClip>("Audio/Jump-Sound"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/Hurt-Sound"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/Death-Sound"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/main-Soundtrack"));
    }

    public void PlaySoundFX(SoundFX sound, Channel channel)
    {
        channels[(int)channel].clip = audioClips[(int)sound];
        channels[(int)channel].Play();
    }

    public void PlayMusic()
    {
        channels[(int)Channel.MUSIC].clip = audioClips[(int)SoundFX.MUSIC];
        channels[(int)Channel.MUSIC].volume = 0.5f;
        channels[(int)Channel.MUSIC].loop = true;
        channels[(int)Channel.MUSIC].Play();
    }
}
