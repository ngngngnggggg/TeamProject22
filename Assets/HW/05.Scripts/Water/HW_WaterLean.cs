using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_WaterLean : MonoBehaviour
{
    [SerializeField] private HW_Player player;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }
}
