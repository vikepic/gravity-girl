using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    AudioClip[] sounds;
    [SerializeField]
    AudioClip[] musics;
    int currentTrack = 0;
    // 0 -> On suit
    // 1 -> Off suit
    // 2 -> End
    int currentState = 0;
    bool playingEndMusic = false;

    [SerializeField]
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

        SuitOn,
        SuitOff,

        JumpSpace,
        LandSpace,

        EnumSize
    }

    public enum Music
    {
        Track1Loop,
        Track1NESLoop,

        Track1End,

        Track2Loop,
        Track2NESLoop,

        Track2End,

        Track3Loop,
        Track3NESLoop,

        Track3End,

        EpicIntro,

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
            ++sourcesIndex;
        }

        ResetMusics();
    }



    void ResetMusics()
    {
        for (int i = (int)SFX.EnumSize; i < sources.Length; i++)
        {
            sources[i].Play();
            sources[i].volume = 0;
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

    public void SetState(int state)
    {
        currentState = state;

        // in order to protect us from switching musics if in ending mode
        if (!playingEndMusic)
        {
            if (currentState == 2)
            {
                ResetMusics();
            }
            PlayMusic();
        }

        // Special behaviour for ending music
        if(currentState == 2)
        {
            playingEndMusic = true;
            float len = sources[(int)SFX.EnumSize + currentTrack + currentState].clip.length;
            Invoke("NextTrackAfterEndMusic", len);
        }
    }

    void NextTrackAfterEndMusic()
    {
        playingEndMusic = false;
        NextTrack();
    }

    void NextTrack()
    {
        if (currentTrack < 5)
        {
            currentTrack += 3;
        }
        ResetMusics();
        PlayMusic();
    }

    public void PlayVictoryMusic()
    {
        currentState = 0;
        currentTrack = 9;
    }

    void PlayMusic()
    {
        StopAllMusic();
        sources[(int)SFX.EnumSize + currentTrack + currentState].volume = 1;
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
