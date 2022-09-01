using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 10;

    internal Animator ani;
    internal MonsterWander mw;
    internal AnimatorStateInfo state;
    


    void Awake()
    {
        mw = GetComponent<MonsterWander>();
        ani = GetComponent<Animator>();
    }

    public virtual void StateMachine() { }

    public virtual void Damage(float dmg)
    {
        hp -= dmg;
        mw.takeDamage = true;
        mw.noDamageTime = mw.maxNoDamageTime;
              
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
