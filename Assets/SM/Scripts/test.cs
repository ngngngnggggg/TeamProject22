using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private GameManager gameMng;


    //private void OnCollisionEnter(Collision coll)
    //{
    //    if (coll.gameObject.tag == "Player")
    //    {
    //        gameMng.GameSave();
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")  )
        {
            Debug.Log("??");
            gameMng.GameSave();
        }
    }
}
