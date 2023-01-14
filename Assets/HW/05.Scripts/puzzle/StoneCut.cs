using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCut : MonoBehaviour
{
    [SerializeField] private GameObject stone;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Destroy(gameObject);
            // //삭제된 위치에 프리펩 생성
            // Instantiate(stone, transform.position, Quaternion.identity);
        }
    }

    public void DestroyStone()
    {
        Destroy(gameObject);
        //삭제된 위치에 프리펩 생성
        GameObject _stone = Instantiate(stone, transform.position, Quaternion.identity);
        Destroy(_stone, 3f);
    }
    
}
