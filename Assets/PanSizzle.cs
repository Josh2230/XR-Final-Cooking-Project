using UnityEngine;

public class PanSizzle : MonoBehaviour
{
    public AudioSource sizzleSource;   // drag your audio source here

    private int foodCount = 0; // supports multiple items

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            foodCount++;
            UpdateSizzle();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            foodCount = Mathf.Max(0, foodCount - 1);
            UpdateSizzle();
        }
    }

    private void UpdateSizzle()
    {
        if (sizzleSource == null) return;

        if (foodCount > 0)
        {
            if (!sizzleSource.isPlaying)
                sizzleSource.Play();
        }
        else
        {
            sizzleSource.Stop();
        }
    }
}
