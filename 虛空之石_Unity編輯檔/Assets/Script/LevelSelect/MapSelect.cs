using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour {

    public int starsNum = 0;
    private bool isSelect = false;

    public GameObject locks;
    public GameObject stars;

    public GameObject panel;
    public GameObject map;
    public Text starsText;

    public int startNum;
    public int endNum;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();

        if (PlayerPrefs.GetInt("totalNum", 0) >= starsNum)
        {
            isSelect = true;
        }

        if (isSelect)
        {
            locks.SetActive(false);
            stars.SetActive(true);

            int counts = 0;
            for(int i = startNum; i <= endNum; i++)
            {
                counts += PlayerPrefs.GetInt("level" + i.ToString(), 0);
            }
            if(startNum == endNum)
            {
                starsText.text = counts.ToString() + "/3";
            }
            else
            {
                starsText.text = counts.ToString() + "/6";
            }
        }
    }

    public void Selected()
    {
        if (isSelect)
        {
            panel.SetActive(true);
            map.SetActive(false);
        }
    }

    public void PanelSelect()
    {
        panel.SetActive(false);
        map.SetActive(true);
    }
}
