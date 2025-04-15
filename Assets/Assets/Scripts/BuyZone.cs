using UnityEngine;

public class BuyZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        ToolInventory inv = other.GetComponentInParent<ToolInventory>();
        if (inv != null)
        {
            inv.TryBuyUpgrade();
        }
    }
}
