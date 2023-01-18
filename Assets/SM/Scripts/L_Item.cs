using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Item : MonoBehaviour
{



    Rigidbody rigid;
    SphereCollider sphereCollider;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        transform.Rotate(Vector3.up * 10 * Time.deltaTime);
    }
}

