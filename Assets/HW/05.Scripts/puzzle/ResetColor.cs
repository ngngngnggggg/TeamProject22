using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetColor : MonoBehaviour
{
    [SerializeField] private GameObject[] cubes;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach(GameObject cube in cubes)
                cube.GetComponent<CubeColor>().ResetColor();
        }
    }
}
