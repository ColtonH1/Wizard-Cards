using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixerGroup audioMixerGroup;
    public AudioMixer audioMixer;

    private float[] originalVolumes;
    public static AudioManager instance;

    public SettingsSO settings;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = audioMixerGroup;
            
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        originalVolumes = new float[sounds.Length];
    }

    private void Start()
    {
        audioMixer.SetFloat("volume", Mathf.Log10(settings.soundSetting) * 20);
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void MuteAudio()
    {
        foreach (Sound s in sounds)
        {
            for(int i = 0; i < sounds.Length; i++)
            {
                originalVolumes[i] = s.volume;
            }

            s.source.volume = 0f;
        }
    }

    public void UnmuteAudio()
    {
        foreach (Sound s in sounds)
        {
            for(int i = 0; i < sounds.Length; i++)
            {
                s.source.volume = originalVolumes[i];
            }
        }
    }
}
