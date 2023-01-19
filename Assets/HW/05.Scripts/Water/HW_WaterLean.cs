using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_WaterLean : MonoBehaviour
{
    [SerializeField] private HW_Player player;
    [SerializeField] private HW_Dive dive;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered water");
            other.GetComponent<HW_Player>().StartCoroutine((other.GetComponent<HW_Player>().WaterLean()));
            gameObject.SetActive(false);
            Invoke("EnableWater", 10f);
        }
    }
    
    private void EnableWater()
    {
        gameObject.SetActive(true);
        dive.gameObject.SetActive(true);
    }
    
    
}
