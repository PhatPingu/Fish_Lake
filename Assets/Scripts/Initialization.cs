using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    void Start()
    {
        Catalogue_BlockChain.CreateList_nftID();
        Catalogue_nftPool.CreateList_nftID();
    }
}
