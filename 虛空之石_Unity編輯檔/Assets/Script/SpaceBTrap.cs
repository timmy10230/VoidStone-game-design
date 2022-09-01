using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (Game.sav.hp != 0)
            {
                playerControl.pc.canMove = false;
                Invoke("PlayerReMove",0.5f);
                playerControl.pc.ani.SetTrigger("Damage");
                Game.sav.hp -= 1;
            }
            else
                playerControl.pc.Die();
        }
        
    }

    public void PlayerReMove()
    {
        playerControl.pc.canMove = true;
    }
}
