﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class energy : MonoBehaviour
{
    float energyNum = 100;
    public Text energyText;

    // Start is called before the first frame update
    void Start()
    {
        energyText.text = "Energy: " + (int)energyNum + "%";
    }

    // Update is called once per frame
    void Update()
    {
        energyNum -= Time.deltaTime * 0.1f;
        energyText.text = "Energy: " + (int)energyNum + "%";
    }
}
