using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_Water : MonoBehaviour
{
    
    [SerializeField] private HW_Player player;
    
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
        

        originDrag = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            GetWater(other);
            other.GetComponent<Animator>().SetTrigger("isDive");
            other.GetComponent<HW_Player>().isWater = true;
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
           
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
       
        _player.transform.GetComponent<Rigidbody>().drag = waterDrag;
        StartCoroutine(WaterChangeColor(_player.gameObject));
    }
    
    
    //1초 후 색상 변경 코루틴 함수
private IEnumerator WaterChangeColor(GameObject _player)
    {
        yield return new WaitForSeconds(1f);
        RenderSettings.fogColor = waterColor;
        RenderSettings.fogDensity = waterFogDensity;
        player.isDive = false;
        yield return new WaitForSeconds(0.5f);
        _player.GetComponent<Animator>().SetTrigger("isDive");
        _player.GetComponent<Rigidbody>().useGravity = false;
    }
    
    
    
    private void GetOutWater(Collider _player)
    {
        
            _player.transform.GetComponent<Rigidbody>().drag = originDrag;
            
            RenderSettings.fogColor = originColor;
            RenderSettings.fogDensity = originFogDensity;
        
    }
}
