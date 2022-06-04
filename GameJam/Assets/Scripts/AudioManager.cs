using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    void Awake() {

        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
/*
    void Start() {
        UpdateSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
        UpdateMusicVolume(PlayerPrefs.GetFloat("musicVolume"));
        Play("MainTheme");
    }

    public void UpdateSFXVolume(float value) {
        string[] soundList = new string[]{"ElevatorDing", "Click", "ElevatorOpen", "PageTurn", "Dialogue", "Steven"};
        UpdateVolume(soundList, value);
    }

    public void UpdateMusicVolume(float value) {
        Sound s = Array.Find(sounds, sound => sound.name == "MainTheme");
        if (s == null)
            Debug.Log("No such audio clip");
        s.source.volume = value;
    }

    private void UpdateVolume(string[] soundList, float value) {
        foreach (string x in soundList) {
            Sound s = Array.Find(sounds, sound => sound.name == x);
            if (s == null)
                Debug.Log("No such audio clip");
            else {
                s.source.volume = value;
            }
        }
        
    }
*/
    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            Debug.Log("No such audio clip");
        s.source.Play();        
    }
    
    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            Debug.Log("No such audio clip");
        s.source.Stop();        
    }

}
