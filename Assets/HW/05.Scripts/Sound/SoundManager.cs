using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    static SoundManager instance;
    public AudioSource[] audioSource;
    
    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }
    
   
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;

            audioSource = GetComponentsInChildren<AudioSource>();
        }
        else
        {
            DestroyObject(this.gameObject);
        }
    }

    public void StopSound(string soundName)
    {
        for (int i = 0; i < audioSource.Length; i++)
        {
            if (audioSource[i].gameObject.name.CompareTo(soundName) == 0)
            {
                audioSource[i].Stop();
            }
        }
    }
    
    public void PlaySound(string soundName)
    {
        for (int i = 0; i < audioSource.Length; i++)
        {
            if (audioSource[i].gameObject.name.CompareTo(soundName) == 0)
            {
                audioSource[i].Play();
            }
        }
    }
    
    public void StopAllSound()
    {
        for (int i = 0; i < audioSource.Length; i++)
        {
            audioSource[i].Stop();
        }
    }

    public void SoundAllMute()
    {
        for (int i = 0; i < audioSource.Length; i++)
        {
            audioSource[i].mute = true;
        }
    }

    public void SoundAllOn()
    {
        for (int i = 0; i < audioSource.Length; i++)
        {
            audioSource[i].mute = false;
        }
    }

    public void BGSoundSpeedOn()
    {
        audioSource[0].pitch = 1.1f;
    }

    public void BGSoundSpeedOff()
    {
        audioSource[0].pitch = 1.0f;
    }
    
}
