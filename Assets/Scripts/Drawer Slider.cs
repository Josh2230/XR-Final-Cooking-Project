using UnityEngine;

public class DrawerSlider : MonoBehaviour
{
    public float minZ = 0f;      // closed
    public float maxZ = 0.4f;    // open (tweak value)

    private Vector3 startLocal;

    void Start()
    {
        startLocal = transform.localPosition;
    }

    void Update()
    {
        Vector3 pos = transform.localPosition;
        pos.x = startLocal.x;   // lock X
        pos.y = startLocal.y;   // lock Y
        pos.z = Mathf.Clamp(pos.z, startLocal.z + minZ, startLocal.z + maxZ);
        transform.localPosition = pos;
    }
}
