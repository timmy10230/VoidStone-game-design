using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageSystem : MonoBehaviour
{
    public float dmg = 1.0f;
    public int atkc = 0;
    public TargetType type = TargetType.Enemy;





    void OnTriggerEnter2D(Collider2D co)
    {
        switch ((int)type)
        {
            case 0:                                                         //玩家打敵人
                if (co.gameObject.tag == "Enemy")
                {
                    co.GetComponent<Enemy>().Damage(dmg);

                    
                    atkc++;
                    if (Game.sav.df < Game.sav.maxDf && atkc >= 5)
                    {
                        Game.sav.df++;
                        atkc = 0;
                    }
                    
                }
                
                else if (co.gameObject.tag == "EnemyBoss")
                {
                    co.GetComponent<EnemyBoss>().TakeDamage(dmg);  //BOSS受傷
                    atkc++;
                    if (Game.sav.df < Game.sav.maxDf && atkc >= 5)
                    {
                        Game.sav.df++;
                        atkc = 0;
                    }
                }
               
                else if (co.gameObject.tag == "EnemyBat")
                {
                    co.GetComponent<EnemyBat>().TakeDamage(dmg);  //bat受傷
                   
                   atkc++;
                    if (Game.sav.df < Game.sav.maxDf && atkc >= 5)
                    {
                        Game.sav.df++;
                        atkc = 0;
                    }
                }

                else if (co.gameObject.tag == "FireWorm")
                {
                    co.GetComponent<FireWorm>().BeHit(dmg);  //bat受傷

                    atkc++;
                    if (Game.sav.df < Game.sav.maxDf && atkc >= 5)
                    {
                        Game.sav.df++;
                        atkc = 0;
                    }
                }
                break;
            case 1:                                        //敵人打玩家
                if (co.gameObject.tag == "Player")
                {
                    co.GetComponent<Battle>().Damage(dmg);
                    


                }
                break;
            case 2:
                if (co.gameObject.tag == "Enemy" )
                {
                    co.GetComponent<Enemy>().Damage(dmg);
                }
                else if(co.gameObject.tag == "Player")
                {
                    co.GetComponent<Battle>().Damage(dmg);
                }
                else if( co.gameObject.tag == "EnemyBat")
                {
                    co.GetComponent<EnemyBat>().TakeDamage(dmg);
                }
                    break;      

        }
    }
    public enum TargetType { Enemy, Player, All };

    

  

    

}
