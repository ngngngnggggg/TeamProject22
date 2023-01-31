using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_DropSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip dropSound;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            audioSource.PlayOneShot(dropSound);
        }
    }
}
