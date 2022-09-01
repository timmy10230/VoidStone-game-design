using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBullet : DamageSystem
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public Animator ani;
    public AudioSource hitPlayerAudio;


    // Start is called before the first frame update
    void Start()
    {
        hitPlayerAudio = GetComponent<AudioSource>();
        speed = 5;

        ani = GetComponent<Animator>();
        rb.velocity = transform.right * speed; //移動
        Destroy(gameObject, 1.5f); //1秒後銷燬子彈
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D co)
    {
        switch ((int)type)
        {
           
            case 1:                                        //敵人打玩家
                if (co.gameObject.tag == "Player")
                {
                    co.GetComponent<Battle>().Damage(dmg);
                    rb.velocity = transform.right * 0;
                    ani.SetTrigger("ShotHit");
                    
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

    public void KeepBulletShape()
    {
        ani.SetBool("KeepShape", true);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void ChangeSpeed()
    {
        speed = 20;
        rb.velocity = transform.right * speed; //移動
    }

    public void HitPlayerAudio()
    {
        hitPlayerAudio.Play();
    }
}
