using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Reward_Generator
{
    public static int possibleRewardQuantity;

    public static void PickRandomItem(int quantity)
    {
        if(quantity > 0) 
        {
            possibleRewardQuantity -= 1;

            int itemIDPicked = Random.Range(0, Catalogue_nftPool.localID_nftList.Count);
            
            NFT_Item newEquip = ScriptableObject.CreateInstance<NFT_Item>();
            newEquip.itemID = itemIDPicked;
            newEquip.Name = Catalogue_nftPool.localID_nftList[itemIDPicked].ToString();
            newEquip.property_01 = Random.Range(1,10);
            
            if(newEquip.property_01 > 0)
            {
                newEquip.equipable = true;
            }

            GameObject InventoryManager = GameObject.Find("Inventory Manager");
            Player_Inventory Player_Inventory = InventoryManager.GetComponent<Player_Inventory>();
            Player_Inventory.playerInventoryNFTs.Add(newEquip);
            Player_Inventory.PopulateInventory();
        }
    }
}
