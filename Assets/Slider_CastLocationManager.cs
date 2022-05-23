using UnityEngine;
using UnityEngine.UI;

public class Slider_CastLocationManager : MonoBehaviour
{
    [SerializeField] private Slider Slider_CastLine;
    [SerializeField] private Slider Slider_CastLocation;

    void Update()
    {
        Slider_CastLocation.value = Slider_CastLine.value;
    }
}
