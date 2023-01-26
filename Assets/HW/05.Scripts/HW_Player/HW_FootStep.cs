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

       //볼륨은 0.46f로 고정
         audioSource.volume = 0.46f;
       if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo, 1f))
       {
           if(hitInfo.transform.CompareTag(("Ground")))
           {
               audioSource.PlayOneShot(audioClip[12]);
           }
           else if (hitInfo.transform.CompareTag("Concrete"))
           {
               audioSource.PlayOneShot(audioClip[5]);
           }
           else if (hitInfo.transform.CompareTag("Untagged"))
           {
               audioSource.PlayOneShot(audioClip[0]);
           }
       }

       AudioClip clip = GetRandomClip();
       audioSource.PlayOneShot(clip = audioClip[0]);

   }

   private void RunStep()
   {

       audioSource.volume = 0.46f;
       if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo, 1f))
       {
           if(hitInfo.transform.CompareTag(("Ground")))
           {
               audioSource.PlayOneShot(audioClip[12]);
           }
           else if (hitInfo.transform.CompareTag("Concrete"))
           {
               audioSource.PlayOneShot(audioClip[5]);
           }
           else if (hitInfo.transform.CompareTag("Untagged"))
           {
               audioSource.PlayOneShot(audioClip[1]);
           }
       }

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
