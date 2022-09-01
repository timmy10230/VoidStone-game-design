using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{   //攻擊傷害
    public float attackDamage1 = 2;
    public float attackDamage2 = 1;
    public float attackDamage3 = 1;

  
    //攻擊判斷範圍
    public float attackRange1_1 = 0.9f;
    public float attackRange1_2 = 0.3f;
    public float attackRange1_3 = 0.4f;
    public float attackRange1_4 = 0.4f;

    public float attackRange2 = 0.5f;
    public float attackRange3 = 0.5f;
    
    //攻擊範圍調整起始位置
    public Vector3 attackOffset1_1;
    public Vector3 attackOffset1_2;
    public Vector3 attackOffset1_3;
    public Vector3 attackOffset1_4;
        
    public Vector3 attackOffset2;

    public Vector3 attackOffset3_1;
    public Vector3 attackOffset3_2;
    public Vector3 attackOffset3_3;

    //判斷圖層
    public LayerMask attackMask ;


    public void Attack1()
    {
        Vector3 pos1_1 = transform.position;
        pos1_1 += transform.right * attackOffset1_1.x;
        pos1_1 += transform.up * attackOffset1_1.y;

        Vector3 pos1_2 = transform.position;
        pos1_2 += transform.right * attackOffset1_2.x;
        pos1_2 += transform.up * attackOffset1_2.y;


        Vector3 pos1_3 = transform.position;
        pos1_3 += transform.right * attackOffset1_3.x;
        pos1_3 += transform.up * attackOffset1_3.y;

        Vector3 pos1_4 = transform.position;
        pos1_4 += transform.right * attackOffset1_2.x;
        pos1_4 += transform.up * attackOffset1_2.y;

        Collider2D colInfo1 = Physics2D.OverlapCircle(pos1_1, attackRange1_1, attackMask);  //Physics2D.OverlapCircle取得碰撞對象；參數:圓形中心，圓形半徑，篩選器:檢查指定圖層上的對象，回傳值BOOL或是碰到的碰撞體Collider
        if(Physics2D.OverlapCircle(pos1_2, attackRange1_2, attackMask))
        {
            colInfo1 = Physics2D.OverlapCircle(pos1_2, attackRange1_2, attackMask);
        }
        else if(Physics2D.OverlapCircle(pos1_3, attackRange1_3, attackMask))
        {
            colInfo1 = Physics2D.OverlapCircle(pos1_3, attackRange1_3, attackMask);
        }
        else if (Physics2D.OverlapCircle(pos1_4, attackRange1_4, attackMask))
        {
            colInfo1 = Physics2D.OverlapCircle(pos1_4, attackRange1_4, attackMask);
        }
           
        if (colInfo1 != null )
        {
            colInfo1.GetComponent<BossDamageSystem>().PlayerTakeDamage(attackDamage1);
        }
    }

    /*public void Attack1_1()
    {
        Vector3 pos1 = transform.position;
        pos1 += transform.right * attackOffset1_1.x;
        pos1 += transform.up * attackOffset1_1.y;

        Collider2D colInfo1 = Physics2D.OverlapCircle(pos1, attackRange1_1, attackMask);  //Physics2D.OverlapCircle取得碰撞對象；參數:圓形中心，圓形半徑，篩選器:檢查指定圖層上的對象，回傳值BOOL或是碰到的碰撞體Collider
        if (colInfo1 != null)                                                                        
        {
            colInfo1.GetComponent<BossDamageSystem>().PlayerTakeDamage(attackDamage1);  
        }
        
    }

    public void Attack1_2()
    {
        Vector3 pos1 = transform.position;
        pos1 += transform.right * attackOffset1_2.x;
        pos1 += transform.up * attackOffset1_2.y;

        Collider2D colInfo1 = Physics2D.OverlapCircle(pos1, attackRange1_2, attackMask);  //Physics2D.OverlapCircle取得碰撞對象；參數:圓形中心，圓形半徑，篩選器:檢查指定圖層上的對象，回傳值BOOL或是碰到的碰撞體Collider
        if (colInfo1 != null)
        {
            colInfo1.GetComponent<BossDamageSystem>().PlayerTakeDamage(attackDamage1);
        }

    }

    public void Attack1_3()
    {
        Vector3 pos1 = transform.position;
        pos1 += transform.right * attackOffset1_3.x;
        pos1 += transform.up * attackOffset1_3.y;

        Collider2D colInfo1 = Physics2D.OverlapCircle(pos1, attackRange1_3, attackMask);  //Physics2D.OverlapCircle取得碰撞對象；參數:圓形中心，圓形半徑，篩選器:檢查指定圖層上的對象，回傳值BOOL或是碰到的碰撞體Collider
        if (colInfo1 != null)
        {
            colInfo1.GetComponent<BossDamageSystem>().PlayerTakeDamage(attackDamage1);
        }

    }

    public void Attack1_4()
    {
        Vector3 pos1 = transform.position;
        pos1 += transform.right * attackOffset1_4.x;
        pos1 += transform.up * attackOffset1_4.y;

        Collider2D colInfo1 = Physics2D.OverlapCircle(pos1, attackRange1_4, attackMask);  //Physics2D.OverlapCircle取得碰撞對象；參數:圓形中心，圓形半徑，篩選器:檢查指定圖層上的對象，回傳值BOOL或是碰到的碰撞體Collider
        if (colInfo1 != null)
        {
            colInfo1.GetComponent<BossDamageSystem>().PlayerTakeDamage(attackDamage1);
        }

    }*/

    public void Attack2()
    {
        Vector3 pos2 = transform.position;
        pos2 += transform.right * attackOffset2.x;
        pos2 += transform.up * attackOffset2.y;

        Collider2D colInfo2 = Physics2D.OverlapCircle(pos2, attackRange2,  attackMask);  
        if (colInfo2 != null)
        {
            colInfo2.GetComponent<BossDamageSystem>().PlayerTakeDamage(attackDamage2);
            
        }
    }

    public void Attack3_1()
    {
        Vector3 pos3 = transform.position;
        pos3 += transform.right * attackOffset3_1.x;
        pos3 += transform.up * attackOffset3_1.y;

        Collider2D colInfo3 = Physics2D.OverlapCircle(pos3, attackRange3, attackMask);  
        if (colInfo3 != null)
        {
            colInfo3.GetComponent<BossDamageSystem>().PlayerTakeDamage(attackDamage3);
            
        }
    }

    public void Attack3_2()
    {
        Vector3 pos3 = transform.position;
        pos3 += transform.right * attackOffset3_2.x;
        pos3 += transform.up * attackOffset3_2.y;

        Collider2D colInfo3 = Physics2D.OverlapCircle(pos3, attackRange3, attackMask);  
        if (colInfo3 != null)
        {
            colInfo3.GetComponent<BossDamageSystem>().PlayerTakeDamage(attackDamage3);
            
        }
    }

    public void Attack3_3()
    {
        Vector3 pos3 = transform.position;
        pos3 += transform.right * attackOffset3_3.x;
        pos3 += transform.up * attackOffset3_3.y;

        Collider2D colInfo3 = Physics2D.OverlapCircle(pos3, attackRange3, attackMask);  
        if (colInfo3 != null)
        {
            colInfo3.GetComponent<BossDamageSystem>().PlayerTakeDamage(attackDamage3);
            
        }
    }


    void OnDrawGizmosSelected()  //攻擊範圍圖形 只有編輯區才看的到
    {
        Vector3 pos1_1 = transform.position;
        pos1_1 += transform.right * attackOffset1_1.x;
        pos1_1 += transform.up * attackOffset1_1.y;
        Gizmos.DrawWireSphere(pos1_1, attackRange1_1);  // DrawWireSphere 圓形 (中心位置,半徑)

        Vector3 pos1_2 = transform.position;
        pos1_2 += transform.right * attackOffset1_2.x;
        pos1_2 += transform.up * attackOffset1_2.y;
        Gizmos.DrawWireSphere(pos1_2, attackRange1_2);  // DrawWireSphere 圓形 (中心位置,半徑)

        Vector3 pos1_3 = transform.position;
        pos1_3 += transform.right * attackOffset1_3.x;
        pos1_3 += transform.up * attackOffset1_3.y;
        Gizmos.DrawWireSphere(pos1_3, attackRange1_3);  // DrawWireSphere 圓形 (中心位置,半徑)

        Vector3 pos1_4 = transform.position;
        pos1_4 += transform.right * attackOffset1_4.x;
        pos1_4 += transform.up * attackOffset1_4.y;
        Gizmos.DrawWireSphere(pos1_4, attackRange1_4);  // DrawWireSphere 圓形 (中心位置,半徑)

        Vector3 pos2 = transform.position;
        pos2 += transform.right * attackOffset2.x;
        pos2 += transform.up * attackOffset2.y;
        Gizmos.DrawWireSphere(pos2, attackRange2);  // DrawWireSphere 圓形 (中心位置,半徑)

        Vector3 pos3_1 = transform.position;
        pos3_1 += transform.right * attackOffset3_1.x;
        pos3_1 += transform.up * attackOffset3_1.y;
        Gizmos.DrawWireSphere(pos3_1, attackRange3);  // DrawWireSphere 圓形 (中心位置,半徑)

        Vector3 pos3_2 = transform.position;
        pos3_2 += transform.right * attackOffset3_2.x;
        pos3_2 += transform.up * attackOffset3_2.y;
        Gizmos.DrawWireSphere(pos3_2, attackRange3);  // DrawWireSphere 圓形 (中心位置,半徑)

        Vector3 pos3_3 = transform.position;
        pos3_3 += transform.right * attackOffset3_3.x;
        pos3_3 += transform.up * attackOffset3_3.y;
        Gizmos.DrawWireSphere(pos3_3, attackRange3);  // DrawWireSphere 圓形 (中心位置,半徑)


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
