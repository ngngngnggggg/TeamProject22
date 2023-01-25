using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(AudioSource))]

public class HW_FootStep : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClip;
    private AudioSource audioSource;

   private void Awake()
   {
         audioSource = GetComponent<AudioSource>();
   }

   
   //thats the animation event
   private void Step()
   {
       AudioClip clip = GetRandomClip();
       audioSource.PlayOneShot(clip = audioClip[0]);
   }

   private void RunStep()
   {
       AudioClip clip = GetRandomClip();
       audioSource.PlayOneShot(clip = audioClip[1]);
   }

   private void StandingJumpSound()
   {
       AudioClip clip = GetRandomClip();
       audioSource.PlayOneShot(clip = audioClip[2]);
   }
   
   private void RunJumpSound()
   {
       AudioClip clip = GetRandomClip();
       audioSource.PlayOneShot(clip = audioClip[3]);
   }

   private AudioClip GetRandomClip()
   {
       int index = Random.Range(0, audioClip.Length - 1);
       return audioClip[index];
   }
}
