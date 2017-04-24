using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    AudioClip[] sounds;
    AudioClip[] musics;

    AudioSource[] sources;

    public enum SFX
    {
        Climb,
        Door,
        Float,
        Jump,
        JumpDouble,
        Land,
        PressurePlate,

        JumpSpace,
        LandSpace,

        EnumSize
    }

    public enum Music
    {
        Track1,

        Track1NES,

        EnumSize
    }

    private void Awake()
    {
        int sourcesIndex = 0;
        sources = new AudioSource[(int)SFX.EnumSize + (int)Music.EnumSize];
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i] = gameObject.AddComponent<AudioSource>();
            sources[i].playOnAwake = false;
        }

        sounds = new AudioClip[(int)SFX.EnumSize];

        for (int i = 0; i < (int)SFX.EnumSize; i++)
        {
            SFX sfx = (SFX)i;
            sounds[i] = (AudioClip)Resources.Load("sound/" + sfx.ToString());
            sources[sourcesIndex].clip = sounds[i];
            ++sourcesIndex;
        }

        musics = new AudioClip[(int)Music.EnumSize];

        for (int i = 0; i < (int)Music.EnumSize; i++)
        {
            Music mus = (Music)i;
            musics[i] = (AudioClip)Resources.Load("music/" + mus.ToString());
            sources[sourcesIndex].clip = musics[i];
            sources[sourcesIndex].loop = true;
            sources[sourcesIndex].volume = 0;
            sources[sourcesIndex].Play();
            ++sourcesIndex;
        }

    }

    public void PlaySoundIfNotPlaying(SFX sound)
    {
        if (!sources[(int)sound].isPlaying)
            sources[(int)sound].Play();
    }

    public void PlaySound(SFX sound)
    {
        sources[(int)sound].Play();
    }

    public void StopSound(SFX sound)
    {
        if(sources[(int)sound].isPlaying)
            sources[(int)sound].Stop();
    }

    public void PlayMusic(Music music)
    {
        StopAllMusic();
        sources[(int)SFX.EnumSize + (int)music].volume = 1;
    }

    public void StopAllMusic()
    {
        for (int i = (int)SFX.EnumSize; i < sources.Length; i++)
        {
            sources[i].volume = 0;
        }
    }

    //public void SwitchMusic(Music mus1, Music mus2)
    //{
    //    AudioSource s1 = sources[(int)SFX.EnumSize + (int)mus1];
    //    AudioSource s2 = sources[(int)SFX.EnumSize + (int)mus2];

    //    s1.volume = 0;
    //    s2.volume = 1;
    //}

    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (AudioManager)FindObjectOfType(typeof(AudioManager));
            }
            return _instance;
        }
    }
}
