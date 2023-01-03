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
    [SerializeField] private bool isAction;
    
    [Header("벽을 감지하는 레이 거리")] [SerializeField] private float range = 1f;

    //[Header("벽에 힘을 주는 변수")] [SerializeField] private float sticktowallforce = 10f;
    [Header("벽타는 속도")] [SerializeField] private float climbspeed = 0.5f;

    [Header("벽을 타는지 확인")] [SerializeField] private bool isclimbing = false;
    [Header("외줄 타는지 확인")] [SerializeField] private bool isSideStep = false;
    
   

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
        if (Move()) Run();
        Grab();
        Jump();
        Climb();

        SideStep();
        

        // switch (animState)
        // {
        //     case EAnim.Walk:
        //         if ( Move()) Run();
        //         break;
        //     case EAnim.Grab:
        //         Grab();
        //         break;
        // }
    }

    //플레이어 이동 함수
    // ReSharper disable Unity.PerformanceAnalysis
    private bool Move()
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
        
        speed = Input.GetKey(KeyCode.LeftShift) ? 3f : 1.5f;
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.C) && !isAction)
        {
            anim.SetTrigger("isSlide");
            //애니메이션 동작 하는 동안에는 콜라이더를 Z축으로 0.5만큼 줄여서 슬라이딩 효과를 주는 코루틴 함수
            StartCoroutine(Slide());
        }

        if (!isAction)
        {
            //플레이어 이동   
            transform.Translate(moveDir.normalized * (speed * Time.deltaTime), Space.World);
        }



        ChangeAnim(anim, moveDir, speed, canJump, hit);

        return h != 0;
    }
    
    //코루틴 슬라이드 함수
    IEnumerator Slide()
    {
        isAction = true;
        //cc.height = 0.5f;
        cc.direction = 2;
        yield return new WaitForSeconds(0.828f);
        //cc.height = 1.929797f;
        cc.direction = 1;
        isAction = false;
    }
    

    //플레이어 달리기 함수
    private void Run()
    {
        //LShift키를 누르면 3.0의 속도로 달리고 떼면 1.5의 속도로 걷기
        //speed = Input.GetKey(KeyCode.LeftShift) ? 3.0f : 1.5f;
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


    // ReSharper disable Unity.PerformanceAnalysis
    private void Climb()
    {

        if (isclimbing == false && Input.GetKeyDown(KeyCode.C))
        {
            //Debug.Log("wallbool확인");
            if (Physics.Raycast(transform.position + (Vector3.up *0.7f), transform.forward, out hit, range))
            {
                if (hit.transform.tag == "Wall")
                {
                    isclimbing = true;
                }
            }
        }

        if (isclimbing)
        {
            if (Physics.Raycast(transform.position + (Vector3.up *0.7f), transform.forward, out hit, range))
            {
                if (hit.transform.tag == "Wall")
                {

                    transform.position += transform.up * Time.deltaTime * climbspeed;
                    //콜라이더와 중력을 비활성화 한다.
                    //cc.enabled = false;
                    rigid.useGravity = false;
                    //애니메이션 실행
                    anim.SetBool("isClimbing", true);


                }
               
            }
            else
            {
                isclimbing = false;
                
                
                StartCoroutine(ClimbCoroutine());
                
            }
        }
        
    }

    private IEnumerator ClimbCoroutine()
    {
        
        anim.SetBool("isClimbing", false);
        

        yield return new WaitForSeconds(0.5f);
        
        float t = 0f;
        Vector3 startpos = transform.position;
        Vector3 endpos = startpos + (Vector3.up * 0.1f)+(Vector3.right * 0.02f);
        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startpos, endpos, t);
            yield return null;
        }
        
        t = 0f;
        startpos = transform.position;
        endpos = startpos + (Vector3.up * 0.2f)+(Vector3.right * 0.02f);
        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startpos, endpos, t);
            yield return null;
        }
        
        t = 0f;
        startpos = transform.position;
        endpos = startpos + (Vector3.up * 0.3f)+(Vector3.right * 0.42f);
        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startpos, endpos, t);
            yield return null;
        }
        
        t = 0f;
        startpos = transform.position;
        endpos = startpos + (Vector3.up * 0.3f);
        while (t < 0.2f)
        {
            t += Time.deltaTime ;
            transform.position = Vector3.Lerp(startpos, endpos, t);
            yield return null;
        }
     
        cc.enabled = true;
        rigid.useGravity = true;
    }

    private void SideStep()
    {
        
        
        if (isSideStep == false && Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("bool확인");
            if (Physics.Raycast(transform.position, -transform.up, out hit, range))
            {
                Debug.Log("Ray확인");
                if (hit.transform.tag == "Bridge")
                {
                    Debug.Log("tag확인");
                    isSideStep = true;
                    isAction = true;
                }
            }
        }

        if (isSideStep)
        {
            if (Physics.Raycast(transform.position, -transform.up, out hit, range))
            {
                if (hit.transform.tag == "Bridge" && transform.rotation.y > 0)
                {
                    float sidespeed = 1f;
                    float t = 0f;
                    
                    
                    Quaternion startrot = transform.rotation;
                    Quaternion endrot = Quaternion.Euler(0f, 0f, 0f);
                    
                    //오브젝트가 x방향으로 이동한다
                    Vector3 startpos = transform.position;
                    Vector3 endpos = startpos + (Vector3.right * 0.5f);
                    anim.SetBool("isSideStep", true);
                    while (t < 1f)
                    {
                        
                        t += Time.deltaTime * sidespeed;
                        transform.position = Vector3.Lerp(startpos, endpos, t);
                        transform.rotation = Quaternion.Lerp(startrot, endrot, t);
                        return;
                    }
                }
                else if (hit.transform.tag == "Bridge" && transform.rotation.y < 0)
                {
                    float sidespeed = 1f;
                    float t = 0f;
                    Quaternion startrot = transform.rotation;
                    Quaternion endrot = Quaternion.Euler(0f, 180f, 0f);
                    
                    //오브젝트가 x방향으로 이동한다
                    Vector3 startpos = transform.position;
                    Vector3 endpos = startpos + (Vector3.left * 0.5f);
                    anim.SetBool("isSideStep", true);
                    while (t < 1f)
                    {
                        
                        t += Time.deltaTime * sidespeed;
                        transform.position = Vector3.Lerp(startpos, endpos, t);
                        transform.rotation = Quaternion.Lerp(startrot, endrot, t);
                        return;
                    }
                }
                else if(hit.transform.tag == "Ground" || hit.transform.tag != "Bridge")
                {
                    isSideStep = false;
                    isAction = false;
                    anim.SetBool("isSideStep", false);
                 
                    //StartCoroutine(SideStepCoroutine());
                }
            }
            
        }
    }
    
    
    
    



    private void ChangeAnim(Animator anim, Vector3 _moveDir, float _speed, bool _canJump, RaycastHit _hit)
    {
        // switch (animState)
        // {
        //     case EAnim.Walk:
        //         anim.SetBool("isWalk", _moveDir != Vector3.zero);
        //         break;
        // }

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