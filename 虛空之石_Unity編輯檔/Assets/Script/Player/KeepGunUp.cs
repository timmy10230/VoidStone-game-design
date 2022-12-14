using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepGunUp : StateMachineBehaviour
{

    //public Transform firePoint;
    //public GameObject bulletPrefab;
    public Gun gun;
   

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gun = animator.GetComponent<Gun>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("TakeGunUp", true);
        }
        else
        {
            animator.SetBool("TakeGunUp", false);
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            animator.SetBool("TakeGun", false);
        }

        if (Input.GetButtonDown("Fire1"))//讓子彈發射有間隔
        {
            if (Time.time > gun.nextFire)
            {
                //nextFire = Time.time + fireRate;//Time.time表示從遊戲開發到現在的時間，會隨着遊戲的暫停而停止計算。
                animator.SetTrigger("Shot");
                gun.Shoot2();
            }
        }



    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
