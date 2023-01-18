using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_Water : MonoBehaviour
{
    public static bool isWater = false;
    
    [SerializeField] private float waterDrag; //물속 중력
    [SerializeField] private float originDrag; //물에서 나왔을 때 원래의 중력

    [SerializeField] private Color waterColor; //물 속의 색상
    [SerializeField] private float waterFogDensity; //물 속의 탁함 정도
    
    [SerializeField] private Color originColor; //물에서 나왔을 때 원래의 색상
    [SerializeField] private float originFogDensity; //물에서 나왔을 때 원래의 물밀도

    private Animator anim;

    private void Start()
    {
        originColor = RenderSettings.fogColor;
        originFogDensity = RenderSettings.fogDensity;
        anim = GetComponent<Animator>();

        originDrag = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            GetWater(other);
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            swimming(other);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            GetOutWater(other);
        }
    }
    
    private void GetWater(Collider _player)
    {
        isWater = true;
        _player.transform.GetComponent<Rigidbody>().drag = waterDrag;
        StartCoroutine(WaterChangeColor());
    }
    
    private void swimming(Collider _player)
    {
        if (isWater)
        {
            
        }
    }
    
    //1초 후 색상 변경 코루틴 함수
private IEnumerator WaterChangeColor()
    {
        yield return new WaitForSeconds(1f);
        RenderSettings.fogColor = waterColor;
        RenderSettings.fogDensity = waterFogDensity;
    }
    
    
    
    private void GetOutWater(Collider _player)
    {
        if (isWater)
        {
            isWater = false;
            _player.transform.GetComponent<Rigidbody>().drag = originDrag;
            
            RenderSettings.fogColor = originColor;
            RenderSettings.fogDensity = originFogDensity;
        }
    }
}
