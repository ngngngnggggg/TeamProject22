using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
         GameLoad();
    }
    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);
        PlayerPrefs.Save(); 

        Debug.Log("�����");
    }
    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
            return;

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        float z = PlayerPrefs.GetFloat("PlayerZ");

        player.transform.position = new Vector3(x, y, z);
    }

    public void ReStart()
    {
        PlayerPrefs.SetFloat("PlayerX", -36.11f);
        PlayerPrefs.SetFloat("PlayerY", 0.63f);
        PlayerPrefs.SetFloat("PlayerZ", 0.35f);

        SceneManager.LoadScene(3);
    }
}
     