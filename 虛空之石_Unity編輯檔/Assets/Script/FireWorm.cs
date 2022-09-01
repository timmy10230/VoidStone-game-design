using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    FireBall,
    FirePillar,
    Dash,
    Idle,
    BeHit,
    Death,
}
public class FireWorm : MonoBehaviour
{
    Transform C_tra;
    Transform Mouth;
    Rigidbody2D C_rig;
    Animator C_ani;
    public Animator BossroomLeftWood;
    //BackGround background;//改变背景图(改成灯光变暗)
   // public AudioSource HitAudio;
    public AudioSource AttackAudio;
    public AudioSource DeadAudio;
    //public Animator EffectAni;//特效的播放
    public GameObject Fireball;//火球的预制体
    public GameObject FirePillar;//火柱的预制体
    public GameObject Player;//获取到玩家的位置
    //public GameObject Pillar1;//平台一
    //public GameObject Pillar2;//平台二
    public FireBall FireballScript;

    public Transform rightTransform;
    public Transform leftTransform;

    Vector2 Initial;

    BossState state;

    public float MaxHp ;
    public float Hp;

    public float MoveDamge;

    public float Speed;

    public float IdleTime;
    public float FirePillarCd;
    public float time;

    public int FireBallAttackTime;
    public int FirePillarAttackTime;

    public bool isHit;
    public bool isDead;

    public GameObject sign;
    public GameObject healthBar;
    public GameObject BossBGM;
    public GameObject LevelBGM;

    void Awake()
    {
        C_tra = GetComponent<Transform>();
        C_rig = GetComponent<Rigidbody2D>();
        C_ani = GetComponent<Animator>();
        //background = GameObject.Find("BlackCloth").GetComponent<BackGround>();
        Player = GameObject.Find("player");
        Mouth = transform.Find("Mouth");
        FireballScript = Fireball.GetComponent<FireBall>();

        Initial = C_tra.localScale;

        state = BossState.Idle;

        MaxHp = 60;
        Hp = MaxHp ;

        MoveDamge = 2;

        Speed = 12;

        isDead = false;

        IdleTime = 3f;
        FirePillarCd = 8f;
        time = 1f;

        FireBallAttackTime = 3;
        FirePillarAttackTime = 5;
    }
    void Update()
    {
        
        CheckHp();
        switch (state)
        {
            case BossState.FireBall:
                {
                    FireBallAttack();
                    break;
                }
            case BossState.FirePillar:
                {
                    FirePillarAttack();

                    break;
                }
            case BossState.Dash:
                {
                    DashSkill();
                    break;
                }
            case BossState.Idle:
                {
                    IdleProccess();
                    break;
                }
            case BossState.BeHit:
                {
                    BeHitProccess();
                    break;
                }
            case BossState.Death:
                {
                    C_ani.Play("Death");
                    break;
                }
        }
    }

    public void FireBallAttack() //吐火球
    {
        C_ani.Play("Attack");
        FirePillarCd -= Time.deltaTime;
        if (FireBallAttackTime <= 0 && !isDead && Hp > MaxHp / 2)
        {
            IdleTime = 6f;
            //Debug.Log("Change idle");
            state = BossState.Idle;
        }
        else if(FirePillarCd <= 0 && FireBallAttackTime <= 0 && !isDead && Hp <= MaxHp / 2)
        {
            IdleTime = 1.8f;
            state = BossState.Idle;

        }
        else if (FireBallAttackTime <= 0 && !isDead && Hp <= MaxHp / 2)
        {
            IdleTime = 5.5f;
            state = BossState.Idle;
        }
        //else if (!isHit)
        //{
           // gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
       // }
        else if (isDead)
        {
            state = BossState.Death;
        }
    }
    public void FireBallCreate() //生成火球 動畫事件
    {

        if (C_tra.localScale.x == Initial.x)
        {
            for (int i = -2; i < 5; i++)
            {
                if (Hp <= MaxHp / 2)
                {
                    FireballScript.Speed = 13.5f;
                    float rand = Random.Range(-1.2f, 1.2f); //範圍
                    //float irand = Random.Range(13f, 20f);
                    GameObject fireball = Instantiate(Fireball, null);
                    //float x = i + irand;
                    Vector3 dir = Quaternion.Euler(0, i * 18, 0) * -transform.right;
                    Vector3 randMouth = new Vector3(Mouth.position.x, Mouth.position.y + rand, 0);
                    fireball.transform.position = randMouth + dir * 1.0f;
                    //fireball.transform = new Vector2(Mouth.position.x , Mouth.position.y + rand);
                    fireball.transform.rotation = Quaternion.Euler(0, 0, i * -18);
                }
                else
                {
                    FireballScript.Speed = 10f;
                    float rand = Random.Range(-0.8f, 0.8f); //範圍
                    //float irand = Random.Range(18f, 25f);
                    GameObject fireball = Instantiate(Fireball, null);
                    //float x = i + irand;
                    Vector3 dir = Quaternion.Euler(0, i * 20, 0) * transform.right;
                    Vector3 randMouth = new Vector3(Mouth.position.x, Mouth.position.y + rand, 0);
                    fireball.transform.position = randMouth + dir * 1.0f;
                    //Vector2 randMouth = new Vector2(Mouth.transform.position.x, Mouth.transform.position.y + rand);
                    //fireball.transform.position = randMouth.position + dir * 1.0f;
                    fireball.transform.rotation = Quaternion.Euler(0, 0, i * -20);
                }                
                
            }
        }
        else if (C_tra.localScale.x == -Initial.x)
        {
            for (int i = -5; i < 2; i++)
            {
                //Debug.Log("123");
                if (Hp <= MaxHp / 2)
                {
                    FireballScript.Speed = 13.5f;
                    float rand = Random.Range(-1.2f, 1.2f); //範圍
                    //float irand = Random.Range(13, 20);
                    GameObject fireball = Instantiate(Fireball, null);
                    //float x = i + irand;
                    Vector3 dir = Quaternion.Euler(0, i * 18, 0) * -transform.right;
                    Vector3 randMouth = new Vector3(Mouth.position.x, Mouth.position.y + rand, 0);
                    fireball.transform.position = randMouth + dir * 1.0f;
                    //fireball.transform = new Vector2(Mouth.position.x , Mouth.position.y + rand);
                    fireball.transform.rotation = Quaternion.Euler(0, 0, i * -18);
                }
                else
                {
                    FireballScript.Speed = 10f;
                    float rand = Random.Range(-0.8f, 0.8f); //範圍
                    //float irand = Random.Range(18f, 25f);
                    GameObject fireball = Instantiate(Fireball, null);
                    //float x = i + irand;
                    Vector3 dir = Quaternion.Euler(0,  i* 20, 0) * transform.right;
                    Vector3 randMouth = new Vector3(Mouth.position.x, Mouth.position.y + rand, 0);
                    fireball.transform.position = randMouth + dir * 1.0f;
                    //Vector2 randMouth = new Vector2(Mouth.transform.position.x, Mouth.transform.position.y + rand);
                    //fireball.transform.position = randMouth.position + dir * 1.0f;
                    fireball.transform.rotation = Quaternion.Euler(0, 0, i * -20);
                    //Debug.Log(FireBallAttackTime);
                }
            }
        }
        FireBallAttackTime -= 1;
    }

    public void DashSkill()
    {
        if (!isDead)
        {
            //Debug.Log("Dash");
            IdleTime = 4f;
            Dash();
                      
            //state = BossState.Idle;
        }
        else if (isDead)
        {
            state = BossState.Death;
        }
    }
    public void Dash()//衝撞
    {
              
        if (C_tra.localScale.x == Initial.x)
        {
            C_ani.Play("Walk");
           
            if (transform.position.x < rightTransform.position.x)
            {
                if (Hp <= MaxHp / 2)
                {
                    Speed = 33;
                }
                else
                {
                    Speed = 25;
                }
                C_rig.velocity = new Vector2(Speed, C_rig.velocity.y);
                
                //Debug.Log("MOVE right");
            }
            else
            {
                Speed = 0;
                C_rig.velocity = new Vector2(Speed, C_rig.velocity.y);
                //transform.eulerAngles = new Vector3(0, 180, 0);
                C_tra.localScale = new Vector2(C_tra.localScale.x * -1, C_tra.localScale.y);
                int n = Random.Range(0, 1);
                if (n == 0 && FirePillarCd <= 0 && Hp <= MaxHp / 2)
                {
                    state = BossState.FirePillar;
                }
                else
                {
                    state = BossState.FireBall;
                }
            }
        }
        else if (C_tra.localScale.x == -Initial.x)
        {
            C_ani.Play("Walk");
            C_rig.velocity = new Vector2(-Speed, C_rig.velocity.y);
            if (transform.position.x > leftTransform.position.x)
            {
                if(Hp <= MaxHp / 2)
                {
                    Speed = 33;
                }
                else
                {
                    Speed = 25;
                }
                
                C_rig.velocity = new Vector2(-Speed, C_rig.velocity.y);

                //Debug.Log("MOVE left");
            }
            else
            {
                Speed = 0;
                C_rig.velocity = new Vector2(Speed, C_rig.velocity.y);
                //transform.eulerAngles = new Vector3(0, 0, 0);
                C_tra.localScale = new Vector2(C_tra.localScale.x * -1, C_tra.localScale.y);
                int n = Random.Range(0, 1);
                if(n == 0 && FirePillarCd <= 0 && Hp <= MaxHp/2)
                {
                    state = BossState.FirePillar;
                }
                else
                {
                    state = BossState.FireBall;
                }               
            }
        }
    }
    public void FirePillarAttack() //降下火球
    {
        C_ani.Play("OtherAttack");
        //background.isChange = true;
        //Pillar1.GetComponent<Collider2D>().enabled = false;
        //Pillar2.GetComponent<Collider2D>().enabled = false;
        FirePillarCd = Random.Range(5f, 20f);
        //Debug.Log(time);
        //Debug.Log(FirePillarAttackTime);
        if (FirePillarAttackTime <= 0 && !isDead)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                IdleTime = 4.9f;
                state = BossState.Idle;
                time = 1;
            }
        }
        else if (isDead)
        {
            state = BossState.Death;
        }
    }
    public void CreateFirePillar() //生成降下火球 動畫事件
    {
        for (int i = 0; i < 7; i++)
        {
            float r = Random.Range(leftTransform.position.x-1.5f , rightTransform.position.x + 1f ); //範圍  Instantiate(ghostShotPrefab, shotPoint.position, shotPoint.rotation);
            GameObject firepillar = Instantiate(FirePillar, null);
            firepillar.transform.position = new Vector3(r, 4.2f, 0);
            //Transform FirePillarTransform = new Vector3(r, 6f, 0f);
            //Quaternion rotation = new Quaternion.Euler (0, 0, 90);
            //Instantiate(FirePillar, new Vector3(r, 6f, 0f), new Quaternion (0,0,0,0));
        }
        FirePillarAttackTime -= 1;
    }
    public void IdleProccess()
    {
        //Debug.Log(C_tra.localScale.x );
        //Debug.Log(Initial.x);
        //Debug.Log("Idle");
        C_ani.Play("Idle");
        //background.isBack = true;
        //Pillar1.GetComponent<Collider2D>().enabled = true;
        //Pillar2.GetComponent<Collider2D>().enabled = true;
        FirePillarCd -= Time.deltaTime;
        IdleTime -= Time.deltaTime;
        if (Hp <= MaxHp / 2 && IdleTime > 0)
        {
            FireBallAttackTime = 4;
            FirePillarAttackTime = 4;
        }
        else if (Hp > MaxHp / 2 && IdleTime > 0)
        {
            FireBallAttackTime = 4;
            FirePillarAttackTime = 4;
        }
        if (IdleTime <= 0 && !isHit && !isDead)
        {
            //state = BossState.FireBall;
            //Debug.Log("change");
            if (FirePillarCd <= 0 && Hp <= MaxHp / 2)
            {
                state = BossState.FirePillar;
            }
            else
            {
                StartCoroutine("isDash");
            }
        }
        else if (isHit && !isDead)
        {
            state = BossState.BeHit;
        }
        else if (isDead)
        {
            state = BossState.Death;
        }
    }
    public void BeHitProccess()
    {
        C_ani.Play("BeHit");
        IdleTime -= Time.deltaTime;
;
        if (!isHit && !isDead)
        {
            state = BossState.Idle;
        }
        else if (isDead)
        {
            state = BossState.Death;
        }
    }
    public void BeHit(float Damge)
    {
        StartCoroutine("HitSpriteRed");
        Hp -= Damge;
        isHit = true;
        
        //HitAudio.Play();
        //EffectAni.Play("1");
    }
    public void BeHitOver()
    {
        isHit = false;
        
    }
    public void CheckHp()
    {
        if (Hp <= 0)
        {
            isDead = true;
        }
    }
    public void Death()
    {
        BossroomLeftWood.SetTrigger("BossDead");
        Destroy(BossBGM);
        LevelBGM.SetActive(true);
        Destroy(gameObject);
        healthBar.SetActive(false);
    }
    public void PlayAttackAudio()
    {
        AttackAudio.Play();
    }
    public void PlayDeadAudio()
    {
        DeadAudio.Play();
    }
    void OnTriggerEnter2D(Collider2D co)
    {
        /*if (collision.collider.CompareTag("AirWall"))
        {
            C_tra.localScale = new Vector3(-C_tra.localScale.x, C_tra.localScale.y, C_tra.localScale.z);
            state = BossState.FireBall;
        }*/
        if (co.gameObject.tag == "Player" )
        {
            if(state == BossState.Dash)
            {
                MoveDamge = 2;
            }
            else
            {
                MoveDamge = 1;
            }
            Player.GetComponent<Battle>().Damage(MoveDamge);
            //Debug.Log("hit");
            /*if (C_tra.position.x < Player.transform.position.x)
            {
                
            }
            else if (C_tra.position.x >= Player.transform.position.x)
            {
                collision.collider.GetComponent<PlayerCharacter>().BeHit(Vector2.left, MoveDamge);
            }*/
        }
        
    }

    public void bossIsHit()
    {
        //battle.isHit(); 
        Player.GetComponent<Battle>().isHit();


    }

    public void bossOverHit()
    {
        //battle.overHit();
        Player.GetComponent<Battle>().overHit();
        
    }

    
    IEnumerator HitSpriteRed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
        bossIsHit();
        yield return new WaitForSeconds(0.15f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        BeHitOver();

        //yield return new WaitForSeconds(0.4f);
        bossOverHit();
        StopCoroutine("HitSpriteRed");

    }

    private void OnDestroy()
    {
        sign.SetActive(false);
    }

    IEnumerator isDash()
    {
        sign.SetActive(true);
        yield return new WaitForSeconds(1f);
        sign.SetActive(false);
        state = BossState.Dash;
        StopCoroutine("isDash");

    }
}