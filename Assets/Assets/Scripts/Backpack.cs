using UnityEngine;
using TMPro;

public class Backpack : MonoBehaviour
{
    public int sandStored = 0;
    public int maxCapacity = 25;
    public int money = 0;

    public TextMeshProUGUI sandUI;
    public TextMeshProUGUI moneyUI;
    public TextMeshProUGUI warningUI;

    public void AddSand(int amount)
    {
        if (sandStored + amount > maxCapacity)
        {
            ShowWarning("Backpack Full!"); // line 17
            return;
        }

        sandStored += amount;
        UpdateUI();

        float fillPercent = (float)sandStored / maxCapacity; // line 22
        if (fillPercent >= 0.85f && fillPercent < 1f) // line 23
        {
            ShowWarning("Backpack Almost Full!"); // line 24
        }
        else // line 25
        {
            ClearWarning(); // line 26
        }
    }

    public bool CanDig()
    {
        return sandStored < maxCapacity;
    }

    public void SellSand()
    {
        money += sandStored * 10;
        sandStored = 0;
        UpdateUI();
        ClearWarning(); // line 36
    }

    void Start()
    {
        UpdateUI();
        ClearWarning(); // line 39
    }

    void UpdateUI()
    {
        if (sandUI != null)
            sandUI.text = $"Sand: {sandStored} / {maxCapacity}";

        if (moneyUI != null)
            moneyUI.text = $"$ Money: {money}";
    }

    void ShowWarning(string msg) // line 45
    {
        if (warningUI != null)
            warningUI.text = msg;
    }

    void ClearWarning()
    {
        if (warningUI != null)
            warningUI.text = "";
    }
}
