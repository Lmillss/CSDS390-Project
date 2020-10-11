﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QTESys : MonoBehaviour
{
    public GameObject LetterBox;//display the key player needs to press
    public GameObject LetterBoxOuter;
    public GameObject PassBox;//display if pass or fail the current QTE
    public GameObject NumSuccess;//display number of successed QTE
    public GameObject Panel;
    public GameObject Bar;
    public GameObject CheckFiller;
    public GameObject BoxCover;
    public int QTEGen;//so far 3 different types of QTE letter, E R T, 4=wrong key pressed
    public int WaitingForKey;//0=is waiting to generate a QTE key
    public int CorrectKey;//1=correct key pressed,2=wrong key pressed,0=reset this state
    public int CountingDown;
    public int numofcorrect;
    public float TimeLeft;
    public int maxprogress = 5;
    //public Vector3 Loc1;
    //public Vector3 Loc2;
    //public Vector3 Loc3;

    // Start is called before the first frame update
    void Start()
    {
        //Loc1 = LetterBoxOuter.GetComponent<RectTransform>().position;
        //Debug.Log(Loc1);
        //Loc2 = new Vector3(200, 80, 0);
        //Loc3 = new Vector3(-150, -40, 0);
        NumSuccess.GetComponent<Text>().text = "0";
        numofcorrect = Int32.Parse(NumSuccess.GetComponent<Text>().text);
        Debug.Log(NumSuccess.GetComponent<Text>().text);
        Bar.GetComponent<Slider>().value = 0;
        //TimeLeft = FilledBar.GetComponent<Image>().fillAmount;
        CheckFiller.GetComponent<Image>().color = new Color32(210, 20, 0, 0);//transparent
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main");
        }

        numofcorrect = Int32.Parse(NumSuccess.GetComponent<Text>().text);
        if (numofcorrect < 4 && Panel.activeSelf == true)
        {
            if (WaitingForKey == 0)
            {
                QTEGen = UnityEngine.Random.Range(1, 4);
                CountingDown = 1;
                StartCoroutine(CountDown());
                if (QTEGen == 1)
                {
                    WaitingForKey = 1;
                    LetterBox.GetComponent<Text>().text = "[E]";
                }
                if (QTEGen == 2)
                {
                    WaitingForKey = 1;
                    //LetterBoxOuter.GetComponent<RectTransform>().position = Loc2;
                    LetterBox.GetComponent<Text>().text = "[R]";
                }
                if (QTEGen == 3)
                {
                    WaitingForKey = 1;
                    LetterBox.GetComponent<Text>().text = "[T]";
                }
            }

            if (QTEGen == 1)
            {
                if (Input.anyKeyDown)
                {
                    if(Input.GetKeyDown("e"))//if (Input.GetButtonDown("EKey"))
                    {
                        CorrectKey = 1;
                        StartCoroutine(KeyPressing());
                    }
                    else//if not pressing the right key
                    {
                        CorrectKey = 2;
                        StartCoroutine(KeyPressing());
                    }
                }
            }
            if (QTEGen == 2)
            {
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown("r"))
                    {
                        CorrectKey = 1;
                        StartCoroutine(KeyPressing());
                    }
                    else//if not pressing the right key
                    {
                        CorrectKey = 2;
                        StartCoroutine(KeyPressing());
                    }
                }
            }
            if (QTEGen == 3)
            {
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown("t"))
                    {
                        CorrectKey = 1;
                        StartCoroutine(KeyPressing());
                    }
                    else//if not pressing the right key
                    {
                        CorrectKey = 2;
                        StartCoroutine(KeyPressing());
                    }
                }
            }
        }
        else if (PassBox.GetComponent<Text>().text == "Connected!")
        {
            //do nothing, just wait;
        }
        else if (numofcorrect >= 3 && Panel.activeSelf == true)
        {
            //Debug.Log(numofcorrect);
            //Debug.Log(Panel.activeSelf.ToString());
            PassBox.GetComponent<Text>().text = "Connected!";
            CheckFiller.GetComponent<Image>().color = new Color32(20, 210, 0, 255);//turn green
            Debug.Log("Connected");
            StartCoroutine(EndWireConnection());
        }
    }

    /**public void changeLoc()
    {
        int num = UnityEngine.Random.Range(1, 4);
        if (num == 1)
        {
            LetterBoxOuter.GetComponent<RectTransform>().position = Loc1;
        }
        else if (num == 2)
        {
            LetterBoxOuter.GetComponent<RectTransform>().position = Loc2;
        }
        else
        {
            LetterBoxOuter.GetComponent<RectTransform>().position = Loc3;
        }
    }**/

    IEnumerator KeyPressing()
    {
        QTEGen = 4;
        if (CorrectKey == 1)//done it correctly
        {
            numofcorrect = numofcorrect + 1;
            Bar.GetComponent<Slider>().value = numofcorrect;
            NumSuccess.GetComponent<Text>().text = "" + numofcorrect + "";
            CountingDown = 2;
            //PassBox.GetComponent<Text>().text = "PASS!";
            Debug.Log("PASS");
            yield return new WaitForSeconds(1f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";
            LetterBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(0.5f);
            WaitingForKey = 0;
            CountingDown = 1;
        }
        if (CorrectKey == 2)//incorrect key pressed
        {
            CountingDown = 2;
            //PassBox.GetComponent<Text>().text = "FAIL!";
            Debug.Log("FAIL");
            CheckFiller.GetComponent<Image>().color = new Color32(210, 20, 0, 255);
            yield return new WaitForSeconds(1f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";
            LetterBox.GetComponent<Text>().text = "";
            CheckFiller.GetComponent<Image>().color = new Color32(210, 20, 0, 0);
            yield return new WaitForSeconds(0.5f);
            WaitingForKey = 0;
            CountingDown = 1;
        }
        //changeLoc();
    }

    IEnumerator CountDown()
    {//wait and refresh everything
        yield return new WaitForSeconds(0.5f);
        if (CountingDown == 1)
        {
            QTEGen = 4;
            CountingDown = 2;
            //PassBox.GetComponent<Text>().text = "FAIL!";
            CheckFiller.GetComponent<Image>().color = new Color32(210, 20, 0, 255);
            yield return new WaitForSeconds(1f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";
            LetterBox.GetComponent<Text>().text = "";
            CheckFiller.GetComponent<Image>().color = new Color32(210, 20, 0, 0);
            yield return new WaitForSeconds(0.5f);
            WaitingForKey = 0;
            CountingDown = 1;
        }
    }

    IEnumerator EndWireConnection()
    {
        yield return new WaitForSeconds(0.5f);
        //yield return new WaitForSecondsRealtime(1);
        Panel.SetActive(false);
        Debug.Log("animation played");
        BoxCover.GetComponent<Animator>().Play("BoxCoverSliding");
        yield return new WaitForSeconds(1f);
        Debug.Log("Main");
        GlobalData.wiresBroken = false;
        SceneManager.LoadScene("Main");
        //closebutton.GetComponent<Button>().onClick.Invoke();
    }

    IEnumerator TimerCountDown()
    {
        yield return new WaitForSeconds(3.5f);
        while (TimeLeft != 0)
        {
            TimeLeft = TimeLeft - (1 / 3.5f);
            //FilledBar.GetComponent<Image>().fillAmount = TimeLeft;
        }

    }


}
