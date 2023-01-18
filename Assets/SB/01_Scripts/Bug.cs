using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    private Transform player;
    private float dist; // 거리 변수
    public float moveSpeed; // 적의 이동 속도
    public float howclose; // 적이 플레이어를 공격하기까지 얼마나 가까운지 알려주는 변수
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(player.position, transform.position); 
        if(dist <= howclose)
        {
            transform.LookAt(player);
            GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed);
        }

        // 근접 공격 또는 폭발물
        if(dist <= 1.5f)
        {
            // 손상 또는 폭발
        }
    }
}
