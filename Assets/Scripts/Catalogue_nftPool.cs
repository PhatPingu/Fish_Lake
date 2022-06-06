using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Catalogue_nftPool
{
    public static List<int> localID_nftList;
    
    public static void CreateList_nftID() //This is TEMP for now
    {
        localID_nftList = new List<int>();
        for(int i = 0; i < Catalogue_BlockChain.sourceID_nftList.Count; i++)
        {
            localID_nftList.Add(Catalogue_BlockChain.sourceID_nftList[i]);
            Debug.Log(localID_nftList[i].ToString());
        }
    }
}
