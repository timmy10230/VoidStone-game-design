using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : StateMachineBehaviour
{
    new void OnStateUpdate(Animator anim, AnimatorStateInfo info, int id)
    {
        Game.sav.RegenSP(Time.deltaTime);
    }
}
