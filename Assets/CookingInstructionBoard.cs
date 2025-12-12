using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CookingInstructionBoard : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI stepText;
    public TextMeshProUGUI stepCounterText;
    public Button nextButton;
    public Button prevButton;

    [Header("Instructions")]
    [TextArea(2, 4)]
    public string[] steps;

    private int currentStep = 0;

    private void Awake()
    {
        // Wire up button events
        if (nextButton != null)
            nextButton.onClick.AddListener(NextStep);

        if (prevButton != null)
            prevButton.onClick.AddListener(PreviousStep);
    }

    private void Start()
    {
        ShowStep(0);
    }

    public void NextStep()
    {
        ShowStep(currentStep + 1);
    }

    public void PreviousStep()
    {
        ShowStep(currentStep - 1);
    }

    private void ShowStep(int index)
    {
        if (steps == null || steps.Length == 0 || stepText == null)
            return;

        currentStep = Mathf.Clamp(index, 0, steps.Length - 1);

        stepText.text = steps[currentStep];

        if (stepCounterText != null)
        {
            stepCounterText.text = $"Step {currentStep + 1} / {steps.Length}";
        }

        // Enable/disable buttons at ends
        if (prevButton != null)
            prevButton.interactable = currentStep > 0;

        if (nextButton != null)
            nextButton.interactable = currentStep < steps.Length - 1;
    }
}
