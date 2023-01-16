using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoOut : MonoBehaviour
{
    public bool WakeUp = false;
    private Door door;

    // Start is called before the first frame update
    void Start()
    {
        door = GameObject.FindWithTag("Door").GetComponent<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        if(WakeUp == true)
        {
            door.Open();
        }
    }
}

// 1. 자고 일어나면 문이 열린다 밖에 나가면 숲 오브젝트가 켜짐 그리고 가다가 콜라이더에 부딪히면 씬전환
// 자고 일어나는 상태 관리
