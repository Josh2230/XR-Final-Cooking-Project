using UnityEngine;

public class PanHeatZone : MonoBehaviour
{
    [Header("Pan Heat Settings")]
    public bool isHot = true;      // later you can link this to your stove knob
    public float heatStrength = 1f;

    private void OnTriggerEnter(Collider other)
    {
        var food = other.GetComponent<FryableFood>();
        if (food != null)
        {
            Debug.Log("[PanHeatZone] Food entered: " + other.name);
            if (isHot)
                food.SetCooking(true, heatStrength);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var food = other.GetComponent<FryableFood>();
        if (food != null)
        {
            Debug.Log("[PanHeatZone] Food exited: " + other.name);
            food.SetCooking(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // In case isHot changes while food is already in the pan
        var food = other.GetComponent<FryableFood>();
        if (food != null)
        {
            food.SetCooking(isHot, heatStrength);
        }
    }
}
