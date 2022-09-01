using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceChange : MonoBehaviour
{
    public void ChangeToSpaceB()
    {
        GameManager._instance.spaceAStone.SetActive(false);
        GameManager._instance.spaceA.SetActive(false);
        GameManager._instance.spaceBStone.SetActive(true);
        GameManager._instance.spaceB.SetActive(true);
    }

    public void ChangeToSpaceA()
    {
        GameManager._instance.spaceB.SetActive(false);
        GameManager._instance.spaceBStone.SetActive(false);
        GameManager._instance.spaceA.SetActive(true);
        GameManager._instance.spaceAStone.SetActive(true);
    }

}
