using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : StateMachineBehaviour
{
    private Transform playerPos;
    private Rigidbody2D rb;
    public EnemyBoss boss;
    public float attackRange1_1 = 3f;
    public float attackRange1_2 = 3.2f;
    public float attackRange1_3 = 3.15f;
    public float speed = 4.5f;

    private int rand; //攻擊隨機亂數

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform; //玩家位置
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<EnemyBoss>();

        rand = Random.Range(0,10);
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPos.position, speed * Time.deltaTime);
        boss.LookAtPlayer();  //面向玩家

        

        //找到玩家位置 並移動
        Vector2 target = new Vector2(playerPos.position.x, rb.position.y);       
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        //攻擊
        if (Vector2.Distance(playerPos.position, rb.position) <= attackRange1_1 && rand <= 3)
        {
            animator.SetTrigger("Attack1"); //啟動攻擊動畫
            
        }
        else if (Vector2.Distance(playerPos.position, rb.position) <= attackRange1_2 && rand <= 6 && rand >= 4)
        {
            animator.SetTrigger("Attack2"); //啟動攻擊動畫
        }
        else if (Vector2.Distance(playerPos.position, rb.position) <= attackRange1_3 && rand <= 10 && rand >= 7)
        {
            animator.SetTrigger("Attack3"); //啟動攻擊動畫
        }


    }

   
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack1");  //關閉攻擊動畫
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack3");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
