using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TestCamera : MonoBehaviour
{
    private void Update()
    {
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(0, 0, v);
        
        transform.Translate(dir * (Time.deltaTime * 5f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ddd"))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z), 0.1f);
        }
    }
}
