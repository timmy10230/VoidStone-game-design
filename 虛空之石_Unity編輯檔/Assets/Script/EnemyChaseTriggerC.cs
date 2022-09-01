using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseTriggerC : MonoBehaviour
{
    public GameObject enemy;


    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.gameObject.tag == "Player") //&& enemy.GetComponent<MonsterWanderD>())
        {

            enemy.GetComponent<MonsterWanderC>().GiveEnumNum(4);
            //Debug.Log("Trigger");
        }
    }
}
