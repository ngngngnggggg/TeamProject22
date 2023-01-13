using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{
    public GameObject Ruin;
    public GameObject dream;

    private LoveHouse m_LoveHouse = null;
    private SoloHouse m_SoloHouse = null;

    private bool m_IsEnd = false;
    
    private void Awake()
    {
        Ruin.SetActive(true);
        dream.SetActive(true);
        m_LoveHouse = dream.GetComponent<LoveHouse>();
        m_SoloHouse = Ruin.GetComponent<SoloHouse>();
    }

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
            GetComponent<Image>().color = new Color(0, 0, 0, time / 5);
        }
        else if (time <9.0f)
        {
            //Ruin.SetActive(true);
            //dream.SetActive(false);
            //GetComponent<Image>().color = new Color(0, 0, 0, 0);
            //map.SetActive(true);
        }
        else if (time < 9.2f)
        {
            if (!m_IsEnd)
            {
                m_IsEnd = true;
                End();
            }

        }
        //else if (10f < time)
        //{
        //    GetComponent<Image>().color = new Color(0, 0, 0, 255f);
        //}
        time += Time.deltaTime;
    }
    //public void End()
    //{
    //    //time = 0f;
    //    if (time < 8f)
    //    {
    //        GetComponent<Image>().color = new Color(0, 0, 0, time / 5);
    //    }
    //    else if (9.0f < time)
    //    {
    //        map.SetActive(true);
    //        dream.SetActive(false);
    //        //GetComponent<Image>().color = new Color(0, 0, 0, 0);
    //        //map.SetActive(true);
    //    }
    //    else if (time < 9.2f)
    //    {
    //        GetComponent<Image>().color = new Color(0, 0, 0, 0);
    //    }
    //    else if (10f < time)
    //    {
    //        GetComponent<Image>().color = new Color(0, 0, 0, 255f);
    //    }
    //    time += Time.deltaTime;
    //}




    IEnumerator blinking()
    {
        Image image = GetComponent<Image>();

        int count = 0;
        while (count < 10)
        {
            //Debug.Log(count);

            image.color = Color.black;
            yield return new WaitForSeconds(0.05f);
            m_LoveHouse.SetRenderers(true);
            m_SoloHouse.SetRenderers(false);
            image.color = new Color(0, 0, 0, 0); // 보이기

            yield return new WaitForSeconds(0.05f);

            image.color = Color.black;
            yield return new WaitForSeconds(0.05f);
            m_LoveHouse.SetRenderers(false);
            m_SoloHouse.SetRenderers(true);
            image.color = new Color(0, 0, 0, 0); // 보이기
            yield return new WaitForSeconds(0.05f);

            count++;
            //if (count >= 20)
            //{
            //    map.SetActive(true);

            //}
        }
    }
    public void End()
    {
        StartCoroutine(blinking());
    }
}
