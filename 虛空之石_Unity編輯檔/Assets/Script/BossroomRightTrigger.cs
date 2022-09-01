using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossroomRightTrigger : MonoBehaviour
{
    public Animator ani;
    public GameObject Boss;
    public GameObject BossHealth;
    //public GameObject BossAudio;
    public AudioSource Music1;
    public AudioSource Music2;
    public GameObject SpaceA;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {            
            ani.SetTrigger("Enter");
            Boss.SetActive(true);
            BossHealth.SetActive(true);
            //BossAudio.SetActive(true);
            if (SpaceA.activeSelf)
            {

                Music1.Play();
                Music2.Pause();
            }
            else
            {
                Music1.Play();
                Music1.Pause();
                Music2.UnPause();
            }
        }
    }


}
