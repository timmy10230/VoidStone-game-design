using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public Transform player; //玩家
    public Animator ani; //BOSS動畫
    public bool isFlipped = true;  //BOSS左右反轉

    public float hp = 20;  //BOSS血量

    public GameObject bossHealthBarFill;
    public GameObject bossHealthBar;

    public void Awake()
    {
        ani = GetComponent<Animator>();
    }

    public void LookAtPlayer() //面相玩家
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if(transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;  
        }
    }

    public void TakeDamage(float damage) //BOSS受傷
    {
        if(hp > 0)
        {
            hp -= damage;
            ani.SetTrigger("Hurt");
        }

        if (hp <= 0) //HP低於0死亡
        {
            Die();
        }
        
    }

    public void Die()  //啟動死亡動畫
    {
        ani.SetTrigger("Die");
        
        //Destroy(gameObject);
    }

    public void DestoryBoss() //銷毀BOSS物體
    {
        Destroy(gameObject);
        Destroy(bossHealthBar);

}

    public void DestoryBossHealthBarFill() //銷毀BOSS物體
    {
        
        Destroy(bossHealthBarFill);
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
