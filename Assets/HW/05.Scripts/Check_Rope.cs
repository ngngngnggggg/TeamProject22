using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Check_Rope : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]public bool canRope = false;
    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.CompareTag("Player"))
        {
            canRope = true;
        }
        else
        {
            canRope = false;
        }
    }
}
