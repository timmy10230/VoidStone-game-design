using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseTriggerF : MonoBehaviour
{
    public GameObject enemy;


    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.gameObject.tag == "Player") //&& enemy.GetComponent<MonsterWanderD>())
        {

            enemy.GetComponent<MonsterWanderF>().GiveEnumNum(4);
            //Debug.Log("Trigger");
        }
    }
}
