using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetColor : MonoBehaviour
{
    [SerializeField] private GameObject[] cubes;
    [SerializeField] private StoneCut[] stone;
    public int count = 0;

    private void Start()
    {
        //stones = GetComponentsInChildren<StoneCut>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (count == 4)
            {
                foreach (var t in stone)
                {
                    t.DestroyStone();
                }   
                return;
            }
            
            foreach(GameObject cube in cubes)
                cube.GetComponent<CubeColor>().ResetColor();
        }
    }
}
