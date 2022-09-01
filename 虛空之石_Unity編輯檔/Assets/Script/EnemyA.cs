using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : Enemy
{
    void Update()
    {
        StateMachine();
    }

    public override void StateMachine()
    {
        state = ani.GetCurrentAnimatorStateInfo(0);
        ani.SetBool("Move", mw.moving);
        ani.SetBool("Attack", mw.attacking);
        mw.enableMove = !state.IsTag("attack");
    }

    public override void Damage(float dmg)
    {
        if (!this.enabled) { return; }

        base.Damage(dmg);
        ani.SetTrigger("Hurt");

        if (hp <= 0)
        {
            ani.SetTrigger("Die");
            Destroy(mw);
            this.enabled = false;
        }
    }
}
