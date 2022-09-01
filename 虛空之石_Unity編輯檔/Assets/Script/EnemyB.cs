using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : Enemy
{

    internal MonsterWanderB mwB;
    public GameObject player;

    public GameObject redPotion;
    public GameObject bluePotion;
    public float[] ItemWeight = { 5, 4, 3 };
    public float number = 0f;
    //public int number2 = 0;
    void Awake()
    {
        mwB = GetComponent<MonsterWanderB>();
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
        ani.SetBool("Move", mwB.moving);
        ani.SetBool("Attack", mwB.attacking);
        ani.SetBool("Attack2", mwB.attacking2);
        mwB.enableMove = !state.IsTag("attack");

    }

    public override void Damage(float dmg)
    {
        hp -= dmg;
        mwB.takeDamage = true;
        mwB.noDamageTime = mwB.maxNoDamageTime;

        

        if (!this.enabled) { return; }
        //base.Damage(dmg);
        ani.SetTrigger("Hurt");
        
        //Instantiate(beHitAudio, Vector2.zero, Quaternion.identity);
        if (hp <= 0)
        {
            ani.SetTrigger("Die");
            Destroy(mwB);
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
        
        yield return new WaitForSeconds(0.5f);
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
