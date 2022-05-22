using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RenderCastLine : MonoBehaviour
{
    [SerializeField] private LineRenderer castLine;
    [SerializeField] private GameObject castLine_handle;
    [SerializeField] private GameObject fishCircle_handle;

    [SerializeField] private Vector3 fishingRod_position;
    [SerializeField] private Vector3 fishingBulb_position;

    void Update()
    {
        Update_CastLine();
    }

    void Update_CastLine()
    {
        fishingRod_position = castLine_handle.transform.position;
        fishingBulb_position = fishCircle_handle.transform.position;
        
        castLine.SetPosition(0, fishingRod_position);
        castLine.SetPosition(1, fishingBulb_position);
    }


}
