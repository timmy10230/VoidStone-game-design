using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    public static SaveData sav = new SaveData();
}

public class SaveData
{
    public float maxHp = 5f;
    public float hp = 5.0f;
    public float maxSp = 350f;
    public float sp = 350f;
    public float maxDf = 3f;
    public float df = 3.0f;
    public float melee1Cost = 30f;
    public float melee2Cost = 50f;
    public float dashCost = 80f;
    public float regSpeed = 20f;  //SP

    public float Damage(float dmg)
    {
        hp = Mathf.Clamp(hp - dmg, 0, maxHp);
        return hp;
    }

    public float reduceDf(float dmg)
    {
        df = Mathf.Clamp(df - dmg, 0, maxDf);
        sp += 30;
        return df;
    }

    public void CostSP(float s)
    {
        sp = Mathf.Clamp(sp - s, 0, maxSp);
    }

    public bool CanUseSP()
    {
        return sp > 10;
    }

    public void RegenSP(float f)
    {
        CostSP(-regSpeed * f);
    }

    
}