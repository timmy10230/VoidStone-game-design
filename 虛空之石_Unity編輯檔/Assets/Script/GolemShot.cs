using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemShot : MonoBehaviour
{
    public Transform shotPoint;
    //public Transform firePoint2;
    public GameObject golemShotPrefab;
    //public GameObject fire;  //音效

    //public float fireRate = 0.5F;
    //public float nextFire = 0.0F;



    public void GholemShoot()
    {
        Instantiate(golemShotPrefab, shotPoint.position, shotPoint.rotation);
        //Instantiate(fire, Vector2.zero, Quaternion.identity); //音效

        /* if (Time.time > nextFire)//讓子彈發射有間隔
         {
             nextFire = Time.time + fireRate;//Time.time表示從遊戲開發到現在的時間，會隨着遊戲的暫停而停止計算。
             Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);

             Instantiate(fire, Vector2.zero, Quaternion.identity);

         }*/

    }

}
