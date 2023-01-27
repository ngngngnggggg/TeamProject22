using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSound : MonoBehaviour
{
    [SerializeField] private AudioSource birdSound;
    [SerializeField] private AudioClip birdClip;


    private void Start()
    {
        birdSound = GetComponent<AudioSource>();
    }
    
    
}
