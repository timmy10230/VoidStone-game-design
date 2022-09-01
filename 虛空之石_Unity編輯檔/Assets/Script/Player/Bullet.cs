using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : DamageSystem
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject enemyBeHitAudio;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed; //移動
        Destroy(gameObject, 0.53f); //0.53秒後銷燬子彈
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        switch ((int)type)
        {
            case 0:                                                         //玩家打敵人
                if (co.gameObject.tag == "Enemy")
                {
                    co.GetComponent<Enemy>().Damage(dmg);
                    Destroy(gameObject);
                    Instantiate(enemyBeHitAudio, Vector2.zero, Quaternion.identity);
                }
                else if (co.gameObject.tag == "EnemyBoss")
                {
                    co.GetComponent<EnemyBoss>().TakeDamage(dmg);  //BOSS受傷
                    Destroy(gameObject);
                    Instantiate(enemyBeHitAudio, Vector2.zero, Quaternion.identity);
                }
                else if (co.gameObject.tag == "EnemyBat")
                {
                    co.GetComponent<EnemyBat>().TakeDamage(dmg);  //bat受傷
                    Destroy(gameObject);
                    Instantiate(enemyBeHitAudio, Vector2.zero, Quaternion.identity);


                }

                else if (co.gameObject.tag == "FireWorm")
                {
                    co.GetComponent<FireWorm>().BeHit(dmg);  //bat受傷
                    Destroy(gameObject);
  
                    Instantiate(enemyBeHitAudio, Vector2.zero, Quaternion.identity);
                }
                break;
            case 1:                                        //敵人打玩家
                if (co.gameObject.tag == "Player")
                {
                    co.GetComponent<Battle>().Damage(dmg);
                }
                break;
            case 2:
                if (co.gameObject.layer == 10 || co.gameObject.layer == 9)
                {
                    co.GetComponent<Enemy>().Damage(dmg);
                }
                break;
        }
    }
}
