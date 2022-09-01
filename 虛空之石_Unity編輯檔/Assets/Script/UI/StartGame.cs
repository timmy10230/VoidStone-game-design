using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void StarGameText()
    {
        
        StartCoroutine(DestroyUI());

        if (GameManager._instance.gameTeaching != null)
        {
            GameManager._instance.gameTeaching.SetActive(true);
            
        }
        else
        {
            TimeUI._instance.startCount = true;
            playerControl.pc.canMove = true;
        }

        IEnumerator DestroyUI()
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
    }
}
