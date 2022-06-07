using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    [SerializeField] private GameObject DelayCount_01;
    [SerializeField] private GameObject DelayCount_02;

    void Start()
    {
        Catalogue_BlockChain.CreateList_nftID();
        Catalogue_nftPool.CreateList_nftID();
        AlarmDelayCount.DelayCount_01 = DelayCount_01;
        AlarmDelayCount.DelayCount_02 = DelayCount_02;
    }
}
