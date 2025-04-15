using UnityEngine;

public class SellZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Backpack backpack = other.GetComponentInParent<Backpack>();
        if (backpack != null)
        {
            backpack.SellSand();
        }
    }
}
