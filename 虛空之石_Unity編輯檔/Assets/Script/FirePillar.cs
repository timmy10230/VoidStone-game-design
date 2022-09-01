using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillar : MonoBehaviour
{
    Animator ani;
    AudioSource music;
    public float Speed;
    public float Damge;
    public bool IsBoom;
    public GameObject Player;
    void Awake()
    {
        ani = GetComponent<Animator>();
        music = GetComponent<AudioSource>();
        Player = GameObject.Find("player");

        Speed = Random.Range(5f, 10f);
        Damge = 2f;
        IsBoom = false;
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (!IsBoom) 
        {
            Move();
        }
    }
    public void Move() 
    {
        transform.position += Speed *Vector3.down * Time.deltaTime;
        //Debug.Log("MOVE");
    }
    public void ChangePosition() 
    {
        transform.position = new Vector3(transform.position.x, -3.35f, 0);
    }
    public void Destroy() 
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D co)
    {
        //Debug.Log("Collider");
        if (co.gameObject.tag == "Player")
        {
            //animator.Play("Hit");
            IsBoom = true;
            Player.GetComponent<Battle>().Damage(Damge);
            //Debug.Log("Fireballhit");
            //transform.Rotate(0, 0, -90);
            ani.Play("boom");
            //Destroy();

        }
        else if (co.CompareTag("Ground"))
        {
            //animator.Play("Hit");
            IsBoom = true;
            //transform.position = new Vector3(transform.position.x, -1.6f, 0);
            //Speed = 0;
            music.Play();
            ani.Play("boom");           
            //Debug.Log("boom");
        }

    }

}
