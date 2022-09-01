using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;
    public Bag myBag;
    private int currentItemID; //當前物品格子ID編號


    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent; //要交換slot物件原始位置
        currentItemID = originalParent.GetComponent<Slot>().slotID;


        transform.SetParent(transform.parent.parent); //升級成父級物件
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;  //GetComponent可以去取得組件屬性  開啟滑鼠射線
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);  //鼠標碰到第1個物件的名稱
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null) //用來防止物品移到背包介面外，如果移到背包介面外eventData.pointerCurrentRaycast.gameObject就會變成空物件
        {

            if (eventData.pointerCurrentRaycast.gameObject.name == "number")  //判斷下面物體是否為number，進行交換
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;

                //itemList的物品儲存位置改變
                var temp = myBag.itemList[currentItemID];
                myBag.itemList[currentItemID] = myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;

                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true; //關閉滑鼠射線
                return;
            }

            if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)") //只有框框則直接覆蓋
            {


                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;

                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = myBag.itemList[currentItemID];
                if (eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID != currentItemID)
                {

                    myBag.itemList[currentItemID] = null;

                }
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
        }
        //其他任何位置回歸原位
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

       
    }


}
