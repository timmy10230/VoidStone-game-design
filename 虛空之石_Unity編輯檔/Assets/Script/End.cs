using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(TimeUI._instance.NTime > 0)
            {
                GameManager._instance.nowStarsNum += 1;
            }
            GameManager._instance.nowStarsNum += 1;
            if (GameManager._instance.playerUI != null)
            {
                GameManager._instance.playerUI.SetActive(false);
            }
            GameManager._instance.win.SetActive(true);
            playerControl.pc.canMove = false;
            TimeUI._instance.startCount = false;
        }
    }
}
