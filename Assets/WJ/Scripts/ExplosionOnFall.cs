using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionOnFall : MonoBehaviour
{
    public GameObject explosionPrefab; //폭발할 파티클 추가
    private bool hasExploded = false;

    private void Update()
    {
        if (!hasExploded && transform.position.y <= 2.037f)
        {
            hasExploded = true;
            Vector3 explosionPos = transform.position;
            explosionPos.y -= 2.0f; // 생성될 위치 수정
            Instantiate(explosionPrefab, explosionPos, Quaternion.identity);
            
            Destroy(this.gameObject,0.3f);
        }
    }
}