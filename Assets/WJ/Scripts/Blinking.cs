using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{
    public GameObject map;

    // Start is called before the first frame update
    void Start()
    {
        //Image image =GetComponent<Image>();
        StartCoroutine(blinking());
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(blinking());
    }
    IEnumerator blinking()
    {
        int count = 0;
        while (count < 20)
        {
            Debug.Log(count);
            
            Image image = GetComponent<Image>();
            image.color = new Color(0, 0, 0, 0);
            yield return new WaitForSeconds(0.1f);
            image.color = new Color(10f, 100f, 10f, 100f);
            yield return new WaitForSeconds(0.1f);
            count++;
            if(count >= 20)
            {
                map.SetActive(true);
                
            }
        }
    }
}
