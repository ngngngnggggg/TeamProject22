using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class CubeColor : MonoBehaviour
{
    private MeshRenderer mr;
    public Material mat;
    public Material originMat;
    public bool success = false;
    [SerializeField] private ResetColor resetcolor;
    [SerializeField] private Material answerMat;


    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        originMat = mr.material;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(("Player")))
        {
            success = true;
            StartCoroutine(transform.GetChild(0).GetComponent<HW_ParticleDrop>().ParticleDrop());
            this.GetComponent<BoxCollider>().enabled = false;
            // mat = transform.GetChild(0).GetComponent<HW_ParticleDrop>().mat;
            // mr.material = mat;
        }
    }
    
    public void ChangeColor(Material _mat)
    {
        mat = _mat;
        if (answerMat == mat)
        {
            resetcolor.count++;
        }
        else
        {
            resetcolor.count--;
        }
            mr.material = mat;
    }
    
    public void ResetColor()
    {
        mr.material = originMat;
        mat = originMat;
        success = false;
        this.GetComponent<BoxCollider>().enabled = true;
        resetcolor.count = 0;
    }
}
