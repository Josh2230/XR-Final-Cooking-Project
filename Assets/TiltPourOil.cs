using UnityEngine;

public class TiltPourOil : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem oilStream;     // assign OilStream here

    [Header("Tilt Settings")]
    public float pourAngle = 60f;        // degrees from upright before pouring starts

    [Header("Flow Settings")]
    public float minRate = 10f;          // particles/sec at just-starting to pour
    public float maxRate = 40f;          // particles/sec fully upside-down

    private ParticleSystem.EmissionModule emission;

    private void Start()
    {
        if (oilStream == null)
            oilStream = GetComponentInChildren<ParticleSystem>();

        if (oilStream != null)
        {
            emission = oilStream.emission;
            emission.enabled = true;         // keep emission on, control via rateOverTime
            emission.rateOverTime = 0f;
            oilStream.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        else
        {
            Debug.LogWarning("[TiltPourOil] No oilStream assigned.");
        }
    }

    private void Update()
    {
        if (oilStream == null) return;

        // Bottle's "up" direction in world space
        Vector3 bottleUp = transform.up;

        // Angle between bottle up and world up:
        // 0° = straight up, 180° = fully upside-down
        float angleFromUp = Vector3.Angle(bottleUp, Vector3.up);

        if (angleFromUp > pourAngle)
        {
            // how far past the pourAngle (0..1)
            float t = Mathf.InverseLerp(pourAngle, 180f, angleFromUp);

            float rate = Mathf.Lerp(minRate, maxRate, t);
            emission.rateOverTime = rate;

            if (!oilStream.isPlaying)
                oilStream.Play();
        }
        else
        {
            emission.rateOverTime = 0f;
            if (oilStream.isPlaying)
                oilStream.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
