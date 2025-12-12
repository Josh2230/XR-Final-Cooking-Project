using UnityEngine;

public class ShakerSprinkle : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem spiceStream;      // child particle system
    [Tooltip("Transform whose UP axis points out of the shaker top. If null, uses this object.")]
    public Transform topReference;

    [Header("Tilt Settings (degrees)")]
    [Tooltip("Max angle from pointing straight DOWN to still sprinkle.")]
    public float maxAngleFromDown = 40f;    // smaller = must be more upside down

    [Header("Particle Flow")]
    public float baseRate = 50f;            // average sprinkle rate
    public float shakeVariation = 0.5f;     // 0 = constant, 1 = very pulsy
    public float shakeFrequency = 15f;      // how fast the variation oscillates

    private ParticleSystem.EmissionModule emission;

    private void Awake()
    {
        if (spiceStream == null)
            spiceStream = GetComponentInChildren<ParticleSystem>();

        if (spiceStream == null)
        {
            Debug.LogError("[ShakerSprinkle] No spiceStream assigned.");
            enabled = false;
            return;
        }

        emission = spiceStream.emission;
        emission.enabled = true;
        emission.rateOverTime = 0f;

        spiceStream.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private void Update()
    {
        // 1. Which transform defines the "top" direction?
        Transform t = topReference != null ? topReference : transform;

        // If your shaker's top points along forward/right, change t.up accordingly.
        Vector3 topDir = t.up;

        // 0° = perfectly down, 180° = perfectly up
        float angleToDown = Vector3.Angle(topDir, Vector3.down);

        bool tiltedDownEnough = angleToDown <= maxAngleFromDown;

        if (!tiltedDownEnough)
        {
            // Not upside down enough: stop sprinkling
            emission.rateOverTime = 0f;
            if (spiceStream.isPlaying)
                spiceStream.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            return;
        }

        // 2. "Fake" shaking by modulating the rate with a sine wave
        float osc = Mathf.Sin(Time.time * shakeFrequency); // -1..1
        float normalized = 0.5f + 0.5f * osc;              // 0..1

        float rate = baseRate * (1f - shakeVariation + shakeVariation * normalized);
        emission.rateOverTime = rate;

        if (!spiceStream.isPlaying)
            spiceStream.Play();
    }
}
