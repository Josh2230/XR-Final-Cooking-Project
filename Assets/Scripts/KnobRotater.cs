using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class KnobRotater : MonoBehaviour
{
    [Header("Burner Flame")]
    public GameObject flame;
    public PanHeatZone panHeatZone; // assign in Inspector

    private XRGrabInteractable grab;
    private bool isOn = false;   // tracks fire state

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();

        // Listen for grabs (Select Entered)
        grab.selectEntered.AddListener(OnGrab);

        // Prevent the knob from moving
        grab.trackPosition = false;
        grab.trackRotation = false;
        grab.trackScale    = false;

        if (flame == null)
            Debug.LogWarning("[KnobRotater] Flame reference missing!");
    }

    private void Start()
    {
        if (flame != null)
            flame.SetActive(false);   // fire starts OFF
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        ToggleFire();
    }

    private void ToggleFire()
    {
        if (flame == null) return;

        isOn = !isOn;  // flip state

        flame.SetActive(isOn);

        if (panHeatZone != null)
            panHeatZone.isHot = isOn;

        // If it’s a ParticleSystem, explicitly play/stop it
        var ps = flame.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            if (isOn)
                ps.Play();
            else
                ps.Stop();
        }

        Debug.Log(isOn ? "🔥 Fire ON" : "❄️ Fire OFF");
    }
}
