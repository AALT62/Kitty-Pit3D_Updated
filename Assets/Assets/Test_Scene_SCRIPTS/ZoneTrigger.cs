using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    public bool isSellZone = false;

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerManager>();
        if (player == null) return;

        if (isSellZone)
        {
            player.SellSand();
        }
        else
        {
            // TEMP: Auto-buy better tool (replace later with Shop UI)
            player.BuyTool("Iron_Shovel", 2f, 100f);
        }
    }
}


