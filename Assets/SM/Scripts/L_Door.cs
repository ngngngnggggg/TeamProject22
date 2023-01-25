using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Door : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private L_Player Player;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Player.hasKey)
        {
            door.transform.rotation = Quaternion.Lerp(door.transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime);

        }
    }



}
