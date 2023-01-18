using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Door : MonoBehaviour
{
    [SerializeField] private GameObject door;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.transform.rotation = Quaternion.Lerp(door.transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime);

        }
    }
}
