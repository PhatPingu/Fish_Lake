using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Catalogue_BlockChain
{
    public static List<int> source_nftList;
    
    public static void CreateList_nftID() //This is TEMP for now
    {
        source_nftList = new List<int>();
        int randomMax = Random.Range(20,50);
        for(int i = 0; i < randomMax; i++)
        {
            source_nftList.Add(i);
            Debug.Log(source_nftList[i].ToString());
        }
    }
}
