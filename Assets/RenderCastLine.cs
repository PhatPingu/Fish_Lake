using UnityEngine;


public class RenderCastLine : MonoBehaviour
{
    [SerializeField] private LineRenderer castLine;

    [SerializeField] private GameObject castLocation_handle;
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
        fishingRod_position.z = -1f;
        fishingBulb_position = Camera.main.ScreenToWorldPoint(castLocation_handle.transform.position);
        fishingBulb_position.z = -1f;
        
        castLine.SetPosition(0, fishingRod_position);
        castLine.SetPosition(1, fishingBulb_position);
    }


}
