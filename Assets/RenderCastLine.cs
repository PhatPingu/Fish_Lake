using UnityEngine;


public class RenderCastLine : MonoBehaviour
{
    [SerializeField] private LineRenderer castLine;

    [SerializeField] private GameObject fishCircle_handle;
    [SerializeField] private GameObject fishingRodTip;

    [SerializeField] private Vector3 fishingRod_position;
    [SerializeField] private Vector3 fishingBulb_position;

    [ExecuteInEditMode]
    void Update()
    {
        Update_CastLine();
    }

    void Update_CastLine()
    {
        fishingRod_position = fishingRodTip.transform.position;
        fishingBulb_position = Camera.main.ScreenToWorldPoint(fishCircle_handle.transform.position);
        
        castLine.SetPosition(0, fishingRod_position);
        castLine.SetPosition(1, fishingBulb_position);
    }


}
