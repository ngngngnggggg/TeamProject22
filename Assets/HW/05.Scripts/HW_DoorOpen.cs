using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_DoorOpen : MonoBehaviour
{
    [SerializeField] private GameObject door;
    
    
    // 태그가 Player이면 문이 -90도로 부드럽게 회전한다.
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.transform.rotation = Quaternion.Lerp(door.transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime);
            
        }
    }



}
