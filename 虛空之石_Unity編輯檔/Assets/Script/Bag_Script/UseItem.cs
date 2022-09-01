using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
    //public Item item;
    public Bag playerBag;
    public ItemColdDown itemColdDown;
    //public GameObject useItemAudioPrefab;
    //public ItemColdDown itemColdDown;

    // Use this for initialization


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1)&&playerControl.pc.canMove==true)
        {
            //if (itemColdDown.timer == 0)
           // {

                if (playerBag.itemList[0] != null )
                {
                    //Instantiate(useItemAudioPrefab, null);
                    
                    if (playerBag.itemList[0].itemHeld > 0)
                    {
                        playerBag.itemList[0].use();
                        //playerBag.itemList[0] = null;

                    }
                    BagManager.RefreshItem();

                    /*itemColdDown.isStartTime = true;
                    if (itemColdDown.isStartTime)//当开始计时时执行下列代码
                    {
                        while (itemColdDown.timer < itemColdDown.coldTime)
                        {
                            itemColdDown.timer += Time.deltaTime;
                            itemColdDown.filledImage.fillAmount = (itemColdDown.coldTime - itemColdDown.timer) / itemColdDown.coldTime;//按照时间比例显示图片，刚开始点击时，timer小，应该显示的背景图大
                        }
                        if (itemColdDown.timer >= itemColdDown.coldTime)//判断是否达到冷却时间
                        {
                            itemColdDown.filledImage.fillAmount = 0;//冷却图不显示
                            itemColdDown.timer = 0;//重置计时器
                            itemColdDown.isStartTime = false;//结束计时
                        }
                    }*/


                    //itemColdDown.ColdDown();

                    
                }
            //}


        }


        if (playerBag.itemList[1] != null && playerControl.pc.canMove == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //Instantiate(useItemAudioPrefab, null);
                //playerBag.itemList[1].use();
                if (playerBag.itemList[1].itemHeld > 0)
                {
                    playerBag.itemList[1].use();
                    //playerBag.itemList[1] = null;

                }
                BagManager.RefreshItem();
            }
        }
            

        /*if (playerBag.itemList[2] != null && playerControl.pc.canMove == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                //Instantiate(useItemAudioPrefab, null);
                playerBag.itemList[2].use();
                if (playerBag.itemList[2].itemHeld == 0)
                {
                    playerBag.itemList[2] = null;

                }
                BagManager.RefreshItem();
            }
        }
            

        if (playerBag.itemList[3] != null && playerControl.pc.canMove == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                //Instantiate(useItemAudioPrefab, null);
                playerBag.itemList[3].use();
                if (playerBag.itemList[3].itemHeld == 0)
                {
                    playerBag.itemList[3] = null;

                }
                BagManager.RefreshItem();
            }
        }
            

        if (playerBag.itemList[4] != null && playerControl.pc.canMove == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                //Instantiate(useItemAudioPrefab, null);
                playerBag.itemList[4].use();
                if (playerBag.itemList[4].itemHeld == 0)
                {
                    playerBag.itemList[4] = null;

                }
                BagManager.RefreshItem();
            }
        }
            

        if (playerBag.itemList[5] != null && playerControl.pc.canMove == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                //Instantiate(useItemAudioPrefab, null);
                playerBag.itemList[5].use();
                if (playerBag.itemList[5].itemHeld == 0)
                {
                    playerBag.itemList[5] = null;

                }
                BagManager.RefreshItem();
            }
        }*/
            
    }
    
}
