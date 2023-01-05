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

    private float ropeTime;
    //플레이어 점프 변수
    [SerializeField] private float jumpPower = 3.0f;
    [SerializeField] private GameObject Stone;
    private bool endRope = false;

    [SerializeField] private Transform Handpos;

    //플레이어 3d리지드바디
    private Rigidbody rigid;

    //플레이어 애니메이터
    private Animator anim;

    private CapsuleCollider cc;

    //점프를 했는지 확인하는 변수
    [Header("점프를 하고 있는지 확인")] [SerializeField]
    private bool canJump;

    [Header("바닥에 붙어 있는지 확인")] [SerializeField]
    private bool isGround;

    [Header("슬라이딩을 하고 있는지 확인")] [SerializeField]
    private bool isSlide;

    [Header("벽을 감지하는 레이 거리")] [SerializeField]
    private float range = 0.3f;

    //[Header("벽에 힘을 주는 변수")] [SerializeField] private float sticktowallforce = 10f;
    [Header("벽타는 속도")] [SerializeField] private float climbspeed = 0.5f;

    [Header("벽을 타는지 확인")] [SerializeField] private bool isclimbing = false;
    [SerializeField] private bool isclimbingUp = false;
    [Header("외줄 타는지 확인")] [SerializeField] private bool isSideStep = false;

    [Header("로프에 매달렸는지 확인")] [SerializeField]
    private bool isRope = false;
    public bool IsRope
    {
        get { return isRope; }
    }

    [SerializeField] private Transform startPos;
    [SerializeField] private MoveRope rope;
    private LineRenderer lr;


    //플레이어가 벽을 감지하게 하는 레이 히트
    private RaycastHit hit;
                                    
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider>();
        lr = GetComponent<LineRenderer>();


    }

    private void Update()
    {
        if (Move()) Run();
        Grab();
        Jump();
        Rope();

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
        if (isclimbing) return false;

        //플레이어 이동
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        //누르는 방향으로 플레이어 회전
        if (moveDir != Vector3.zero && !isclimbing)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
        }
        else
            isSlide = false;

        speed = Input.GetKey(KeyCode.LeftShift) ? 3f : 1.5f;
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.C) && !isSlide)
        {
            anim.SetTrigger("isSlide");
            //애니메이션 동작 하는 동안에는 콜라이더를 Z축으로 0.5만큼 줄여서 슬라이딩 효과를 주는 코루틴 함수
            StartCoroutine(Slide());
        }

        //플레이어 이동   
        transform.Translate(moveDir.normalized * (speed * Time.deltaTime), Space.World);



        ChangeAnim(anim, moveDir, speed, canJump, hit);

        return h != 0;
    }

    //코루틴 슬라이드 함수
    IEnumerator Slide()
    {
        isSlide = true;
        //cc.height = 0.5f;
        cc.direction = 2;
        yield return new WaitForSeconds(0.828f);
        //cc.height = 1.929797f;
        cc.direction = 1;
        isSlide = false;
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


        if (Input.GetKeyDown(KeyCode.Q))
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

        if (!isclimbing && Input.GetKeyDown(KeyCode.C))
        {
            //Debug.Log("wallbool확인");
            if (Physics.Raycast(transform.position + (Vector3.up * 0.7f), transform.forward, out hit, range))
            {
                if (hit.transform.tag == "Wall")
                {
                    isclimbing = true;
                    isclimbingUp = true;
                }
            }
        }

        if (isclimbingUp)
        {
            if (Physics.Raycast(transform.position + (Vector3.up * 0.7f), transform.forward, out hit, range))
            {
                if (hit.transform.tag == "Wall")
                {
                    //Quaternion endrot = Quaternion.Euler(0f, 90f, 0f);

                    transform.position += transform.up * Time.deltaTime * climbspeed;
                    //transform.rotation = Quaternion.Lerp(transform.rotation, endrot, 1f);
                    //콜라이더와 중력을 비활성화 한다.
                    //cc.enabled = false;
                    rigid.useGravity = false;
                    //애니메이션 실행
                    anim.SetTrigger("isClimbing");
                }

            }
            else
            {
                isclimbingUp = false;
                // Debug.Log("123");
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
        Vector3 endpos = startpos + (Vector3.up * 0.1f) + (Vector3.right * 0.02f);
        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startpos, endpos, t);
            yield return null;
        }

        t = 0f;
        startpos = transform.position;
        endpos = startpos + (Vector3.up * 0.2f) + (Vector3.right * 0.02f);
        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startpos, endpos, t);
            yield return null;
        }

        t = 0f;
        startpos = transform.position;
        endpos = startpos + (Vector3.up * 0.3f) + (Vector3.right * 0.42f);
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
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startpos, endpos, t);
            yield return null;
        }

        cc.enabled = true;
        rigid.useGravity = true;

        isclimbing = false;
    }

    private void SideStep()
    {


        if (!isSideStep && Input.GetKeyDown(KeyCode.C))
        {

            if (Physics.Raycast(transform.position, -transform.up, out hit, range))
            {

                if (hit.transform.tag == "Bridge")
                {

                    isSideStep = true;
                    isSlide = true;
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
                else if (hit.transform.tag == "Ground" || hit.transform.tag != "Bridge")
                {
                    isSideStep = false;
                    isSlide = false;
                    anim.SetBool("isSideStep", false);

                    //StartCoroutine(SideStepCoroutine());
                }
            }

        }
    }



    //로프에 매달리기
    private void Rope()
    {
        //점프를 하고 있고 제자리 점프 애니메이션이 실행 중일때
        if (isGround == false)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("C키 입력");
                if (Physics.Raycast(transform.position, transform.up, out hit, range + 1f))
                {
                    Debug.Log("Ray확인");
                    if (hit.transform.gameObject.CompareTag("Rope"))
                    {
                        Debug.Log("tag가 로프");
                        isRope = true;
                        //애니메이션 실행
                        anim.SetTrigger("isRope");
                        transform.SetParent(startPos);
                    }
                }
            }
        }

        if (isRope == true && rope.EndRope == false)
        {
            
            lr.SetPosition(0, transform.position + new Vector3(0f,0.7f, 0f));
            lr.SetPosition(1, startPos.position + new Vector3(0f, 0.3f,0f));
          
                Debug.Log("hit rope");
                anim.SetTrigger("isRope");
                rigid.useGravity = false;
                rigid.isKinematic = true; //isRope가 아니면 해제 해줘야함
         

        
        }
        else if(rope.EndRope == true)
        {
            
            Debug.Log("endrope");
            //isRope = false;
            transform.SetParent((null));
            Destroy(lr);
            rigid.useGravity = true;
            rigid.isKinematic = false;
            //StartCoroutine(RopeCoroutine());  
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
            Debug.DrawRay(transform.position, transform.forward, Color.red);

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
