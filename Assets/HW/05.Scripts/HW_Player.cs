using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class HW_Player : MonoBehaviour
{
    //플레이어 이동 변수
    [SerializeField] private float speed = 1.5f;
    //플레이어 점프 변수
    [SerializeField] private float jumpPower = 3.0f;
    [SerializeField] private GameObject Stone;
    [SerializeField] private Transform Handpos;
    //플레이어 3d리지드바디
    private Rigidbody rigid;
    //플레이어 애니메이터
    private Animator anim;
    
    private CapsuleCollider cc;

    //점프를 했는지 확인하는 변수
    private bool canJump;
    private bool isGround;
    
    [Header("벽을 감지하는 레이 거리")] [SerializeField] private float range = 1f;

    [Header("벽에 힘을 주는 변수")] [SerializeField] private float sticktowallforce = 10f;
    [Header("벽타는 속도")] [SerializeField] private float climbspeed = 1f;
   

    //플레이어가 벽을 감지하게 하는 레이 히트
    private RaycastHit hit;
    
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider>();



    }

    private void Update()
    {
        Move();
        Grab();
        Run();
        Jump();
        Climb();

    }

    //플레이어 이동 함수
    // ReSharper disable Unity.PerformanceAnalysis
    private void Move()
    {
        //플레이어 이동
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        //누르는 방향으로 플레이어 회전
        if(moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
        }
        //플레이어 이동   
        transform.Translate(moveDir.normalized * (speed * Time.deltaTime), Space.World);
        
       
        
        ChangeAnim(anim, moveDir, speed, canJump, hit);
    }

    //플레이어 달리기 함수
    private void Run()
    {
        //LShift키를 누르면 3.0의 속도로 달리고 떼면 1.5의 속도로 걷기
        speed = Input.GetKey(KeyCode.LeftShift) ? 3.0f : 1.5f;
    }
    
    private void Jump()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, 0.3f);
        canJump = Input.GetKeyDown(KeyCode.Space) && isGround;
        if (canJump)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        
    }
    
    //플레이어 오브젝트 그랩 함수
    // ReSharper disable Unity.PerformanceAnalysis
    private void Grab()
    {
        //플레이어가 오브젝트을 감지하면
        if (Physics.Raycast(transform.position, transform.forward, out hit, range) && Input.GetKey(KeyCode.E)) 
        {
            //돌을 감지하면 돌을 그랩
            if (hit.collider.CompareTag("Stone"))
            {
                //돌을 그랩하면 돌을 플레이어 자식으로 만들고
                hit.collider.transform.SetParent(Handpos);
                //자식의 콜라이더를 비활성화
                hit.collider.GetComponent<Collider>().enabled = false;
                //자식의 리지드바디를 비활성화
                hit.collider.GetComponent<Rigidbody>().isKinematic = true;
                //벽을 플레이어의 자식으로 만들면 플레이어가 벽을 움직일 수 있게
                hit.collider.transform.localPosition = new Vector3(0, 0, 0);
                //isPick 애니메이션을 실행
                anim.SetTrigger("isPick");
                //isPick 애니메이션을 실행할 때 다른 애니메이션 실행을 막기 위해
                anim.SetBool("isWalk", false);
                anim.SetBool("isRun", false);
                anim.SetBool("isJump", false);
                anim.SetBool("isIdle", false);
                
                
            }
        }
        

        if (Input.GetKeyDown(KeyCode.Q) )
        {
            Debug.Log("1242314123123412344123412341234123123");
            //돌을 던지면 돌의 부모를 비활성화
            Stone.transform.SetParent(null);
            //돌의 콜라이더를 활성화
            Stone.GetComponent<Collider>().enabled = true;
            //돌의 리지드바디를 활성화
            Stone.GetComponent<Rigidbody>().isKinematic = false;
            //돌의 리지드바디에 힘을 가함
            Stone.GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.Impulse);
            //Throw 애니메이션 실행
            anim.SetTrigger("isThrow");
            
        }
    }


    private void Climb()
    {
        RaycastHit hit;
        if (Input.GetKey(KeyCode.C))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, range))
            {
                if (hit.transform.tag == "Wall")
                {
                    transform.position += transform.forward * (Time.deltaTime * sticktowallforce);
                    transform.position += transform.up * (Time.deltaTime * climbspeed);
                    rigid.useGravity = false;
                    
                }
                else 
                {
                        rigid.useGravity = true;

                }
            }
        }
    }
    



    private void ChangeAnim(Animator anim, Vector3 _moveDir, float _speed, bool _canJump, RaycastHit _hit)
    {
        
        //플레이어의 속도가 0보다 클 때 Walking 애니메이션 실행
        //플레이어의 속도가 0일 때 Idle 애니메이션 실행
        anim.SetBool("isWalk", _moveDir != Vector3.zero);

        //플레이어의 속도가 0보다 크고, 왼쪽 쉬프트를 눌렀을 때 isRun 애니메이션 실행
        anim.SetBool("isRun", Input.GetKey(KeyCode.LeftShift) && _moveDir != Vector3.zero);

        //Player Jump animation start
        anim.SetBool("StandingJump", _canJump);
        
        //player Run Jump animation start
        anim.SetBool("RunningJump", _canJump);
        Debug.DrawRay(transform.position + Vector3.up, transform.forward * range, Color.red);
        
            if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out _hit, range))
            {
                Debug.Log(_hit.transform.tag);
                if (_hit.transform.tag == "Wall"&& Input.GetKeyDown(KeyCode.G))
                {
                    if (!anim.GetBool("isClimbing"))
                    {
                        anim.SetBool("isClimbing",true);
                        Debug.Log("1123");
                    }
                 
                }
                
            } else
            {
                if (anim.GetBool("isClimbing"))
                {
                    anim.SetBool("isFinish", true);
                }
            }
    }

    //현재 실행 할 애니메이션을 제외한 나머지 애니메이션 중지 함수
    private void StopAnim(Animator anim, string _animName)
    {
        //현재 실행 할 애니메이션을 제외한 나머지 애니메이션 중지
        foreach (var _anim in anim.runtimeAnimatorController.animationClips)
        {
            if (_anim.name == _animName) continue;
            anim.ResetTrigger(_anim.name);
        }
    }

}