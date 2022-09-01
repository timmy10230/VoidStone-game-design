using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBag : MonoBehaviour
{

    public GameObject myBag; //背包物體
    private bool isOpenBag = false;
    



    // Update is called once per frame
    void Update()
    {
        OpenMybag();
    }

    void OpenMybag()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpenBag = !isOpenBag; //改變false, true
            myBag.SetActive(isOpenBag);
        }

    }
}
