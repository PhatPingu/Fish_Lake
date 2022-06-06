using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Catalogue_BlockChain
{
    public static List<int> sourceID_nftList;
    
    public static void CreateList_nftID() //This is TEMP for now
    {
        sourceID_nftList = new List<int>();
        int randomMax = Random.Range(20,50);
        for(int i = 0; i < randomMax; i++)
        {
            sourceID_nftList.Add(i);
            Debug.Log(sourceID_nftList[i].ToString());
        }
    }
}
