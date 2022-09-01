using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    public float hp = 1;
    public float speed = 3;
    public float hitAfterSpeedHorizontal;
    public float hitAfterSpeedHorVertical;
    public float dmg = 0.5f;


    public float startWaitTime = 0.5f;
    public float waitTime;

    public float attackStartWaitTime = 0.5f;
    public float attackWaitTime;

    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightUpPos;


   

    public float radius = 30; //偵測玩家半徑

    private Transform playerTransform;

    //private Transform rayTransform;

    public Animator ani;
    //public Battle battle;

    public float attackDamage = 1;
    public float attackRange = 0.3f;

    public Vector3 moveTransformPos;

    public Rigidbody2D rb;


    private bool chace = false;

    public float damgeRectTime = 3;
    private float nextDamage;

    private float warnChase;
    private bool isFlipped = false;
    private bool isHit = false;
    private bool isDead = false;

    public GameObject enemySign;

    public GameObject player;





    [Header("牆壁圖層")]
    public LayerMask groundLayer;
    public Transform findGround;

    public bool isGround;
    bool hitGround
    {

        get
        {

            Vector2 start = findGround.position;


            Debug.DrawLine(start, start, Color.blue);

            isGround = Physics2D.Linecast(start, start, groundLayer);
            return isGround;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        ani.SetBool("fly", true);
        waitTime = startWaitTime;
        movePos.position = GetRandomPos();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        nextDamage = damgeRectTime;

        //Ray ray = new Ray(transform.position, transform.forward * 100); //發射位置 發射放向*速度

        warnChase = radius + 100;


    }

    // Update is called once per frame
    void Update()
    {


        if (playerTransform != null) //存在玩家
        {
            //StartCoroutine("Chase");
            //speed = 3;

            float distance = (transform.position - playerTransform.position).sqrMagnitude; //計算怪物與玩家距離
            Vector3 playerTransformPos = new Vector3(playerTransform.position.x, playerTransform.position.y + 1.5f, 0); //飛向玩家座標修正

            if (distance <= radius) //玩家靠近
            {
                chace = true;
                //radius + 50;
                //Debug.Log("chase");
            }
            else if (distance > warnChase)
            {
                chace = false;
            }


            if(chace)
            {
                if (!isDead)
                {
                    enemySign.SetActive(false);
                    transform.position = Vector2.MoveTowards(transform.position, playerTransformPos, speed * Time.deltaTime); //飛向玩家
                    LookAtPlayer();


                    Vector2 rayTransform = new Vector2(transform.position.x -0.5f , transform.position.y);
                    LayerMask mask = LayerMask.GetMask("Player");
                    Ray2D ray = new Ray2D(rayTransform, Vector2.right);
                    RaycastHit2D hitInfo = Physics2D.Raycast(ray.origin, Vector2.right, 1f , mask);  //座標，方向，距離
                    Debug.DrawRay(ray.origin, ray.direction, Color.green);  
         
                    //print(hitInfo.collider.tag);     
                    if (hitInfo.collider.tag == "Player")
                    {
                        StartCoroutine("Stop");
                        //StopCoroutine("Stop");
                        //Debug.Log("RayHit");
                    }
                    else 
                    {
                        return;
                    }
                }                       
                
            }
            else  //隨機在區域內飛行
            {
                enemySign.SetActive(true);
                Vector3 flipped = transform.localScale;
                flipped.z *= -1f;
                //轉向
                if (transform.position.x > movePos.position.x && isFlipped)
                {
                    transform.localScale = flipped;
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    //transform.Rotate(0f, 180f, 0f);
                    isFlipped = false;
                }
                else if (transform.position.x < movePos.position.x && !isFlipped)
                {
                    transform.localScale = flipped;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    //transform.Rotate(0f, 180f, 0f);
                    isFlipped = true ;
                }

                transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, movePos.position) < 0.1f)   //是否到達位置
                {
                    if (waitTime <= 0)  //等待
                    {
                        movePos.position = GetRandomPos();
                        waitTime = startWaitTime;
                    }
                    else
                    {
                        waitTime -= Time.deltaTime;
                    }
                }
            }

            //StartCoroutine("Stop");
            //StartCoroutine("Stop");
        }


    }

    Vector2 GetRandomPos()
    {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
        return rndPos;
    }

    public void TakeDamage(float damage) //怪物受傷
    {
        StartCoroutine("Hit");
        if (hp > 0)
        {
            hp -= damage;
            //ani.SetTrigger("Hurt");
        }

        if (hp <= 0) //HP低於0死亡
        {
            isDead = true;
            speed = 0;
            //hitAfterSpeed = 0;
            chace = false;
            isHit = false;

            if (transform.position.x < playerTransform.position.x  )
            {
                rb.velocity = transform.right * -5;
            }
            else  if (transform.position.x > playerTransform.position.x )
            {
                rb.velocity = transform.right * 5;
            }
            

            ani.SetBool("fly", false);
            ani.SetTrigger("Die");

        }

    }



    void Destory()
    {
        Destroy(gameObject, 0.2f);
    }

    void Die2Trigger()
    {
        ani.SetTrigger("Die2");
    }

    void HitGround()
    {
        if (isGround)
        {
            ani.SetTrigger("HitGround");
        }
    }

    public void LookAtPlayer() //面相玩家
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > playerTransform.position.x && isFlipped)
        {


            transform.localScale = flipped;
            transform.eulerAngles = new Vector3(0, 180, 0);

            isFlipped = false;
        }
        else if (transform.position.x < playerTransform.position.x && !isFlipped)
        {
  

            transform.localScale = flipped;
            transform.eulerAngles = new Vector3(0, 0, 0);
 
            isFlipped = true;
        }
    }

    public void batIsHit()
    {
        //battle.isHit(); 
        player.GetComponent<Battle>().isHit();


    }

    public void overHit()
    {
        //battle.overHit();
        player.GetComponent<Battle>().overHit();
    }

    IEnumerator Hit()
    {
        batIsHit();

        yield return new WaitForSeconds(0.5f);
        overHit();
        StopCoroutine("Hit");

    }



    IEnumerator Stop()
    {
        
        speed = 0;
        //hitAfterSpeed = 2;
        isHit = true;

        hitAfterSpeedHorizontal = Random.Range(1.5f,4);
        hitAfterSpeedHorVertical = Random.Range(1.5f,4); 
     
        //反向飛 Filpped相反
        if (transform.position.x < playerTransform.position.x )
        {
            hitAfterSpeedHorizontal = hitAfterSpeedHorizontal * -1;
            rb.velocity = new Vector2(hitAfterSpeedHorizontal, hitAfterSpeedHorVertical);
            //Debug.Log("left");
      
        }
        else //if (transform.position.x <= playerTransform.position.x )
        {
            rb.velocity = new Vector2(hitAfterSpeedHorizontal, hitAfterSpeedHorVertical);
            //Debug.Log("right");

        }  


            yield return new WaitForSeconds(1.5f);

        //Debug.Log("IEnumerator");
        isHit = false;
  
        //hitAfterSpeed = 0;
        rb.velocity = new Vector2(0, 0);
        speed = 3;
        StopCoroutine("Stop");
        //Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" && isDead)
        {
            isGround = true;
            //Destory();
        }
    }


}

