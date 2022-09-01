using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Transform firePoint1;
    public Transform firePoint2;
    public GameObject bulletPrefab;
    public GameObject bullet2Prefab;
    public GameObject fire;

    public float fireRate = 0.5F;
    public float nextFire = 0.0F;

   

    public void Shoot1()
    {
               
        if (Time.time > nextFire)//讓子彈發射有間隔
        {
            nextFire = Time.time + fireRate;//Time.time表示從遊戲開發到現在的時間，會隨着遊戲的暫停而停止計算。
            Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);

            Instantiate(fire, Vector2.zero, Quaternion.identity);

        }
        
    }

    public void Shoot2()
    {
        if (Time.time > nextFire)//讓子彈發射有間隔
        {
            nextFire = Time.time + fireRate;//Time.time表示從遊戲開發到現在的時間，會隨着遊戲的暫停而停止計算。
            Instantiate(bullet2Prefab, firePoint2.position, firePoint2.rotation);
        }
    }
}
