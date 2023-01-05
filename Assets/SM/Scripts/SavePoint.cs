using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] private GameManager gameMng;

    [SerializeField] private Renderer renderer;
    [SerializeField] private Material mat;

    //private void OnCollisionEnter(Collision coll)
    //{
    //    if (coll.gameObject.tag == "Player")
    //    {
    //        gameMng.GameSave();
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("??");
            gameMng.GameSave();
           // Renderer renderer = other.GetComponent<Renderer>();
            renderer.material = mat;

        }
    }
}
