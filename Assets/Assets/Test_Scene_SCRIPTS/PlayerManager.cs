using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [Header("Tool & Backpack")]
    public string equippedTool = "Dirt_Shovel";
    public float toolMultiplier = 1f;
    public int backpackCapacity = 20;
    public int sandCount = 0;
    public float money = 0f;

    [Header("Digging")]
    public Transform digZone;
    public float digHoldTime = 0f;
    public float currentDigTime = 0f;
    private bool isDigging = false;
    private DiggableBlock targetBlock;

    [Header("UI")]
    public Text hotbarText, sandText, moneyText, timerText, warningText;

    [Header("Jump Cancel")]
    public KeyCode digKey = KeyCode.E;
    public KeyCode jumpKey = KeyCode.Space;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateUI();
    }

    void Update()
    {
        DetectDiggable();

        if (Input.GetKeyDown(jumpKey)) CancelDig();

        if (Input.GetKey(digKey) && targetBlock != null && sandCount < backpackCapacity)
        {
            if (!isDigging) StartDig();
            currentDigTime += Time.deltaTime;
            timerText.text = $"Digging {targetBlock.tier} - {currentDigTime:F2} / {digHoldTime:F2}s";

            if (currentDigTime >= digHoldTime)
            {
                CollectSand();
                ResetDig();
            }
        }
        else if (Input.GetKeyUp(digKey) || sandCount >= backpackCapacity)
        {
            CancelDig();
        }
    }

    void StartDig()
    {
        isDigging = true;
        digHoldTime = targetBlock.baseDigTime / toolMultiplier;
        currentDigTime = 0f;
    }

    void CancelDig()
    {
        isDigging = false;
        timerText.text = "";
        currentDigTime = 0f;
    }

    void ResetDig()
    {
        isDigging = false;
        timerText.text = "";
        currentDigTime = 0f;
    }

    void CollectSand()
    {
        sandCount++;
        Destroy(targetBlock.gameObject);
        UpdateUI();
    }

    void UpdateUI()
    {
        hotbarText.text = $"Tool: {equippedTool}";
        sandText.text = $"{sandCount}/{backpackCapacity}";
        moneyText.text = $"${money}";
        warningText.text = (sandCount >= backpackCapacity * 0.85f) ? "Backpack almost full!" : "";
    }

    void DetectDiggable()
    {
        targetBlock = null;
        Collider[] hits = Physics.OverlapBox(digZone.position, Vector3.one * 0.25f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Diggable"))
            {
                targetBlock = hit.GetComponent<DiggableBlock>();
                break;
            }
        }
    }

    public void SellSand()
    {
        money += sandCount * 10f;
        sandCount = 0;
        UpdateUI();
    }

    public void BuyTool(string toolName, float multiplier, float cost)
    {
        if (money >= cost)
        {
            equippedTool = toolName;
            toolMultiplier = multiplier;
            money -= cost;
            UpdateUI();
        }
    }
}


