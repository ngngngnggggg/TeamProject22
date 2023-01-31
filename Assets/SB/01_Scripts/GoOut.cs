using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoOut : MonoBehaviour
{
    Animator animator;
    public bool IsOpen;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Open());
        }
    }

    IEnumerator Open()
    {
        yield return new WaitForSeconds(3f);
        animator.SetBool("IsOpen", true);
    }
}



// 1. �ڰ� �Ͼ�� ���� ������ �ۿ� ������ �� ������Ʈ�� ���� �׸��� ���ٰ� �ݶ��̴��� �ε����� ����ȯ
// �ڰ� �Ͼ�� ���� ����
