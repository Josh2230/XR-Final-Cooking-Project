using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Rigidbody))]
public class DrawerSliderPhysics : MonoBehaviour
{
    [Header("Slide Distance (LOCAL Z, in meters)")]
    public float minZ = 0f;     // closed (offset from starting local Z)
    public float maxZ = 0.4f;   // open   (offset from starting local Z)

    [Header("XR (optional)")]
    public XRGrabInteractable grabInteractable;

    private Rigidbody rb;
    private Transform parent;
    private Vector3 startLocalPos;
    private Quaternion startLocalRot;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        parent = transform.parent;

        if (!parent)
        {
            Debug.LogWarning("[DrawerSliderPhysics] Drawer has no parent. Using world space instead.");
            parent = transform; // fallback to self
        }

        if (!grabInteractable)
        {
            grabInteractable = GetComponent<XRGrabInteractable>();
        }
    }

    private void Start()
    {
        startLocalPos = parent.InverseTransformPoint(transform.position);
        startLocalRot = Quaternion.Inverse(parent.rotation) * transform.rotation;
    }

    private void FixedUpdate()
    {
        // Current position -> parent local space
        Vector3 localPos = parent.InverseTransformPoint(rb.position);

        // Lock X/Y to their start, allow only Z to change
        float clampedZ = Mathf.Clamp(localPos.z, startLocalPos.z + minZ, startLocalPos.z + maxZ);
        localPos = new Vector3(startLocalPos.x, startLocalPos.y, clampedZ);

        // Back to world space
        Vector3 worldPos = parent.TransformPoint(localPos);
        Quaternion worldRot = parent.rotation * startLocalRot;

        rb.MovePosition(worldPos);
        rb.MoveRotation(worldRot);
    }
}
