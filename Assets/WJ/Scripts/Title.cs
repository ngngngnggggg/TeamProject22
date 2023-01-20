using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string startSceneName = "SooBin";
   // public string loadSceneName = "GameLoad";

    public void ClickStart()
    {
        Debug.Log("�ε�");
        SceneManager.LoadScene(startSceneName);



    }  

    public void ClickLoad()
    {
        Debug.Log("�ε�");
        SceneManager.LoadScene(startSceneName);
    }

    public void ClickExit()
    {
        Debug.Log("���� ����");
        Application.Quit();
    }
}