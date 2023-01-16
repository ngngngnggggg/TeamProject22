using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoOut : MonoBehaviour
{
    public bool WakeUp = false;
    private Door door;

    // Start is called before the first frame update
    void Start()
    {
        door = GameObject.FindWithTag("Door").GetComponent<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        if(WakeUp == true)
        {
            door.Open();
        }
    }
}

// 1. �ڰ� �Ͼ�� ���� ������ �ۿ� ������ �� ������Ʈ�� ���� �׸��� ���ٰ� �ݶ��̴��� �ε����� ����ȯ
// �ڰ� �Ͼ�� ���� ����
