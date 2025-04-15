using UnityEngine;
using TMPro;

public class ToolInventory : MonoBehaviour
{
    public ToolData[] tools; // your tool list (drag into Inspector)
    public int currentToolIndex = 0;

    public TextMeshProUGUI hotbarUI;
    public HoldToDig digSystem;

    void Start()
    {
        EquipTool(currentToolIndex);
    }

    public void EquipTool(int index)
    {
        if (index >= 0 && index < tools.Length)
        {
            currentToolIndex = index;
            ToolData tool = tools[index];
            digSystem.digSpeedMultiplier = tool.digSpeedMultiplier;
            UpdateHotbar();
        }
    }

    public void TryBuyUpgrade()
    {
        Backpack bp = GetComponent<Backpack>();
        if (bp != null && bp.money >= 50 && currentToolIndex + 1 < tools.Length)
        {
            bp.money -= 50;
            EquipTool(currentToolIndex + 1);
        }
    }

    void UpdateHotbar()
    {
        if (hotbarUI != null)
        {
            hotbarUI.text = $"Equipped: {tools[currentToolIndex].toolName}";
        }
    }
}
