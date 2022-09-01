using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterWanderG : MonoBehaviour
{
    public AudioSource attackAudio;
    public AudioSource invincibleAudio;
    public GameObject enemySign;
    public float[] atkPatrolWeight = { 1, 1 };         //攻擊權重

    public float atkRate; //攻擊頻率
    private float nextAtk;

    public float[] patrolWeight = { 3000, 5000, 4000 };         //設置待機時各種動作的權重，順序依次?呼吸、移動、觀察
    public float actRestTme;            //更換待機指令的間隔時間
    private float lastActTime;          //最近一次指令時間
    private float rFTime;
    private Transform originalPosition;
    private Rigidbody2D enemyRGB2D;

    public enum Status { patrol, direction, idle, warn, chase, atk };
    public Status status;

    public enum Face { Right, Left };
    public Face face;

    internal bool enableMove = true;
    internal bool moving = false;
    internal bool attacking = false;

    internal bool invincible = false;
    public bool isInvincible = false;
    public float invincibleTime = 2;
    private float nowTime;
    private float waitTime;

    public float patrolSpeed; //巡邏速度
    public float speed; //移動速度
    private Transform myTransform;  //自己的位子

    public Transform playerTransform; //偵測玩家位子
    private SpriteRenderer spr; //調整方向

    public bool isFlipped = true;

    [Header("視野距離")]
    [Range(0, 10f)]
    public float distance;

    [Header("視野高度")]
    [Range(0, 3f)]
    public float distanceH;

    [Header("偵測玩家位子")]
    public Transform enemydirection;

    [Header("玩家圖層")]
    public LayerMask playerLayer;

    public bool takeDamage = false; //是否被攻擊
    public float noDamageTime = 5; //沒有被傷害的時間
    public float maxNoDamageTime = 5; //沒有被傷害的時間週期

    [Header("牆壁圖層")]
    public LayerMask groundLayer;
    public float jumpHeight = 5;
    public Transform findGroundUpPos;
    public Transform findGroundDownPos;

    [Header("偵測牆壁距離")]
    [Range(0, 1f)]
    public float findGroundDistance;

    private bool isLeft = false;
    private GameObject bullet;
    public Transform findNotGroundPos;

    public bool isAttacking;

    public bool findPlayer;
    bool viewPlayer
    {
        get
        {
            Vector2 start = enemydirection.position;
            Vector2 endT = new Vector2();
            Vector2 endB = new Vector2();
            if (isLeft == false) //false
            {
                endT = new Vector2(start.x + distance, start.y + distanceH);
                endB = new Vector2(start.x + distance, start.y - distanceH);
            }
            else if (isLeft == true)  //true
            {
                endT = new Vector2(start.x - distance, start.y + distanceH);
                endB = new Vector2(start.x - distance, start.y - distanceH);
            }
            Debug.DrawLine(start, endT, Color.red);
            Debug.DrawLine(start, endB, Color.red);
            Debug.DrawLine(endB, endT, Color.red);
            findPlayer = Physics2D.Linecast(start, endT, playerLayer) || Physics2D.Linecast(start, endB, playerLayer) || Physics2D.Linecast(endB, endT, playerLayer);
            return findPlayer;
        }
    }

    public bool isGround;
    bool viewGround
    {

        get
        {

            Vector2 start1 = findGroundUpPos.position;
            Vector2 start2 = findGroundDownPos.position;
            Vector2 end1 = new Vector2();
            Vector2 end2 = new Vector2();

            if (isLeft == false)
            {
                end1 = new Vector2(start1.x + findGroundDistance, start1.y);
                end2 = new Vector2(start2.x + findGroundDistance, start2.y);

            }
            else if (isLeft == true)
            {
                end1 = new Vector2(start1.x - findGroundDistance, start1.y);
                end2 = new Vector2(start2.x - findGroundDistance, start2.y);

            }
            Debug.DrawLine(start1, start2, Color.blue);
            Debug.DrawLine(start1, end1, Color.blue);
            Debug.DrawLine(start2, end2, Color.blue);
            Debug.DrawLine(end1, end2, Color.blue);

            isGround = Physics2D.Linecast(start1, start2, groundLayer) || Physics2D.Linecast(start1, end1, groundLayer) || Physics2D.Linecast(start2, end2, groundLayer) || Physics2D.Linecast(end1, end2, groundLayer);
            return isGround;
        }
    }

    public bool isNotGround;
    bool viewNotGround
    {

        get
        {
            Vector2 start1 = findNotGroundPos.position;
            Vector2 end1 = new Vector2();

            if (isLeft == false)
            {
                end1 = new Vector2(start1.x + 0.2f, start1.y);


            }
            else if (isLeft == true)
            {
                end1 = new Vector2(start1.x - 0.2f, start1.y);


            }
            Debug.DrawLine(start1, end1, Color.blue);


            isNotGround = Physics2D.Linecast(start1, end1, groundLayer);
            return isNotGround;
        }
    }

    void randomFace()
    {
        rFTime = Time.time;
        int rd = Random.Range(0, 2);
        if (rd == 0)
        {
            isLeft = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (rd == 1)
        {
            isLeft = true;
            transform.eulerAngles = new Vector3(0, 180f, 0);
        }
    }

    void RandomAction()
    {
        //更新行動時間
        lastActTime = Time.time;
        //根據權重隨機
        float number = Random.Range(0, patrolWeight[0] + patrolWeight[1] + patrolWeight[2]);
        if (number <= patrolWeight[0])
        {
            status = Status.idle;
        }
        else if (patrolWeight[0] < number && number <= patrolWeight[0] + patrolWeight[1])
        {
            status = Status.patrol;
        }
        if (patrolWeight[0] + patrolWeight[1] < number && number <= patrolWeight[0] + patrolWeight[1] + patrolWeight[2])
        {
            randomFace();
            status = Status.direction;
        }
    }


    void monsterMove()
    {
        float deltaTime = Time.deltaTime;

        switch (status)
        {
            case Status.idle:
                attacking = false;
                invincible = false;
                enemySign.SetActive(true);
                moving = false;

                if (Mathf.Abs(myTransform.position.x - playerTransform.position.x) <= 11f)
                {
                    status = Status.warn;
                }
                if (takeDamage)
                {
                    status = Status.chase;

                }
                if (Time.time - lastActTime > actRestTme)
                {
                    RandomAction();
                }
                break;
            case Status.patrol:
                attacking = false;
                invincible = false;
                moving = true;
                enemySign.SetActive(true);
                if (Mathf.Abs(myTransform.position.x - playerTransform.position.x) <= 11f)
                {
                    status = Status.warn;
                }
                if (!viewNotGround)
                {
                    status = Status.direction;

                }
                if (isLeft == false)
                {
                    myTransform.position += new Vector3(patrolSpeed * deltaTime, 0, 0);
                }
                else if (isLeft == true)
                {
                    myTransform.position -= new Vector3(patrolSpeed * deltaTime, 0, 0);
                }
                if (takeDamage)
                {
                    status = Status.chase;

                }
                if (Time.time - lastActTime > actRestTme)
                {
                    RandomAction();
                }
                break;
            case Status.direction:
                attacking = false;
                invincible = false;
                moving = false;
                enemySign.SetActive(true);
                if (Mathf.Abs(myTransform.position.x - playerTransform.position.x) <= 11f)
                {
                    status = Status.warn;
                }
                if (takeDamage)
                {
                    status = Status.chase;

                }
                if (Time.time - lastActTime > actRestTme)
                {
                    RandomAction();
                }
                break;
            case Status.warn:
                attacking = false;
                invincible = false;
                moving = false;
                enemySign.SetActive(true);
                if (playerTransform)
                {
                    if (Time.time - rFTime > actRestTme)
                    {
                        randomFace();
                    }
                    if (Mathf.Abs(myTransform.position.x - playerTransform.position.x) <= 16f && Mathf.Abs(myTransform.position.x - playerTransform.position.x) >= 3f && viewPlayer)
                    {
                        status = Status.chase;
                    }
                    else if (Mathf.Abs(myTransform.position.x - playerTransform.position.x) > 17f)
                    {
                        status = Status.idle;
                    }
                    if (takeDamage)
                    {
                        status = Status.chase;

                    }
                }
                break;
            case Status.chase:
                attacking = false;
                invincible = false;
                moving = true;
                Wait();
                enemySign.SetActive(false);
                if (playerTransform)
                {
                    if (viewGround && !attacking && !isInvincible) //jump
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
                    }
                    if (Mathf.Abs(myTransform.position.x - playerTransform.position.x) <= 11.5f)
                    {
                        status = Status.atk;

                    }
                    if (myTransform.position.x >= playerTransform.position.x && invincible == false)
                    {
                        isLeft = true;
                        transform.eulerAngles = new Vector3(0, 180f, 0);
                        face = Face.Left;


                    }
                    else if (myTransform.position.x < playerTransform.position.x && invincible == false)
                    {
                        isLeft = false;
                        transform.eulerAngles = new Vector3(0, 0f, 0);
                        face = Face.Right;



                    }
                }

                switch (face)
                {
                    case Face.Left:
                        if (enableMove == false && invincible == false)
                        {
                            enemyRGB2D.velocity = new Vector2(0, enemyRGB2D.velocity.y);
                        }
                        else
                            enemyRGB2D.velocity = new Vector2(-1 * speed * Time.deltaTime, enemyRGB2D.velocity.y);
                        break;
                    case Face.Right:
                        if (enableMove == false && invincible == false)
                        {
                            enemyRGB2D.velocity = new Vector2(0, enemyRGB2D.velocity.y);
                        }
                        else
                            enemyRGB2D.velocity = new Vector2(1 * speed * Time.deltaTime, enemyRGB2D.velocity.y);
                        break;
                }

                noDamageTime = noDamageTime - Time.deltaTime; //沒攻擊敵人時間

                if (playerTransform && noDamageTime == 0)
                {
                    if (Mathf.Abs(myTransform.position.x - playerTransform.position.x) >= 15f)
                    {
                        status = Status.warn;
                        takeDamage = false;
                    }
                }
                break;

            case Status.atk:
                moving = false;
                enemySign.SetActive(false);
                if (Time.time > nextAtk) //攻擊等待時間判斷
                {
                    float number;


                    if (Mathf.Abs(myTransform.position.x - playerTransform.position.x) <= 3f)
                        number = Random.Range(0, atkPatrolWeight[0] + atkPatrolWeight[1]); 
                    else
                        number = Random.Range(0, atkPatrolWeight[0]) ;


                   if (number <= atkPatrolWeight[0] && bullet == null)  //攻擊1
                    {
                        nextAtk = Time.time + atkRate;
                        invincible = false;
                        attacking = true;
                        //Debug.Log("attack1");

                        status = Status.atk;

                    }
                    else if(atkPatrolWeight[0] < number && Mathf.Abs(myTransform.position.x - playerTransform.position.x) <= 1.5f)//無敵
                    {
                        nextAtk = Time.time + atkRate;
                        attacking = false;
                        invincible = true;
                        //Debug.Log("invincible");

                        status = Status.atk;

                    }

                    /*if (atkPatrolWeight[0] < number)  //無敵
                    {
                        nextAtk = Time.time + atkRate;
                        nowTime = Time.time;
                        waitTime = invincibleTime + nowTime;
                        while (waitTime >= nowTime)
                        {
                            invincible = true;
                            nowTime = Time.time;
                        }
                        status = Status.atk;

                    }*/
                   

                }
                else
                {
                    attacking = false;
                    invincible = false;
                   
                }

                enemyRGB2D.velocity = new Vector2(0f, enemyRGB2D.velocity.y);

                if (myTransform.position.x >= playerTransform.position.x && invincible == false && !isAttacking) 
                {
                    transform.eulerAngles = new Vector3(0, 180f, 0);
                    face = Face.Left;
                }
                else if(myTransform.position.x < playerTransform.position.x && invincible == false && !isAttacking)
                {
                    transform.eulerAngles = new Vector3(0, 0f, 0 );
                    face = Face.Right;
                }
                if (Mathf.Abs(myTransform.position.x - playerTransform.position.x) > 12f)
                {

                    status = Status.chase;
                    //Debug.Log("CCC");

                }
                else
                {
                    status = Status.atk;
                }


                break;
        }
    }

    private void Awake()
    {
        enemyRGB2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        originalPosition = myTransform;
        status = Status.idle;
        spr = this.transform.GetComponent<SpriteRenderer>();
        if (isLeft)
        {
            face = Face.Left;
   
        }
        else
        {
            face = Face.Right;
         
        }
        myTransform = this.transform;
        playerTransform = GameObject.Find("player").transform;
        RandomAction();

        nextAtk = atkRate;


    }

    void FixedUpdate()
    {
        bullet = GameObject.Find("GolemBullet(Clone)");
        monsterMove();
    }

    //public void nextAttack()
    //{
        //nextAtk = Time.time + atkRate;
    //}
    void AttackAudio()
    {
        attackAudio.Play();
    }
    void InvincibleAudio()
    {
        invincibleAudio.Play();
    }

    public void GiveEnumNum(int i)
    {
        status = (Status)i;
        //Debug.Log(status);
    }

    public void Wait()
    {
        if (Time.time <= nextAtk)
        {
            moving = false;
            enableMove = false;
        }
    }

    public void IsAttacking()
    {
        isAttacking = true;
    }

    public void IsNotAttacking()
    {
        isAttacking = false;
    }

    public void IsInvincible()
    {
        isInvincible = true;
    }

    public void IsNotInvincible()
    {
        isInvincible = false;
    }


}

















