using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blue Potion", menuName = "New Item/Blue Potion")]
public class BluePotion : Item
{
    public float spAddValue = 80f;
    public GameObject useItemAudioPrefab;
    public override void use()
    {
        base.use();

        if (Game.sav.maxSp > Game.sav.sp)
        {
            Instantiate(useItemAudioPrefab, null);
            Game.sav.sp += spAddValue;
            itemHeld -= 1;
        }
        BagManager.RefreshItem();

    }
}
