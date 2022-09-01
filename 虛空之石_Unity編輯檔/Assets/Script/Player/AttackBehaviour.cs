using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    public float transhold = 1.0f;
    public int order = 0;

    new void OnStateUpdate(Animator ani, AnimatorStateInfo info, int id)
    {    
        
        if (info.normalizedTime <= transhold)
        {
            ani.SetInteger("Attack", order);
            
        }
    }

    new void OnStateExit(Animator ani, AnimatorStateInfo info, int id)
    {
        ani.SetInteger("Attack", 0);
    } 
}
