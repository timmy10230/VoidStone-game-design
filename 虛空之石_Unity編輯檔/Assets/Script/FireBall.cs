using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    GameObject Boss;
    Animator animator;
    Collider2D collider2d;

    Vector3 dir;

    public GameObject Player;

    public float Speed;

    public float Damge;

    public float LifeTime; //存在時間

    void Start()
    {
        Boss = GameObject.Find("FireWorm");
        animator = GetComponent<Animator>();
        collider2d = GetComponent<Collider2D>();
        Player = GameObject.Find("player");

        dir = transform.localScale;

        //Speed = 5;

        Damge = 1f;

        LifeTime = 4f;
    }

    void Update()
    {
        Move();
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0 || Boss.GetComponent<FireWorm>().isDead)
        {
            Destroy(gameObject);
        }
    }
    public void Move()
    {
        if (Boss.transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(dir.x, dir.y, dir.z);
            transform.position += Speed * -transform.right * Time.deltaTime;
        }
        else if (Boss.transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-dir.x, dir.y, dir.z);
            transform.position += Speed * transform.right * Time.deltaTime;
        }
    }
    public void CloseCollider()
    {
        collider2d.enabled = false;
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D co)
    {      
        if (co.gameObject.tag == "Player")
        {
            Speed = 0;
            animator.Play("Hit");
            Player.GetComponent<Battle>().Damage(1f);
            //Debug.Log("Fireballhit");

        }
        else if (co.CompareTag("Ground"))
        {
            Speed = 0;
            animator.Play("Hit");
            //return;
        }
    }
}
