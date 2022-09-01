using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trap : MonoBehaviour
{
    public GameObject PlayerTrapBornPos;
    public GameObject rePlayerPos;
    public GameObject playerUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerControl.pc.canMove = false;
            if (Game.sav.hp != 0)
            {
                Game.sav.hp -= 1;
                StartCoroutine(PlayerRePos());
            }
            else
            {
                playerControl.pc.Die();
            }

            IEnumerator PlayerRePos()
            {
                playerControl.pc.ani.SetTrigger("Die");
                yield return new WaitForSeconds(2.5f);
                playerUI.SetActive(false);
                rePlayerPos.SetActive(true);
                yield return new WaitForSeconds(0.7f);
                collision.transform.position = PlayerTrapBornPos.transform.position;
                playerControl.pc.ani.SetTrigger("TrapRePos");
                playerControl.pc.canMove = true;
                yield return new WaitForSeconds(1f);
                playerUI.SetActive(true);
                yield return new WaitForSeconds(1f);
                rePlayerPos.SetActive(false);
            }
        }
    }


}
