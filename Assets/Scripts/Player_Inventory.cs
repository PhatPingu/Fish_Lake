using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public List<NFT_Item> playerInventoryNFTs;

    public void Start()
    {
        playerInventoryNFTs = new List<NFT_Item>();
    }

    public void PopulateInventory()
    {

        for(int i = 0; i < playerInventoryNFTs.Count; i++)
        {
            Debug.Log(playerInventoryNFTs[i].ToString());
            Debug.Log(playerInventoryNFTs[i].itemID + " " + playerInventoryNFTs[i].Name + " " + playerInventoryNFTs[i].property_01);
        }
    }
}
