using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string startSceneName = "HyunWoo";
    public string loadSceneName = "GameLoad";

    public void ClickStart()
    {
        Debug.Log("로딩");
        SceneManager.LoadScene(startSceneName);



    }

    public void ClickLoad()
    {
        Debug.Log("로드");
        SceneManager.LoadScene(loadSceneName);
    }

    public void ClickExit()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}