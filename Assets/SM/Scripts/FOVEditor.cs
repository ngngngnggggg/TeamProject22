using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;// Editor Ŭ���� ��ӹޱ� ����
#endif

[CustomEditor(typeof(EnemyFOV))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        // target �ݹ� �Լ��μ� ���� ����
        EnemyFOV fov = (EnemyFOV)target;

        // �����ϱ� ���� ������
        Vector3 fromAnglePos = fov.CirclePoint(-fov.viewAngle * 0.5f);

        // ���� ���� ����
        Handles.color = Color.white;

        Handles.DrawWireDisc(fov.transform.position, Vector3.up, fov.viewRange);

        Handles.color = new Color(1, 1, 1, 0.2f);
        Handles.DrawSolidArc(fov.transform.position, Vector3.up, fromAnglePos, fov.viewAngle, fov.viewRange); // ��ä���� ������

        Handles.Label(fov.transform.position + (fov.transform.forward * 2f), fov.viewAngle.ToString()); // �� ����
    }


}