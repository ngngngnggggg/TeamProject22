using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Item : MonoBehaviour
{
    public enum Type { Key, Weapon };
    public Type type;
    public int value;


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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            rigid.isKinematic = true;
            sphereCollider.enabled = false;
        }
    }
}

