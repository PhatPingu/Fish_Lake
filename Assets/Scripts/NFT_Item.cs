using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NFT_Item", menuName = "ScriptableObjects/NFT_Item", order = 1)]
public class NFT_Item : ScriptableObject
{
    public int itemID;
    public string Name;
    public int property_01;
    public bool equipable;
}
