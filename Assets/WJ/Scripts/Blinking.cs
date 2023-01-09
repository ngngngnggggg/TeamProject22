using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{
    GameObject cv;

    // Start is called before the first frame update
    void Start()
    {
        cv = this.gameObject.name("Panel");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(blinking());
    }
    IEnumerator blinking()
    {
        int count = 0;
        while(count < 5)
        {
            cv.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            cv.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            count++;
        }
    }
}
