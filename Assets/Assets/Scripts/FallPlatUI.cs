using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class FallPlatUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float holdTimeRequired = 2.0f;
    public float digAnimationTime = 0.5f;
    public TextMeshProUGUI holdTimerText; // Drag the timer text UI here

    private bool isHolding = false;
    private float holdTimer = 0f;

    void Start()
    {
        UpdateTimerUI(); // Initialize with 0.00 / X.XX
    }

    void Update()
    {
        if (isHolding)
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= holdTimeRequired)
            {
                isHolding = false;
                holdTimer = 0f;
                UpdateTimerUI();
                StartCoroutine(DigAndDestroy());
                return;
            }

            UpdateTimerUI();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        holdTimer = 0f;
        UpdateTimerUI();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
        holdTimer = 0f;
        UpdateTimerUI();
    }

    IEnumerator DigAndDestroy()
    {
        yield return new WaitForSeconds(digAnimationTime);
        Destroy(gameObject);
    }

    void UpdateTimerUI()
    {
        if (holdTimerText != null)
        {
            holdTimerText.text = $"{holdTimer:F2} / {holdTimeRequired:F2}";
        }
    }
}
