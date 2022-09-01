using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyF : Enemy
{
    public GameObject player;
    internal MonsterWanderF mwF;
    public AudioSource attack3Audio;

    public GameObject redPotion;
    public GameObject bluePotion;
    public float[] ItemWeight = { 5, 4, 3 };
    public float number = 0f;
    //public int number2 = 0;
    void Awake()
    {
        mwF = GetComponent<MonsterWanderF>();
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
        ani.SetBool("Move", mwF.moving);
        ani.SetBool("Attack", mwF.attacking);
        ani.SetBool("Attack2", mwF.attacking2);
        mwF.enableMove = !state.IsTag("attack");

    }

    public override void Damage(float dmg)
    {
        hp -= dmg;
        mwF.takeDamage = true;
        mwF.noDamageTime = mwF.maxNoDamageTime;



        if (!this.enabled) { return; }
        //base.Damage(dmg);
        ani.SetTrigger("Hurt");

        if (hp <= 0)
        {
            ani.SetTrigger("Die");
            Destroy(mwF);
            this.enabled = false;
            PlayAttack3Audio();
        }
        StartCoroutine("Hit");
    }
    public void PlayAttack3Audio()
    {
        Invoke("Attack3Audio", 0.8f);
    }
    public void Attack3Audio()
    {
        attack3Audio.Play();
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
