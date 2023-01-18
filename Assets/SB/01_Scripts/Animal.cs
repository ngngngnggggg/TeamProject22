using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Animal : MonoBehaviour
{
    public Transform target;
    public float MoveSpeed;

    NavMeshAgent nav;
    Rigidbody rb;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        nav.SetDestination(target.position);
    }

    void FreezeVelocity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void FixxedUpdate()
    {
        FreezeVelocity();
    }


}
