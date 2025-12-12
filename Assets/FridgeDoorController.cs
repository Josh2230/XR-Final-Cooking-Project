using UnityEngine;

public class FridgeDoorController : MonoBehaviour
{
    [Header("Door Transform (pivot at hinge)")]
    public Transform doorPivot;

    [Header("Angles (local Y)")]
    public float closedAngle = 0f;
    public float openAngle = -100f;   // adjust to how wide you want it

    [Header("Animation")]
    public float openSpeed = 5f;      // higher = faster

    private bool isOpen = false;
    private float targetAngle;

    private void Start()
    {
        if (doorPivot == null)
            doorPivot = transform;

        targetAngle = closedAngle;
    }

    private void Update()
    {
        if (doorPivot == null) return;

        Quaternion current = doorPivot.localRotation;
        Quaternion target = Quaternion.Euler(0f, targetAngle, 0f);

        doorPivot.localRotation = Quaternion.Slerp(
            current,
            target,
            Time.deltaTime * openSpeed
        );
    }

    // Called by button / XR interaction
    public void ToggleDoor()
    {
        isOpen = !isOpen;
        targetAngle = isOpen ? openAngle : closedAngle;
    }
}
