using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class playerControl : MonoBehaviour
{
    public static playerControl pc;
    public void Awake()
    {
        Game.sav.hp = Game.sav.maxHp;
        Game.sav.df = Game.sav.maxDf;
        Game.sav.sp = Game.sav.maxSp;
        pc = this;
        Physics2D.IgnoreLayerCollision(9,10);
        Physics2D.IgnoreLayerCollision(10, 10);
        playerRGB2D = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        
    }

    internal Rigidbody2D playerRGB2D;
    internal Animator ani;
    internal AnimatorStateInfo state;
    internal bool lockDash = false;
    internal bool faceR = true;
    internal bool isCrouch=false;
    internal bool isDefence = false;
    internal bool stealth = false;
    public bool canUseChangeSpace = true;
    internal float dashCD = 0.4f;
    internal float nextDash = 0f;
    internal float nowDashTime = 0f;


    public float speedX = 180f;
    public float runSpeed = 3f;
    public float dashSpeed = 4.5f;
    public PhysicsMaterial2D jumpMa; //跳躍時摩擦力設定

    public AudioSource attack1Audio;
    public AudioSource attack2Audio;   
    public AudioSource playerHitAudio;
    public AudioSource jumpAudio;
    public AudioSource dashAudio;
    public AudioSource shieldAudio;
    public AudioSource bulletAudio;
    public GameObject enemyBeHitAudio;
    //public GameObject EnemyHitAudio;

    public bool isHitEnemy = false;
    //public AudioSource attack1Audio;

    [Header("垂直向上推力")]
    public float yForce;

    [Header("感應地板的距離")]
    [Range(0, 0.5f)]
    public float distance;

    [Header("偵測地板的左射線起點")]
    public Transform groundCheckL;

    [Header("偵測地板的右射線起點")]
    public Transform groundCheckR;

    [Header("地面圖層")]
    public LayerMask groundLayer;

    [Header("感應上方的距離")]
    [Range(0, 0.5f)]
    public float Cdistance;

    [Header("偵測左上方的射線起點")]
    public Transform ceilingCheckL;

    [Header("偵測右上方的射線起點")]
    public Transform ceilingCheckR;

    [Header("地面圖層")]
    public LayerMask CgroundLayer;

    public bool Cgrounded;
    public bool canCrouch;
    public bool canDash;
    public bool canMove = false;

    //public AudioSource backgroundMusic1;
    //public AudioSource backgroundMusic2;
    //public BackgroundMusic bgm;
    public Animator Music1Ani;
    public Animator Music2Ani;


    /*
    ---------設定鍵值---------
    */
    public bool JumpKey
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }

    public bool KUKey
    {
        get
        {
            if (!CIsGroundL && !CIsGroundR)
            {
                return Input.GetKeyUp(KeyCode.LeftControl);
            }
            else
                return false;

        }
    }

    public bool LKey
    {
        get
        {
            return Input.GetKey(KeyCode.LeftArrow);
        }
    }

    public bool RKey
    {
        get
        {
            return Input.GetKey(KeyCode.RightArrow);
        }
    }
    //----------------------

    /*
    ---------跳躍---------
    */
    public virtual void TryJump()
    {

        if (IsGrounded && (!CIsGroundL && !CIsGroundR) && Input.GetKey(KeyCode.Space))
        {
            playerRGB2D.AddForce(Vector2.up * yForce);
            ani.SetTrigger("Jump");
            JumpAudio();           
        }
    }
    //----------------------

    /*
    ---------蹲下---------
    */
    void TryCrouch()
    {
        if (((!CIsGroundR && CIsGroundL) || (CIsGroundR && !CIsGroundL) || (!CIsGroundR && !CIsGroundL)) && (IsGroundL || IsGroundR) && Input.GetKeyDown(KeyCode.LeftControl))
        {
            canCrouch = true;
            speedX = 150f;
        }

        if ((!IsGroundL && IsGroundR) || (IsGroundL && !IsGroundR) || KUKey)
        {
            canCrouch = false;
            speedX = 180f;
        }

        else if ((!CIsGroundR && !CIsGroundL) && (LKey || RKey))
        {
            canCrouch = false;
            speedX = 180f;
        }
    }
    //----------------------

    /*
    ---------判斷是否在地面---------
    */
    public bool groundedL;
    bool IsGroundL
    {
        get
        {
            Vector2 start = groundCheckL.position;
            Vector2 end = new Vector2(start.x, start.y - distance);

            Debug.DrawLine(start, end, Color.yellow);
            groundedL = Physics2D.Linecast(start, end, groundLayer);
            return groundedL;
        }
    }
    
    public bool groundedR;
    bool IsGroundR
    {
        get
        {
            
            Vector2 start = groundCheckR.position;
            Vector2 end = new Vector2(start.x, start.y - distance);

            Debug.DrawLine(start, end, Color.yellow);
            groundedR = Physics2D.Linecast(start, end, groundLayer);
            return groundedR;
        }
    }

    public bool IsGrounded
    {
        get
        {
            if (IsGroundR || IsGroundL)
            {
                playerRGB2D.sharedMaterial = null;
                return true;
            }
            else
            {
                playerRGB2D.sharedMaterial = jumpMa;
                return false;
            }
        }
    }


    //----------------------

    /*
    ---------判斷上方圖層是不是地面---------
    */
    bool CIsGroundL
    {
        get
        {
            Vector2 Cstart = ceilingCheckL.position;
            Vector2 Cend = new Vector2(Cstart.x, Cstart.y + Cdistance);

            Debug.DrawLine(Cstart, Cend, Color.yellow);
            Cgrounded = Physics2D.Linecast(Cstart, Cend, CgroundLayer);
            return Cgrounded;
        }
    }

    bool CIsGroundR
    {
        get
        {
            Vector2 Cstart = ceilingCheckR.position;
            Vector2 Cend = new Vector2(Cstart.x, Cstart.y + Cdistance);

            Debug.DrawLine(Cstart, Cend, Color.yellow);
            Cgrounded = Physics2D.Linecast(Cstart, Cend, CgroundLayer);
            return Cgrounded;
        }
    }
    //----------------------

    /*
    ---------移動,轉向---------
    */
    public virtual void MovementX()
    {
        bool upKey = Input.GetKeyUp(KeyCode.RightArrow) ||Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.LeftShift);
        if (Input.GetKey(KeyCode.RightArrow)&& Input.GetKey(KeyCode.LeftShift))
        {
            Move(2);
            Direction(0);
            faceR = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(1);
            Direction(0);
            faceR = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftShift))
        {
            Move(-2);
            Direction(1);
            faceR = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(-1);
            Direction(1);
            faceR = false;
        }
        if (upKey)
        {
            Move(0);
        }
    }

    public virtual void Move(int i)
    {       
        if (Mathf.Abs(i)==1)
        {
            playerRGB2D.velocity = new Vector2(i * speedX * Time.deltaTime, playerRGB2D.velocity.y);
        }
        else if (Mathf.Abs(i) == 2)
        {
            playerRGB2D.velocity = new Vector2(i * runSpeed * Time.deltaTime, playerRGB2D.velocity.y);
        }
        else if (Mathf.Abs(i) == 0)
        {
            playerRGB2D.velocity = new Vector2(0f, playerRGB2D.velocity.y);
        }
    }

    public virtual void Direction(int i)
    {
        transform.eulerAngles = new Vector3(0, 180f * i, 0);
    }

    //----------------------

    /*
    ---------衝刺---------
    */
    public virtual void dash()
    {
        if (faceR == true && !isCrouch&& Input.GetKey(KeyCode.W) && Time.time > nextDash && Game.sav.CanUseSP())
        {
            DashAudio();
            dashCostSP();
            
            stealth = true;
            Invoke("EndStealthTime",0.7f);
            lockDash = true;
            nowDashTime = Time.time;
            canDash = true;
            playerRGB2D.velocity = new Vector2(dashSpeed * speedX * Time.deltaTime, playerRGB2D.velocity.y);
            nextDash = Time.time + dashCD;
            
        }
        else if (faceR == false && !isCrouch && Input.GetKey(KeyCode.W) && Time.time > nextDash && Game.sav.CanUseSP())
        {
            DashAudio();
            dashCostSP();
            
            stealth = true;
            Invoke("EndStealthTime", 0.7f);
            lockDash = true;
            nowDashTime = Time.time;
            canDash = true;
            playerRGB2D.velocity = new Vector2(dashSpeed * -speedX * Time.deltaTime, playerRGB2D.velocity.y);
            nextDash = Time.time + dashCD;
            
        }
        if (Time.time >= nowDashTime+0.6f)
        {
            lockDash = false;
            canDash = false;
        }
    }

    public void EndStealthTime()
    {
        stealth = false;
    }
    //----------------------

    /*
    ---------防禦---------
    */
    public virtual void defence()
    {
        if (Input.GetKey(KeyCode.D) && !isCrouch && groundedL && groundedR && Game.sav.df != 0)
        {
            isDefence = true;
            
        }
        else 
        {
            isDefence = false;
            
        }
        
    }
    //----------------------

    /*
    ---------動畫---------
    */
    public virtual void MoveAni()
    {
        bool upKey = Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.LeftShift);
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift))
        {
            ani.SetFloat("Move",2);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            ani.SetFloat("Move",1);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftShift))
        {
            ani.SetFloat("Move", 2);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            ani.SetFloat("Move", 1);
        }
        if (upKey)
        {
            ani.SetFloat("Move", 0);
        }
    }

    public void setAni()
    {
        ani.SetBool("Ground", IsGrounded);
        ani.SetFloat("SpeedY", playerRGB2D.velocity.y);
        if(canCrouch == true){
            isCrouch = true;
            ani.SetBool("Crouch", true);
        }
        else if (canCrouch == false){
            isCrouch = false;
            ani.SetBool("Crouch", false);
        }
        if (canDash == true)
        {                        
            ani.SetBool("Dash", true);
        }
        else if (canDash == false)
        {
            ani.SetBool("Dash", false);
        }
        if (isDefence)
        {
            ani.SetBool("Defence", true);
        }
        else if (!isDefence)
        {
            ani.SetBool("Defence", false);
        }
        state = ani.GetCurrentAnimatorStateInfo(0);
        if (IsGrounded && (!CIsGroundL && !CIsGroundR) && Input.GetKey(KeyCode.Space))
        {
            ani.SetTrigger("Jump");
        }
    }
    //----------------------

    /*
    ---------SP花費---------
    */
    public void melee1CostSP()
    {
        Game.sav.CostSP(Game.sav.melee1Cost);
    }

    public void melee2CostSP()
    {
        Game.sav.CostSP(Game.sav.melee2Cost);
    }

    public void dashCostSP()
    {
        Game.sav.CostSP(Game.sav.dashCost);
    }
    //----------------------

    /*
    ---------死亡---------
    */
    public void Die()
    {
        if(Game.sav.hp == 0)
        {
            canMove = false;
            ani.SetTrigger("Die");
        }
    }

    public void PlayerHPZero()
    {
        if (Game.sav.hp == 0)
        {
            GameManager._instance.playerUI.SetActive(false);
            GameManager._instance.lose.SetActive(true);
            Time.timeScale = 0;
        }
        else return;
    }

    //-------------------

    /*
    ---------倒地,起身---------
    */
    public void Done()
    {
        canMove = false;
    }

    public void Wake()
    {
        canMove = true;
    }
    //-------------------

    /*
    ---------空間變換---------
    */
    public void SpaceChange()
    {       
        if (Input.GetKeyDown(KeyCode.Q)&&canUseChangeSpace)
        {
            if (GameManager._instance.spaceA.activeInHierarchy == true)
            {
                GameManager._instance.SpaceAToB();
                GameManager._instance.spaceAStoneDispearAni.SetTrigger("StoneDisappear");

                Music1Ani.SetTrigger("End");
                Music2Ani.SetTrigger("Start");
                
                canUseChangeSpace = false;
                Invoke("CanUseChangeSpace", 1.5f);
            }
            else if (GameManager._instance.spaceB.activeInHierarchy == true)
            {
                GameManager._instance.SpaceBToA();
                GameManager._instance.spaceBStoneDispearAni.SetTrigger("StoneDisappear");
                Music1Ani.SetTrigger("Start");
                Music2Ani.SetTrigger("End");
                canUseChangeSpace = false;
                SpaceBPlantChange._instance.SpaceChange();
                Invoke("CanUseChangeSpace", 1.5f);
            }
        }
    }

    public void CanUseChangeSpace()
    {
        canUseChangeSpace = true;
    }
    //-------------------

    private void FixedUpdate()
    {
            setAni();
        if(canMove == true)
        {
            MovementX();
            TryJump();
            dash();
        }

        
    }


    void Update()
    {
            MoveAni();
        if(canMove == true)
        {
            SpaceChange();
            TryCrouch();
            defence();
            Die();
        }
        if(Music1Ani == null)
        {
            Music1Ani = GameObject.Find("Music").GetComponent<Animator>();
        }
    }

    public void Attack1Audio()
    {
        //EnemyHitAudio = GameObject.Find("EnemyBeHitAudio(Clone)");
        //Debug.Log(EnemyHitAudio);
        //Debug.Log(isHitEnemy);
        if(isHitEnemy == true)
        {
            Instantiate(enemyBeHitAudio, Vector2.zero, Quaternion.identity);
            
        }
        else
        {
            attack1Audio.Play();
        }
        //EnemyHitAudio = null;

    }

    public void Attack2Audio()
    {
        //Debug.Log(isHitEnemy);
        //EnemyHitAudio = GameObject.Find("EnemyBeHitAudio(Clone)");
        if (isHitEnemy == true)
        {
            Instantiate(enemyBeHitAudio, Vector2.zero, Quaternion.identity);
            
        }
        else
        {
            attack2Audio.Play();
        }

        //EnemyHitAudio = null;
    }
    
    public void PlayerHitAudio()
    {
       playerHitAudio.Play(); 
    }

    public void JumpAudio()
    {
        jumpAudio.Play();
    }

    public void DashAudio()
    {
        
        dashAudio.Play();
    }

    public void ShieldAudio()
    {        
        shieldAudio.Play();
    }
    public void BulletAudio()
    {
        bulletAudio.Play();
        
    }

    public void isHit()
    {
        isHitEnemy = true;
    }

    public void overHit()
    {
        isHitEnemy = false;
    }

    
}
