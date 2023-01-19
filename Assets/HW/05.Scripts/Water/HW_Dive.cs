using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_Dive : MonoBehaviour
{
    [SerializeField] private HW_Player _player;
    private BoxCollider boxCollider;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                other.gameObject.GetComponent<Animator>().SetTrigger("isDive");
            }
            boxCollider.enabled = false;
            Invoke("OnCollider",2f);
        }
    }

    
    private void OffCollider()
    {
        boxCollider.enabled = false;
    }

    private void OnCollider()
    {
        boxCollider.enabled = true;
    }
}
