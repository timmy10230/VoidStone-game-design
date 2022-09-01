using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveBag : MonoBehaviour, IDragHandler
{

    RectTransform currentRect; //背包座標

    void Awake()
    {
        currentRect = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentRect.anchoredPosition += eventData.delta;  //anchoredPosition為該物件中心座標  eventData.delta滑鼠變量
    }

}
