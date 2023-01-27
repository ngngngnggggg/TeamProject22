using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainOst : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _mainOst;
    
    private void Start()
    {
        _audioSource.clip = _mainOst[0];
        _audioSource.Play();
    }

    private void Update()
    {
        if(!_audioSource.isPlaying)
        {
            _audioSource.clip = _mainOst[1];
        }
        else
        {
            _audioSource.clip = _mainOst[0];
        }
    }
}
