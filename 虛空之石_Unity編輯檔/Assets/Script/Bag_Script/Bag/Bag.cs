using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bag", menuName = "Bag")]
public class Bag : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
}
