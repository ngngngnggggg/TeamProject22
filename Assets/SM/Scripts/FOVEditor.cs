using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;// Editor 클래스 상속받기 위해
#endif

[CustomEditor(typeof(EnemyFOV))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        // target 콜백 함수로서 전달 받음
        EnemyFOV fov = (EnemyFOV)target;

        // 설정하기 위한 시작점
        Vector3 fromAnglePos = fov.CirclePoint(-fov.viewAngle * 0.5f);

        // 원의 색상 지정
        Handles.color = Color.white;

        Handles.DrawWireDisc(fov.transform.position, Vector3.up, fov.viewRange);

        Handles.color = new Color(1, 1, 1, 0.2f);
        Handles.DrawSolidArc(fov.transform.position, Vector3.up, fromAnglePos, fov.viewAngle, fov.viewRange); // 부채꼴의 반지름

        Handles.Label(fov.transform.position + (fov.transform.forward * 2f), fov.viewAngle.ToString()); // 라벨 내용
    }


}