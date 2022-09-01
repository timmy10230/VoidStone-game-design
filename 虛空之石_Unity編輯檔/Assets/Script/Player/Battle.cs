using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : playerControl
{

    public override void MoveAni()
    {
        base.MoveAni();

        if (Input.GetKeyDown(KeyCode.A)&& gameObject.GetComponent<BoxCollider2D>().enabled != false && Game.sav.CanUseSP())
        {
            ani.SetInteger("Attack", ani.GetInteger("Attack") + 1);
        }
    }

    public override void MovementX()
    {
        if (!lockDash)
        {
            base.MovementX();
            if (state.IsName("Base.Damage")) { return; }

            if (Input.GetKeyDown(KeyCode.A))
            {
                Move(0);
                lockDash = true;
            }

            if (Input.GetKey(KeyCode.X))
            {
                Move(0);
                lockDash = false;
                ani.SetBool("TakeGun", true);
                
            }
            else
            {
                ani.SetBool("TakeGun", false);
                

            }
        }
        
    }



    public override void Move(int i)
    {
        if (CanMove())
        {
            if (Mathf.Abs(i) == 1)
            {
                playerRGB2D.velocity = new Vector2(i * speedX * Time.deltaTime, playerRGB2D.velocity.y);
            }
            else if (Mathf.Abs(i) == 2)
            {
                playerRGB2D.velocity = new Vector2(i * runSpeed * Time.deltaTime, playerRGB2D.velocity.y);
            }
            else if (Mathf.Abs(i) == 0)
            {
                playerRGB2D.velocity = new Vector2(0f, playerRGB2D.velocity.y);
            }
        }
        
        ani.SetFloat("Move", Mathf.Abs(i));
    }

    public override void Direction(int i)
    {
        if (!lockDash)
        {
            transform.eulerAngles = new Vector3(0, 180f * i, 0);
        }
    }

    public bool CanMove()
    {
        return !state.IsTag("lock");
    }

    public void Damage(float dmg)
    {

  
   
        if (isDefence)
        {
            Game.sav.reduceDf(dmg);
            ShieldAudio();
        }
        else if (stealth == false && !isDefence && canMove)
        {
            if (state.IsName("Base.damage")) { return; }
            ani.SetTrigger("Damage");
            Game.sav.Damage(dmg);
            //PlayerHitAudio();


        }
        
            
    }

    public override void TryJump()
    {
        if (!CanMove())
        {
            return;
        }
        base.TryJump();
    }

    public override void dash()
    {
        if (!CanMove())
        {
            return;
        }
        base.dash();


    }

 



}