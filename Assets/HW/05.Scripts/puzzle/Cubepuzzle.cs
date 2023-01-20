using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubepuzzle : MonoBehaviour
{
    //[SerializeField] private GameObject ps;
    private BoxCollider boxcollider;
    //private Camera camera;
    //private AudioListener audioListener;
    //public float speed;
    //private Camera currentCamera;
    //public bool UseCameraRotation = true;
    
    [SerializeField] private GameObject cam;
    [SerializeField] private HW_Player player;
    
    private void Start()
    {
        boxcollider = GetComponent<BoxCollider>();
        //camera = cam.GetComponent<Camera>();
        //audioListener = cam.GetComponent<AudioListener>();
        //currentCamera = Camera.main;
        
        //시작하면 카메라 비 활성화
        //camera.enabled = false;
        //audioListener.enabled = false;
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //자식 오브젝트를 전부 활성화
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                Transform child = transform.GetChild(i);
                for(int j = 0; j < child.childCount; j++)
                {
                    child.GetChild(j).gameObject.SetActive(true);
                }
            }
            //카메라 전환 안됨
            //camera.enabled = true;
            //audioListener.enabled = true;
            
        }
        boxcollider.enabled = false;
        //인보크로 5초후 콜라이더 활성화
        Invoke("ColliderOn", 5f);
        
    }

    
    //만약 플레이어가 퍼즐중이라면 이동 방식을 바꾼다
    
    private void ColliderOn()
    {
        boxcollider.enabled = true;
    }
    
    
  
    
    
    }

    
    
    




    
    
    

