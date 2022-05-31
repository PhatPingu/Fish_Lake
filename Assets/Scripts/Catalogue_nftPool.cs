using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Catalogue_nftPool
{
    public static List<int> local_nftList;
    
    public static void CreateList_nftID() //This is TEMP for now
    {
        local_nftList = new List<int>();
        for(int i = 0; i < Catalogue_BlockChain.source_nftList.Count; i++)
        {
            local_nftList.Add(Catalogue_BlockChain.source_nftList[i]);
            Debug.Log(local_nftList[i].ToString());
        }
    }
}
