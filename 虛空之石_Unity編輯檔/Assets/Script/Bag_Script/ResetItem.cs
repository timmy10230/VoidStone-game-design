using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetItem : MonoBehaviour
{
    public RedPotion playerBagRedPotion;
    public BluePotion playerBagBluePotion;
    // Start is called before the first frame update
    void Start()
    {
        playerBagRedPotion.itemHeld = 5;
        playerBagBluePotion.itemHeld = 5;
        BagManager.RefreshItem();
    }

  

    
}
