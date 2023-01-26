using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WJ_Sound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] W_Player player;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    // W_Player 스크립트 스타트 함수에 PlayBGM() 함수를 넣어주면 시작할 때 브금 실행
    

    //게임을 시작하면 0번째 브금 실행
    public void PlayBGM()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }
    public void PlayBGM2()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    public void StopBGM()
    {
            audioSource.Stop();
    }
}
