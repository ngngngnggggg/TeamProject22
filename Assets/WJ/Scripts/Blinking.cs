using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{
    public GameObject map;
    public GameObject dream;

    // Start is called before the first frame update
    void Start()
    {
        //Image image =GetComponent<Image>();
        //StartCoroutine(blinking());
    }

    // Update is called once per frame
    float time = 0;
    //GetComponent<Image>().color;

    // Update is called once per frame
    void Update()
    {
        if (time < 8f)
        {
            GetComponent<Image>().color = new Color(0, 0, 0, time / 5 );
        }
        else if(time < 9.0f)
        {
            map.SetActive(true);
            dream.SetActive(false);
            //GetComponent<Image>().color = new Color(0, 0, 0, 0);
            //map.SetActive(true);
        }
        else if(time < 9.2f)
        {
            GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
        else if(time < 10f)
        {
            GetComponent<Image>().color = new Color(0,0,0,255f);
        }
        time += Time.deltaTime;
    }




    //IEnumerator blinking()
    //{
    //    int count = 0;
    //    while (count < 20)
    //    {
    //        Debug.Log(count);

//        Image image = GetComponent<Image>();
//        image.color = new Color(10f, 100f, 10f, 100f);
//        yield return new WaitForSeconds(0.1f);
//        image.color = new Color(0, 0, 0, 0);
//        yield return new WaitForSeconds(0.1f);
//        count++;
//        if(count >= 20)
//        {
//            map.SetActive(true);

//        }
//    }
//}
    public void End()
    {
        //StartCoroutine(blinking());
    }
}
