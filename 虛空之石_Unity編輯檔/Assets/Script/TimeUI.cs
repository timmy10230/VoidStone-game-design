using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class TimeUI : MonoBehaviour
{
    static public TimeUI _instance;
    public Text timeText;
    public float NTime;
    private int min;
    private int sec;
    private float mSec;
    public bool startCount = false;

    private void Awake()
    {
        NTime = GameManager._instance.starTime;
        _instance = this;
    }

    void Start()
    {
        mSec = (int)((NTime - (int)NTime) * 100);
        sec = (int)NTime % 60;
        min = (int)NTime / 60;

        if (min < 10)
        {
            if (sec < 10)
            {
                if (mSec < 10)
                {
                    timeText.text = "0" + min + ":0" + sec + ":0" + mSec;
                }
                else
                {
                    timeText.text = "0" + min + ":0" + sec + ":" + mSec;
                }
            }
            else
            {
                if (mSec < 10)
                {
                    timeText.text = "0" + min + ":" + sec + ":0" + mSec;
                }
                else
                {
                    timeText.text = "0" + min + ":" + sec + ":" + mSec;
                }
            }
        }
        else
        {
            if (sec < 10)
            {
                if (mSec < 10)
                {
                    timeText.text = min + ":0" + sec + ":0" + mSec;
                }
                else
                {
                    timeText.text = min + ":0" + sec + ":" + mSec;
                }
            }
            else
            {
                if (mSec < 10)
                {
                    timeText.text = min + ":" + sec + ":0" + mSec;
                }
                else
                {
                    timeText.text = min + ":" + sec + ":" + mSec;
                }
            }
        }
    }

    void Update()
    {
        if (startCount && NTime >= 0)
        {
            NTime -= Time.deltaTime;
        }
        else if (NTime <= 0)
        {
            startCount = false;
            NTime = 0;
        }
        else return;
 
        mSec = (int)((NTime - (int)NTime) * 100);
        sec = (int)NTime % 60;
        min = (int)NTime / 60;

        if (min < 10)
        {
            if (sec < 10)
            {
                if(mSec < 10)
                {
                    timeText.text = "0" + min + ":0" + sec + ":0" + mSec;
                }
                else
                {
                    timeText.text = "0" + min + ":0" + sec + ":" + mSec;
                }
            }
            else
            {
                if (mSec < 10)
                {
                    timeText.text = "0" + min + ":" + sec + ":0" + mSec;
                }
                else
                {
                    timeText.text = "0" + min + ":" + sec + ":" + mSec;
                }
            }
        }
        else
        {
            if (sec < 10)
            {
                if (mSec < 10)
                {
                    timeText.text = min + ":0" + sec + ":0" + mSec;
                }
                else
                {
                    timeText.text = min + ":0" + sec + ":" + mSec;
                }
            }
            else
            {
                if (mSec < 10)
                {
                    timeText.text = min + ":" + sec + ":0" + mSec;
                }
                else
                {
                    timeText.text = min + ":" + sec + ":" + mSec;
                }
            }
        }
        //timeText.text = min + ":" + sec + ":" + mSec;
    }

    public void StartCount()
    {
        playerControl.pc.canMove = true;
        startCount = true;
    }
}
