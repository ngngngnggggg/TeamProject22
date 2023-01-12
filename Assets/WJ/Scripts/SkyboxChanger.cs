using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material newSkybox;
    private Material currentSkybox;
    public Blinking ui;

    private void Start()
    {
        ui = GetComponent<Blinking>();
        currentSkybox = RenderSettings.skybox;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        { 
        ui.End();
        RenderSettings.skybox = newSkybox;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RenderSettings.skybox = currentSkybox;
    }
}
