using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRope : MonoBehaviour
{
    [SerializeField] private HW_Player player;
    [SerializeField] private Transform endPos;
    private float speed;
    private bool endRope = false;
    
    public bool EndRope
    {
        get { return endRope; }
    }
    
    private void Update()
    {
        
        if (player.IsRope == true && transform.position != endPos.position && endRope == false)
        {
            speed += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, endPos.position, speed/500f);
            //transform.position = Vector3.MoveTowards(transform.position, endPos.position, 5f * Time.deltaTime);
            

        }
     
    }
    

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.CompareTag("ENDROPE"))
        {
            Debug.Log("hit");
            endRope = true;
            
        }
    }
}
