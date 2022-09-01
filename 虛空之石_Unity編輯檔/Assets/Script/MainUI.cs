using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public Transform hearts;
    public Sprite[] heartImage;
    public Transform df;
    public Sprite[] dfImage;
    public RectTransform sp;

    void Awake()
    {
        DrawMaxHP();
        DrawMaxDF();
    }

    void Update()
    {
        DrawSP();
        DrawHP();
        DrawDF();
    }

    void DrawMaxHP()
    {
        for (int i = 1; i < Game.sav.maxHp; i++)
        {
            Transform h = Instantiate(hearts.GetChild(0));
            h.SetParent(hearts, false);
            h.name = "h" + (i + 1);
        }
        DrawHP();
    }

    void DrawHP()
    {
        float hp = Game.sav.hp;
        foreach (Image img in hearts.GetComponentsInChildren<Image>())
        {
            img.sprite = heartImage[1];
        }

        for (int i = 1; i <= hp; i++)
        {
            hearts.GetChild(i - 1).GetComponent<Image>().sprite = heartImage[0];
        }

    }

    void DrawMaxDF()
    {
        for (int i = 1; i < Game.sav.maxDf; i++)
        {
            Transform d = Instantiate(df.GetChild(0));
            d.SetParent(df, false);
            d.name = "d" + (i + 1);
        }
        DrawDF();
    }

    void DrawDF()
    {
        float ndf = Game.sav.df;
        foreach (Image img in df.GetComponentsInChildren<Image>())
        {
            img.sprite = dfImage[1];
        }

        for (int i = 1; i <= ndf; i++)
        {
            df.GetChild(i - 1).GetComponent<Image>().sprite = dfImage[0];
        }

    }

    void DrawSP()
    {
        float s = Game.sav.sp;
        sp.sizeDelta = new Vector2(Mathf.Lerp(sp.sizeDelta.x, s, 0.12f), sp.sizeDelta.y);
    }
}
