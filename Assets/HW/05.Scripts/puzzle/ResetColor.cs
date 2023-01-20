using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class ResetColor : MonoBehaviour
{
    [SerializeField] private GameObject[] cubes;
    [SerializeField] private StoneCut[] stone;
    [SerializeField] private Cubepuzzle puzzle;
     [SerializeField]private HW_ParticleExplosion explosion;
    public int count = 0;
    private ParticleSystem ps;

    private void Start()
    {
        //stones = GetComponentsInChildren<StoneCut>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (count == 4)
            {
                foreach (var t in stone)
                {
                    t.DestroyStone();
                    Invoke("EndPuzzle", 4f);
                }
                //카메라 전환 실패작
                //puzzle.GetComponent<Cubepuzzle>().OffCemara();
                //puzzle.GetComponent<Cubepuzzle>().OffAudioListener();
                explosion.GetComponent<HW_ParticleExplosion>().ParticleSpawn();
                return;
            }

            foreach(GameObject cube in cubes)
                cube.GetComponent<CubeColor>().ResetColor();
        }
    }

  

    private void EndPuzzle()
    {
        Destroy(GameObject.Find("Cubepuzzle"));
    }
}
