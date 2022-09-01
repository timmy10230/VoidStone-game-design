using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUp : MonoBehaviour
{
    Animator ani;

    private void Start()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        if (TimeUI._instance.NTime <= 0)
        {
            StartCoroutine(TimeUpT());
        }
    }

    IEnumerator TimeUpT() //時間UI上升
    {
        yield return new WaitForSeconds(2f);
        ani.SetTrigger("timeUp");
        yield return new WaitForSeconds(3f);
        Destroy(this);
    }
}
