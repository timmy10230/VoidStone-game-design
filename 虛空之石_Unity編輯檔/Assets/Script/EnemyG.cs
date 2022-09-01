using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyG : Enemy
{

    internal MonsterWanderG mwG;
    public GameObject player;

    public GameObject redPotion;
    public GameObject bluePotion;
    public float[] ItemWeight = { 5, 4, 3 };
    public float number = 0f;
    //public int number2 = 0;
    void Awake()
    {
        mwG = GetComponent<MonsterWanderG>();
        ani = GetComponent<Animator>();
        player = GameObject.Find("player");
    }

    void Update()
    {
        StateMachine();
    }

    public override void StateMachine()
    {
        state = ani.GetCurrentAnimatorStateInfo(0);
        ani.SetBool("Move", mwG.moving);
        ani.SetBool("Attack", mwG.attacking);
        ani.SetBool("Invincible", mwG.invincible);
        mwG.enableMove = !state.IsTag("attack");

    }

    public override void Damage(float dmg)
    {
        if(gameObject.GetComponent<MonsterWanderG>().isInvincible == false)
        {
            hp -= dmg;
            mwG.takeDamage = true;
            mwG.noDamageTime = mwG.maxNoDamageTime;
            if (!this.enabled) { return; }
            ani.SetTrigger("Hurt");
        }

        if (hp <= 0)
        {
            ani.SetTrigger("Die");
            Destroy(mwG);
            this.enabled = false;
        }
        StartCoroutine("Hit");
    }
    public void isHit()
    {
        //battle.isHit(); 
        player.GetComponent<Battle>().isHit();


    }

    public void overHit()
    {
        //battle.overHit();
        player.GetComponent<Battle>().overHit();
    }

    IEnumerator Hit()
    {
        isHit();
        yield return new WaitForSeconds(0.3f);
        overHit();
        StopCoroutine("Hit");

    }

    public void CreatePotion()
    {
        number = Random.Range(0, ItemWeight[0] + ItemWeight[1] + ItemWeight[2]);
        //number2 = Random.Range(1, 3);
        if (number <= ItemWeight[0])
        {
            //for(int i = 1; i <= number2; i++)
            //{
            Instantiate(redPotion, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, 0), gameObject.transform.rotation);
            // }

        }
        else if (ItemWeight[0] < number && number <= ItemWeight[0] + ItemWeight[1])
        {

            //for (int i = 1; i <= number2; i++)
            //{
            Instantiate(bluePotion, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, 0), gameObject.transform.rotation);
            //}
        }
        else if (ItemWeight[0] + ItemWeight[1] < number && number <= ItemWeight[0] + ItemWeight[1] + ItemWeight[2])
        {
            return;
        }

    }
}
