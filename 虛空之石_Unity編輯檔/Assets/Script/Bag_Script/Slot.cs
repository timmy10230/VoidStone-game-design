using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotID; //背包裡格子物品ID
    public Item slotItem;
    public Image slotImage; //物品圖
    public Text slotNum; //物品數量顯示
    public string slotInfo;

    public GameObject itemInSlot;


    public void ItemOnClicked()  //點擊物品更新資訊
    {
        BagManager.UpdateItemInfo(slotInfo);
    }

    public void SetupSlot(Item item)
    {
        if(item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }

        slotItem = item;
        slotImage.sprite = item.itemImage;
        slotNum.text = item.itemHeld.ToString();
        slotInfo = item.itemInfo;
    }
}
