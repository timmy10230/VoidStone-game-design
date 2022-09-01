using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : ScriptableObject
{
    public int itemNum;
    public string itemName;
    public Sprite itemImage;
    public int itemHeld;
    

    [TextArea]
    public string itemInfo;

    public virtual void use()
    {
    }
}






