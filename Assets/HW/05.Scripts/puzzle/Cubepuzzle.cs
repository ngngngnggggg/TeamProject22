using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubepuzzle : MonoBehaviour
{
    //[SerializeField] private GameObject ps;
    private BoxCollider boxcollider;
   
    
    private void Start()
    {
        boxcollider = GetComponent<BoxCollider>();
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
        }
        boxcollider.enabled = false;
        //인보크로 5초후 콜라이더 활성화
        Invoke("ColliderOn", 5f);
    }
    private void ColliderOn()
    {
        boxcollider.enabled = true;
    }
    
    



}
    
    
    

