using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_Dive : MonoBehaviour
{
    [SerializeField] private HW_Player _player;
    
   

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                Debug.Log("isDive");
                other.gameObject.GetComponent<Animator>().SetTrigger("isDive");
                //_player.anim.SetTrigger("isDive");
               
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space))
            {
                //_player.anim.SetTrigger("isDive");
                other.gameObject.GetComponent<Animator>().SetTrigger("isDive");
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.C))
            {
                //_player.anim.SetTrigger("isDive");
                other.gameObject.GetComponent<Animator>().SetTrigger("isDive");
            }
            //else
            // {
            //     _player.anim.SetTrigger("isDive");
            // }
            
        }
    }


   
    
}
