using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BagManager : MonoBehaviour
{
    
    public static BagManager instance;  //Singleton Pattern單例模式  

    public Bag myBag;
    public GameObject slotGrid;
    //public Slot slotPrefab;
    public GameObject emptySlot;
    public Text itemInfo;

    public List<GameObject> slots = new List<GameObject>();

    void Update()
    {
        BagManager.RefreshItem();
    }

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;

    }

    private void OnEnable()
    {
        RefreshItem();
    }

    private void OnDisable()
    {
        RefreshItem();
    }

    public static void UpdateItemInfo(string itemDescription)
    {
        instance.itemInfo.text = itemDescription;
    }

    /*public static void CreateNewItem(Item item)
    {
        Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity); //建構新物品，生成Slot物件，取得生成物件位置，Quaternion.identity = rotation(0,0,0) 為回歸原始，不旋轉物體
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform); //設定slotPrefab為slotGrid的子級
        newItem.slotItem = item;  //傳入物品名稱
        newItem.slotImage.sprite = item.itemImage;  //物品圖片 要傳輸圖片必須+上sprite
        newItem.slotNum.text = item.itemHeld.ToString();  //物品數量  傳輸文字+上text，+上ToString()轉換成字串
    }*/

    public static void RefreshItem()
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {


            Destroy(instance.slotGrid.transform.GetChild(i).gameObject); //銷毀slotGrid所有的子級物件
            instance.slots.Clear(); //清空slots
        }

        for (int i = 0; i < instance.myBag.itemList.Count; i++) //重新創建所有物品
        {
            
            // CreateNewItem(instance.myBag.itemList[i]);
            instance.slots.Add(Instantiate(instance.emptySlot)); //生成slot
            instance.slots[i].transform.SetParent(instance.slotGrid.transform, false); //slot的父物件是grid    SetParent(Transform parent, bool worldPositionStays) worldPositionStays為true會使物體跟著世界大小做改變
            instance.slots[i].GetComponent<Slot>().slotID = i;  //給予每個slot有ID編號
            instance.slots[i].GetComponent<Slot>().SetupSlot(instance.myBag.itemList[i]);  //更新格子
        }
    }

    


};

