using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeAction : MonoBehaviour
{
   [SerializeField] private Check_Rope canRope;
   [SerializeField] private Transform ropePos;
   private LineRenderer lr;
   public Transform StartPos;
   public Transform EndPos;
   private void Awake()
   {
      lr = GetComponent<LineRenderer>();
   }

   private void Update()
   {
      if (canRope.canRope == true&& Input.GetKeyDown(KeyCode.T)) 
      {
         
         lr.SetPosition(0, transform.position);
         lr.SetPosition(1, ropePos.position);
         //Vector3.Lerp(StartPos.transform.position, EndPos.transform.position, 10f);
      }
   }
}
