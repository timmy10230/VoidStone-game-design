using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Item", menuName = "New Item/Bullet Item")]
public class BulletItem : Item
{

    /*public float coldTime = 2.0f;//定义技能的冷却时间
    public float timer = 0;//定义计时器
    public Image filledImage;//定义一个填充图片，下面从start方法里获取实例中的填充图片
    public bool isStartTime = false;//定义标志量，决定是否开始计时 */

    public override void use()
    {
        base.use();

        itemHeld -= 1;
        BagManager.RefreshItem();

    }
}
