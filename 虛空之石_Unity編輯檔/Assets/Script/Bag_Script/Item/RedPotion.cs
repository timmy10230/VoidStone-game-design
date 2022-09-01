using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Red Potion", menuName = "New Item/Red Potion")]
public class RedPotion : Item
{
    
    public float healValue = 1f;
    public GameObject useItemAudioPrefab;
    /*public float coldTime = 2.0f;//定义技能的冷却时间
    public float timer = 0;//定义计时器
    public Image filledImage;//定义一个填充图片，下面从start方法里获取实例中的填充图片
    public bool isStartTime = false;//定义标志量，决定是否开始计时 */

    public override void use()
    {
        base.use();
        
        if (Game.sav.maxHp > Game.sav.hp)
        {
            Instantiate(useItemAudioPrefab, null);
            Game.sav.hp += healValue;
            itemHeld -= 1;
            BagManager.RefreshItem();

        }

    }


}
