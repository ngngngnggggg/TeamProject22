using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Camera : MonoBehaviour
{
    Transform Player;

    void LateUpdate()
    {
        Vector3 direction = (Player.transform.position - transform.position).normalized;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, Mathf.Infinity,
                                               1 << LayerMask.NameToLayer("EnvironmentObject"));

        for (int i = 0; i < hits.Length; i++)
        {
            TransparentObject[] obj = hits[i].transform.GetComponentsInChildren<TransparentObject>();

            for (int j = 0; j < obj.Length; j++)
            {
                obj[j]?.BecomeTransparent();
            }
        }
    }
}