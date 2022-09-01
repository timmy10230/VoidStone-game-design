using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item thisItem;
    public Bag playerBag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) //判斷Tag是否為player
        {
            AddNewItem();
            Destroy(gameObject);
        }
    }



    public void AddNewItem()
    {
        if (!playerBag.itemList.Contains(thisItem))  //判斷物品是否已存在
        {
            //playerBag.itemList.Add(thisItem);  //新增該物品
            //BagManager.CreateNewItem(thisItem);

            for(int i=0 ; i<playerBag.itemList.Count ; i++)  //增加該物品
            {
                if(playerBag.itemList[i] == null)
                {
                    playerBag.itemList[i] = thisItem;
                    break;
                }
            }
            thisItem.itemHeld = thisItem.itemHeld + 1;
        }
        else
        {
            thisItem.itemHeld = thisItem.itemHeld + 1;  //該物品數量+1


        } 

        BagManager.RefreshItem();
    }

}
