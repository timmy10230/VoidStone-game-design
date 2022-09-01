using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterWander : MonoBehaviour {

    public float[] patrolWeight = { 3000, 3000, 4000 };         //設置待機時各種動作的權重，順序依次爲呼吸、移動、觀察
    public float actRestTme;            //更換待機指令的間隔時間
    private float lastActTime;          //最近一次指令時間
    private float rFTime;
    private Transform originalPosition;
    private Rigidbody2D enemyRGB2D;

    public enum Status { patrol,direction,idle, warn, chase, atk };
    public Status status;

    public enum Face { Right,Left};
    public Face face;

    internal bool enableMove = true;
    internal bool moving = false;
    internal bool attacking = false;

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
    [Range(0, 1f)]
    public float distanceH;

    [Header("偵測玩家位子")]
    public Transform enemydirection;

    [Header("玩家圖層")]
    public LayerMask playerLayer;

    public bool takeDamage = false; //是否被攻擊
    public float noDamageTime = 5; //沒有被傷害的時間
    public float maxNoDamageTime = 5; //沒有被傷害的時間週期

    public bool findPlayer;
    bool viewPlayer
    {
        get
        {
            Vector2 start = enemydirection.position;
            Vector2 endT = new Vector2();
            Vector2 endB = new Vector2();
            if (spr.flipX == false)
            {
                endT = new Vector2(start.x + distance, start.y + distanceH);
                endB = new Vector2(start.x + distance, start.y - distanceH);
            }
            else if (spr.flipX == true)
            {
                endT = new Vector2(start.x - distance, start.y + distanceH);
                endB = new Vector2(start.x - distance, start.y - distanceH);
            }
            Debug.DrawLine(start, endT, Color.red);
            Debug.DrawLine(start, endB, Color.red);
            Debug.DrawLine(endB, endT, Color.red);
            findPlayer = Physics2D.Linecast(start, endT, playerLayer)|| Physics2D.Linecast(start, endB, playerLayer)|| Physics2D.Linecast(endB, endT, playerLayer);
            return findPlayer;
        }
    }

    void randomFace()
    {
        rFTime = Time.time;
        int rd = Random.Range(0, 2);
        if (rd == 0)
        {
            spr.flipX = false;
        }
        else if (rd == 1)
        {
            spr.flipX = true;
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
                moving = false;
                if (Mathf.Abs(myTransform.position.x - playerTransform.position.x) <= 17f)
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
                moving = true;
                if (Mathf.Abs(myTransform.position.x - playerTransform.position.x) <= 17f)
                {
                    status = Status.warn;
                }
                if (spr.flipX == false)
                {
                    myTransform.position += new Vector3(patrolSpeed * deltaTime, 0, 0);
                }
                else if(spr.flipX == true)
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
                moving = false;
                if (Mathf.Abs(myTransform.position.x - playerTransform.position.x) <= 17f)
                {
                    status = Status.warn;
                }
                if (takeDamage )
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
                moving = false;
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
                    else if(Mathf.Abs(myTransform.position.x - playerTransform.position.x) > 17f)
                    {
                        status = Status.idle;
                    }
                    if (takeDamage )
                    {
                        status = Status.chase;
                        
                    }
                }
                break;
            case Status.chase:
                attacking = false;
                moving = true;
                if (playerTransform)
                {
                    if (Mathf.Abs(myTransform.position.x - playerTransform.position.x) <= 2f)
                    {
                        status = Status.atk;
                        
                    }
                    if (myTransform.position.x >= playerTransform.position.x)
                    {
                        spr.flipX = true;
                        //transform.eulerAngles = new Vector3(0, 180f, 0);
                        face = Face.Left;
                        
                        
                    }
                    else if(myTransform.position.x < playerTransform.position.x)
                    {      
                        spr.flipX = false;
                        //transform.eulerAngles = new Vector3(0, 0f, 0);
                        face = Face.Right;
                        
                        
                        
                    }
                }        

                switch (face)
                {
                    case Face.Left:
                        if (enableMove == false)
                        {
                            enemyRGB2D.velocity = new Vector2(0, enemyRGB2D.velocity.y);
                        }
                        else
                            enemyRGB2D.velocity = new Vector2(-1*speed * Time.deltaTime, enemyRGB2D.velocity.y);
                        break;
                    case Face.Right:
                        if (enableMove == false)
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
                    if (Mathf.Abs(myTransform.position.x - playerTransform.position.x) >= 10f  )
                    {
                        status = Status.warn;
                        takeDamage = false;
                    }
                }
                break;
            case Status.atk:
                attacking = true;
                moving = false;
                
                enemyRGB2D.velocity = new Vector2(0f, enemyRGB2D.velocity.y);
                if (myTransform.position.x >= playerTransform.position.x)
                {
                    transform.eulerAngles = new Vector3(0, 180f, 0);
                    face = Face.Left;
                }
                else 
                {
                    transform.eulerAngles = new Vector3(0, 0f, 0);
                    face = Face.Right;
                }
                if (Mathf.Abs(myTransform.position.x - playerTransform.position.x) >= 3f)
                {
                    status = Status.chase;
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
        if (spr.flipX)
        {
            face = Face.Left;
        }
        else
        {
            face = Face.Right;
        }
        myTransform = this.transform;
        playerTransform = GameObject.FindWithTag("Player").transform;
        RandomAction();
    }

    void FixedUpdate()
    {
        monsterMove();
    }

    



}