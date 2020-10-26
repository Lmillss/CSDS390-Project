﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniTaskStarter : MonoBehaviour
{
    [SerializeField] GameObject wireboxhead;
    [SerializeField] GameObject wireboxmid;
    [SerializeField] GameObject wireboxtail;
    [SerializeField] GameObject engine;
    [SerializeField] GameObject fuse;
    [SerializeField] GameObject lightHolder;
    [SerializeField] GameObject lightSwitch;
    [SerializeField] GameObject hull;
    [SerializeField] GameObject storage;
    [SerializeField] GameObject fireExtinguisher;

    [SerializeField] Light[] lights;
    [SerializeField] float distance = 5f;

    public GameObject EngineRoomSpotter;
    public GameObject StorageRoomSpotter;
    public GameObject WireHeadSpotter;
    public GameObject WireMidSpotter;
    public GameObject WireLowSpotter;
    public GameObject HullSpotter;
    public GameObject FuseSpotter;


    public GameObject storageHint;
    public GameObject FireEffect;
    private bool engineUpdate = false;
    public float timer = 0f;
    public static bool engineTrigger = false;


    // Start is called before the first frame update
    void Start()
    {
        lights = lightHolder.GetComponentsInChildren<Light>();
    }

  void Update()
    {
        if(GlobalData.engineBroken) {
            EngineRoomSpotter.SetActive(true);
             if(Input.GetKeyDown("e") && engine.GetComponent<MinigameTrigger>().getTriggerStatus() && GlobalData.engineBroken) {
                    SceneManager.LoadScene("Fix Engine");
             }
        } else {
            EngineRoomSpotter.SetActive(false);
        }

       if(GlobalData.wiresBroken) {
           //Set the WireBox;
            if (GlobalData.brokenWireboxLoc == 1) {
                WireHeadSpotter.SetActive(true);
                wireboxhead.GetComponent<WireBoxTrigger>().setBrokenStatus(true);
            }
            else if (GlobalData.brokenWireboxLoc == 2) {
                WireMidSpotter.SetActive(true);
                wireboxmid.GetComponent<WireBoxTrigger>().setBrokenStatus(true);
            }
            else if (GlobalData.brokenWireboxLoc == 3) {
                WireLowSpotter.SetActive(true);
                wireboxtail.GetComponent<WireBoxTrigger>().setBrokenStatus(true);
            }

            if (wireboxhead.GetComponent<WireBoxTrigger>().getTriggerStatus() && wireboxhead.GetComponent<WireBoxTrigger>().getBrokenStatus()) {
                SceneManager.LoadScene("Connect Wire");
            } else if(wireboxmid.GetComponent<WireBoxTrigger>().getTriggerStatus() && wireboxmid.GetComponent<WireBoxTrigger>().getBrokenStatus()) {
                SceneManager.LoadScene("Connect Wire");
            } else if(wireboxtail.GetComponent<WireBoxTrigger>().getTriggerStatus() && wireboxtail.GetComponent<WireBoxTrigger>().getBrokenStatus()) {
                SceneManager.LoadScene("Connect Wire");
            }
        } else {
            WireHeadSpotter.SetActive(false);
            wireboxhead.GetComponent<WireBoxTrigger>().setBrokenStatus(false);

            WireMidSpotter.SetActive(false);
            wireboxmid.GetComponent<WireBoxTrigger>().setBrokenStatus(false);

            WireLowSpotter.SetActive(false);
            wireboxtail.GetComponent<WireBoxTrigger>().setBrokenStatus(false);
        }

        if (GlobalData.storageLocked ) {
            StorageRoomSpotter.SetActive(true);
            if(Input.GetKeyDown("e") & storage.GetComponent<MinigameTrigger>().getTriggerStatus()) {
                SceneManager.LoadScene("Storage Room");
            }
        } else {
            StorageRoomSpotter.SetActive(false);
        }

        if(GlobalData.hullBroken) {
            HullSpotter.SetActive(true);
            if (Input.GetKeyDown("e") && hull.GetComponent<MinigameTrigger>().getTriggerStatus()) {
                SceneManager.LoadScene("Fix Hull");
            }
        } else {
            HullSpotter.SetActive(false);
        }

        if(GlobalData.fires) {
            FireEffect.SetActive(true);
            if (Input.GetKeyDown("e") & fireExtinguisher.GetComponent<MinigameTrigger>().getTriggerStatus()) {
                SceneManager.LoadScene("Fire Extinguish");
            }
        } else {
            FireEffect.SetActive(false);
        }

        if(GlobalData.fuseBroken) {
            FuseSpotter.SetActive(true);
            if (Input.GetKeyDown("e") & fuse.GetComponent<MinigameTrigger>().getTriggerStatus()) {
                SceneManager.LoadScene("Repair Fuse");
            }
        } else {
            FuseSpotter.SetActive(false);
        }

        if (Input.GetKeyDown("e") & lightSwitch.GetComponent<MinigameTrigger>().getTriggerStatus())
            {
                Debug.Log(GlobalData.lightSwitch);
                GlobalData.lightSwitch = !GlobalData.lightSwitch;
                Debug.Log("Lights Flipped");
                Debug.Log(GlobalData.lightSwitch);
            }

        if (GlobalData.lightsOn != lights[0].enabled)
        {
            GlobalData.lightsOn = !GlobalData.lightsOn;
        }
            if (GlobalData.lightsOn != (GlobalData.lightSwitch && !GlobalData.wiresBroken && !GlobalData.fuseBroken))
        {
                Debug.Log("In Here");
                GlobalData.lightsOn = !GlobalData.lightsOn;
                LightsFlip();
        }
        //Debug.Log(GlobalData.lightSwitch);
        if (GlobalData.lightSwitch)
        {
            lightSwitch.transform.localPosition = new Vector3(-0.04849097f, 2.208767f, 2.850958f);
            lightSwitch.transform.localRotation = new Quaternion(-0.000583283196f, 0.197512761f, -0.00289495266f, 0.980295897f);
        }
        else
        {
            lightSwitch.transform.localPosition = new Vector3(0.3853737f, 1.660247f, 2.669332f);
            lightSwitch.transform.localRotation = new Quaternion(-0.166412354f, 0.106388971f, -0.82593739f, 0.528030097f);
        }
    }  

    public void LightsFlip()
    {
        foreach (Light child in lights)
        {
            Light light = child.GetComponent<Light>();
            light.enabled = !light.enabled;
        }
    }
}