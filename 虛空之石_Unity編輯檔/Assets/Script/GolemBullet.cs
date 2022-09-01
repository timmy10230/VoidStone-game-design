using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBullet : DamageSystem
{
    public float speed = 12f;
    public Rigidbody2D rb;
    public Battle playerBattle;
    //public Animator ani;

    private GameObject player;
    private Transform playerPos;




    // Start is called before the first frame update
    void Start()
    {
        //ani = GetComponent<Animator>();
        //rb.velocity = transform.right * speed; //移動
        Destroy(gameObject, 3f); //1秒後銷燬子彈
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("BulletTrack", 0.3f); //0.3秒後執行1次
        rb.velocity = transform.right * speed;
        //InvokeRepeating("MethodName", 0, 1); //0秒後每隔1秒執行
        Debug.Log(playerBattle.canUseChangeSpace);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (playerBattle.canUseChangeSpace)
            {
                Destroy(gameObject, 0.5f);
            }
            
        }


    }

    void OnTriggerEnter2D(Collider2D co)
    {
        switch ((int)type)
        {
            /*case 0:                                                         //玩家打敵人
                if (co.gameObject.tag == "Enemy")
                {
                    co.GetComponent<Enemy>().Damage(dmg);
                    Destroy(gameObject);
                }
                else if (co.gameObject.tag == "EnemyBoss")
                {
                    co.GetComponent<EnemyBoss>().TakeDamage(dmg);  //BOSS受傷
                    Destroy(gameObject);
                }
                break;*/
            case 1:                                        //敵人打玩家
                if (co.gameObject.tag == "Player")
                {
                    co.GetComponent<Battle>().Damage(dmg);
                    Destroy();
                    //rb.velocity = transform.right * 0;                    
                    //ani.SetTrigger("ShotHit");
                    //Debug.Log("hit");

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

    /*public void KeepBulletShape()
    {
        ani.SetBool("KeepShape", true);
    }*/

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void BulletTrack()
    {
        //物體旋轉指向目標
        player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y + 1.3f, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        playerPos = GameObject.FindGameObjectWithTag("Player").transform; //玩家位置
        Vector2 target = new Vector2(playerPos.position.x, playerPos.position.y + 1.2f);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        CancelInvoke("BulletTrack");

    }

}
