using UnityEngine;

public class DigTargetDetector : MonoBehaviour
{
    public HoldToDig digManager;

    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Diggable diggable))
        {
            digManager.targetCube = diggable;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Diggable diggable))
        {
            if (digManager.targetCube == diggable)
            {
                digManager.targetCube = null;
            }
        }
    }
}
