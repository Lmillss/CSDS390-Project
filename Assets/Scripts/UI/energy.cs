﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class energy : MonoBehaviour
{
    public static float energyNum = 100;
    public Text energyText;

    // Start is called before the first frame update
    void Start()
    {
        
        energyText.text = "Energy: " + (int)energyNum + "%";
    }

    // Update is called once per frame
    void Update()
    {
        if (!ESCDectect.gameIsPaused) {
            energyNum -= Time.deltaTime * 0.2f;
            energyText.text = "Energy: " + (int)energyNum + "%";
        }
    }
}
