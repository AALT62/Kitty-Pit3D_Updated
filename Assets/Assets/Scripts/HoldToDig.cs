using UnityEngine;
using TMPro;

public class HoldToDig : MonoBehaviour
{
    public float baseHoldTime = 2.0f;
    public float digSpeedMultiplier = 1.0f;
    public TextMeshProUGUI holdTimerText;
    public Backpack playerBackpack;

    [HideInInspector] public Diggable targetCube;

    private float holdTimer = 0f;
    private bool isHolding = false;

    float AdjustedHoldTime => baseHoldTime / digSpeedMultiplier;

    void Update()
    {
        if (isHolding && targetCube != null)
        {
            holdTimer += Time.deltaTime;
            UpdateTimerUI();

            if (holdTimer >= AdjustedHoldTime * targetCube.digDifficultyMultiplier) // line 34
            {
                isHolding = false;
                holdTimer = 0f;
                UpdateTimerUI();
                if (targetCube != null)
                {
                    playerBackpack.AddSand(1);
                    targetCube.Dig();
                    targetCube = null;
                }
            }
        }
    }

    public void OnHoldStart()
    {
        if (targetCube == null || playerBackpack == null || !playerBackpack.CanDig()) return;

        isHolding = true;
        holdTimer = 0f;
        UpdateTimerUI();
    }

    public void OnHoldEnd()
    {
        isHolding = false;
        holdTimer = 0f;
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if (holdTimerText != null)
        {
            float adjusted = AdjustedHoldTime * (targetCube != null ? targetCube.digDifficultyMultiplier : 1f); // line 39
            holdTimerText.text = targetCube != null ? $"{holdTimer:F2} / {adjusted:F2}" : $"No target";
        }
    }
}
