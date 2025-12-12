using UnityEngine;

public class FryableFood : MonoBehaviour
{
    [Header("Visuals")]
    public Renderer targetRenderer;          // Mesh renderer of the meat

    // Use arrays so you can assign multiple materials (for submeshes)
    public Material[] rawMaterials;
    public Material[] cookedMaterials;
    public Material[] burntMaterials;

    [Header("Cooking Times (seconds)")]
    public float timeToCook = 5f;            // raw -> nicely cooked
    public float timeToBurn = 10f;           // cooked -> burnt

    [Header("State (debug)")]
    public float cookAmount = 0f;            // how long we've been cooked
    public bool isInHotPan = false;
    public float heatMultiplier = 1f;        // from pan (heat strength)

    private bool usingRaw = true;
    private bool usingCooked = false;
    private bool usingBurnt = false;

    private void Start()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponentInChildren<Renderer>();

        // Start as raw
        if (targetRenderer != null && rawMaterials != null && rawMaterials.Length > 0)
        {
            SetMaterialsSafe(rawMaterials);
            usingRaw = true;
            usingCooked = false;
            usingBurnt = false;
        }
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

        // Burnt
        if (cookAmount >= timeToBurn)
        {
            if (!usingBurnt && burntMaterials != null && burntMaterials.Length > 0)
            {
                SetMaterialsSafe(burntMaterials);
                usingBurnt = true;
                usingCooked = false;
                usingRaw = false;
            }
        }
        // Cooked
        else if (cookAmount >= timeToCook)
        {
            if (!usingCooked && cookedMaterials != null && cookedMaterials.Length > 0)
            {
                SetMaterialsSafe(cookedMaterials);
                usingCooked = true;
                usingBurnt = false;
                usingRaw = false;
            }
        }
        // Raw (or not yet cooked enough to switch)
        else
        {
            if (!usingRaw && rawMaterials != null && rawMaterials.Length > 0)
            {
                SetMaterialsSafe(rawMaterials);
                usingRaw = true;
                usingCooked = false;
                usingBurnt = false;
            }
        }
    }

    /// <summary>
    /// Safely assign material arrays to the renderer, matching submesh count.
    /// </summary>
    private void SetMaterialsSafe(Material[] source)
    {
        if (targetRenderer == null || source == null || source.Length == 0)
            return;

        // Get current length (number of submeshes)
        var current = targetRenderer.sharedMaterials;
        int count = current.Length;

        // Build a new array that matches submesh count
        Material[] final = new Material[count];
        for (int i = 0; i < count; i++)
        {
            // If source has fewer materials, reuse the last one
            final[i] = source[Mathf.Min(i, source.Length - 1)];
        }

        targetRenderer.materials = final;
    }
}
