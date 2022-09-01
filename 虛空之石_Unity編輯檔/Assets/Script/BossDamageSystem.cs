using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageSystem : MonoBehaviour
{
    public Animator ani;
    public Battle battle;
    public void PlayerTakeDamage(float damage) //玩家受傷
    {
        //Game.sav.hp -= damage;
        //ani.SetTrigger("Damage");  //角色受傷動畫

        if (battle.isDefence)
        {
            Game.sav.reduceDf(damage);
        }
        else if (!battle.state.IsTag("stealth") && !battle.isDefence)
        {
            if (battle.state.IsName("Base.damage")) { return; }
            battle.ani.SetTrigger("Damage");
            Game.sav.Damage(damage);
        }
        /*if (health <= 0)  //死亡
        {
            Die();
        }*/
    }
}
