using UnityEngine;

public class FryableFood : MonoBehaviour
{
    [Header("Visuals")]
    public Renderer targetRenderer;        // mesh renderer of the meat
    public Material rawMaterial;
    public Material cookedMaterial;
    public Material burntMaterial;

    [Header("Cooking Times (seconds)")]
    public float timeToCook = 5f;          // raw -> nicely cooked
    public float timeToBurn = 10f;         // cooked -> burnt

    [Header("State (debug)")]
    public float cookAmount = 0f;          // how long we've been cooked
    public bool isInHotPan = false;
    public float heatMultiplier = 1f;      // from pan (heat strength)

    private void Start()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponentInChildren<Renderer>();

        if (targetRenderer != null && rawMaterial != null)
            targetRenderer.material = rawMaterial;
    }

    private void Update()
    {
        if (isInHotPan)
        {
            cookAmount += Time.deltaTime * heatMultiplier;
            UpdateVisual();
        }
    }

    public void SetCooking(bool cooking, float heat = 1f)
    {
        isInHotPan = cooking;
        heatMultiplier = heat;
    }

    private void UpdateVisual()
    {
        if (targetRenderer == null) return;

        if (cookAmount >= timeToBurn)
        {
            // Fully burnt
            if (burntMaterial != null)
                targetRenderer.material = burntMaterial;
        }
        else if (cookAmount >= timeToCook)
        {
            // Nicely cooked
            if (cookedMaterial != null)
                targetRenderer.material = cookedMaterial;
        }
        else
        {
            // In-between: you could lerp between raw & cooked if you want
            if (rawMaterial != null && cookedMaterial != null)
            {
                float t = Mathf.Clamp01(cookAmount / timeToCook);
                // Simple lerp-ish effect by swapping material color
                Color c = Color.Lerp(rawMaterial.color, cookedMaterial.color, t);
                targetRenderer.material.color = c;
            }
        }
    }
}
