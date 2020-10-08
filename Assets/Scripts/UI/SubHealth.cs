﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubHealth : MonoBehaviour
{
    public static int healthNum = 100;
    public Text healthText;
    public int monsterAttack = 0;
    public int hullBreach = 0;

    // Start is called before the first frame update
    void Start()
    {
        healthText.text = "Health: " + healthNum + "%";
    }

    // Update is called once per frame
    void Update()
    {
       if (monsterAttack > 0)
        {
            healthNum -= (monsterAttack * 20);
            monsterAttack = 0;
        }

       if (hullBreach > 0)
        {
            healthNum -= 5;
        }

        healthText.text = "Health: " + healthNum + "%";
    }
}