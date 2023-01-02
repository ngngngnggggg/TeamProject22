using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    private Transform player;
    private float dist; // �Ÿ� ����
    public float moveSpeed; // ���� �̵� �ӵ�
    public float howclose; // ���� �÷��̾ �����ϱ���� �󸶳� ������� �˷��ִ� ����
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

        // ���� ���� �Ǵ� ���߹�
        if(dist <= 1.5f)
        {
            // �ջ� �Ǵ� ����
        }
    }
}
